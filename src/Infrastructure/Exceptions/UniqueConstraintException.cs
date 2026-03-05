namespace Infrastructure.Exceptions;

public class UniqueConstraintException : Exception
{
    public UniqueConstraintException(string Key) : base(@"Unique constraint violation. Key: {key}")
    {
    }
}