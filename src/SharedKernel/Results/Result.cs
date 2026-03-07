namespace Shared.Results;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Errors { get; }

    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Errors = error;
    }

    // Factory Methods
    public static Result Success() => new (true, Error.None);
    public static Result Failure(Error error) => new (false, error);
    
    // Validation Method
    public static Result Validate(params Result[] results)
    {
        var error = results
            .Where(r => r.IsFailure)
            .Select(r => r.Errors)
            .FirstOrDefault();

        if (error != null)
            return  Failure(error);
        
        return Success();
    }
}

public class Result<T> : Result
{
    private readonly T? _value;
    public T Value => IsSuccess ? _value! : throw new InvalidOperationException("Cannot access the value of a failed result.");

    private Result(T value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }
    
    public static Result<T> Success(T value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        
        return new(value, true, Error.None);
    }
    public new static Result<T> Failure(Error error) => new(default!, false, error);
}