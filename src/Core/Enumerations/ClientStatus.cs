using Core.Common;

namespace Core.Enumerations;

public class ClientStatus : Enumeration
{
    public static readonly ClientStatus Active = new ClientStatus(1, nameof(Active));
    public static readonly ClientStatus Inactive = new ClientStatus(2, nameof(Inactive));
    
    public ClientStatus(int id, string name) : base(id, name)
    {
    }
}