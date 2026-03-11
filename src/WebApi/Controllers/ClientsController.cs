using Application.Clients.Commands;
using Application.Clients.DTOs;
using Application.Clients.Queries;
using Core.DomainErrors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Clients;

namespace WebApi.Controllers;

[ApiController]
[Route("clients")]
public class ClientsController : ControllerBase
{
    private readonly ISender _sender;
    
    public ClientsController(ISender sender)
    {
        _sender = sender;
    }

    
    [HttpGet]
    public async Task<ActionResult<ClientDto>> GetAllClients(
        [FromQuery(Name = "name")] string? name,
        [FromQuery(Name = "cnpj")] string? cnpj,
        [FromQuery(Name = "status")] string? status,
        [FromQuery(Name = "orderby")] string? orderby,
        [FromQuery(Name = "descending")] bool? descending)
    {
        var query = new GetAllClients.Query(cnpj, name, status, orderby, descending);
        var result = await _sender.Send(query);
        
        if (result.IsFailure)
            return BadRequest(result.Errors);
        
        return Ok(result.Value);
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClientDto>> GetClientById(Guid id)
    {
        var query = new GetClientById.Query(id);
        var result = await _sender.Send(query);
        
        if (result.IsFailure)
        {
            if (result.Errors.Code.Equals("Client.NotFound"))
                return NotFound(result.Errors);
            
            return BadRequest(result.Errors);
        }
        
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateClient([FromBody] CreateClientRequest request)
    {
        var command = new CreateClient.Command(request.Cnpj, request.Name);
        
        var result = await _sender.Send(command);
        
        if (result.IsFailure)
            return BadRequest(result.Errors);
        
        return CreatedAtAction(nameof(GetClientById), new { id = result.Value }, result.Value);
    }

    [HttpPut("{id:guid}/rename")]
    public async Task<ActionResult> RenameClient (
        Guid id,
        [FromBody] RenameClientRequest request)
    {
        var command = new RenameClient.Command(id, request.Name);
        
        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            if (result.Errors.Code.Equals("Client.NotFound"))
                return NotFound(result.Errors);
            
            return BadRequest(result.Errors);
        }
        
        return NoContent();
    }
    
    [HttpPut("{id:guid}/activate")]
    public async Task<ActionResult> ActivateClient (Guid id)
    {
        var command = new ActivateClient.Command(id);
        
        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            if (result.Errors.Code.Equals("Client.NotFound"))
                return NotFound(result.Errors);
            
            return BadRequest(result.Errors);
        }
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteClientById(Guid id)
    {
        var command = new DeactivateClient.Command(id);
        var result = await _sender.Send(command);
        
        if (result.IsFailure)
        {
            if (result.Errors.Code.Equals("Client.NotFound"))
                return NotFound(result.Errors);
            
            return BadRequest(result.Errors);
        }
        
        return NoContent();
    }
}