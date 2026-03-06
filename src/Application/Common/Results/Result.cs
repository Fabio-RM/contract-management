using MediatR;

namespace Application.Common.Results;

public class Result<TValue>
{
    public TValue? Value  { get; private set; }
    public bool IsSuccess { get; private set; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; private set; }

    private Result(bool isSuccess, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
        Value = default;
    }

    private Result(TValue value, bool isSuccess, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
        Value = value;
    }
    
    public static Result<TValue?> Success() => new (true, string.Empty);
    public static Result<TValue> Success(TValue value) => new (value, true, string.Empty);
    public static Result<TValue?> Failure(string error) => new (false, error);
}