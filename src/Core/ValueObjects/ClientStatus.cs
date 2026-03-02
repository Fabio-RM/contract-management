using Core.Common;

namespace Core.ValueObjects;

public sealed class ClientStatus : Enumeration
{
    public static readonly ClientStatus Active = new ClientStatus(1, nameof(Active));
    public static readonly ClientStatus Inactive = new ClientStatus(2, nameof(Inactive));
    
    private ClientStatus(int id, string displayName) : base(id, displayName)
    {
    }
}