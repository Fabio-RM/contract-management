namespace Shared.Results;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }

    protected Result(bool isSuccess, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new Result(true, string.Empty);
    public static Result Failure(string error) => new Result(false, error);
}

public class Result<T> : Result
{
    private readonly T _value;
    public T Value => IsSuccess 
        ? _value 
        : throw new InvalidOperationException("Não é possível acessar o valor de uma falha.");

    private Result(T value, bool isSuccess, string error) 
        : base(isSuccess, error)
    {
        _value = value;
    }

    public static Result<T> Success(T value) => new Result<T>(value, true, string.Empty);
    public new static Result<T> Failure(string error) => new Result<T>(default!, false, error);
}