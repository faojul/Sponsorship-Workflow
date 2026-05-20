namespace Sponsorship.Application.Common.Results
{
    public class Result : IResult
    {
        public Result()
        {
        }

        public List<string> Messages { get; set; } = new();

        public bool Succeeded { get; set; }
        public int StatusCode { get; set; }

        public static IResult Fail(int statusCode)
        {
            return new Result { Succeeded = false, StatusCode = statusCode };
        }

        public static IResult Fail(int statusCode, string message)
        {
            return new Result { Succeeded = false, Messages = new List<string> { message }, StatusCode = statusCode };
        }

        public static IResult Fail(int statusCode, List<string> messages)
        {
            return new Result { Succeeded = false, Messages = messages, StatusCode = statusCode };
        }

        public static Task<IResult> FailAsync(int statusCode)
        {
            return Task.FromResult(Fail(statusCode));
        }

        public static Task<IResult> FailAsync(int statusCode, string message)
        {
            return Task.FromResult(Fail(statusCode, message));
        }

        public static Task<IResult> FailAsync(int statusCode, List<string> messages)
        {
            return Task.FromResult(Fail(statusCode, messages));
        }

        public static IResult Success(int statusCode)
        {
            return new Result { Succeeded = true, StatusCode = statusCode };
        }

        public static IResult Success(int statusCode, string message)
        {
            return new Result { Succeeded = true, Messages = new List<string> { message }, StatusCode = statusCode };
        }

        public static IResult Success(int statusCode, List<string> messages)
        {
            return new Result { Succeeded = true, Messages = messages, StatusCode = statusCode };
        }

        public static Task<IResult> SuccessAsync(int statusCode)
        {
            return Task.FromResult(Success(statusCode));
        }

        public static Task<IResult> SuccessAsync(int statusCode, string message)
        {
            return Task.FromResult(Success(statusCode, message));
        }

        public static Task<IResult> SuccessAsync(int statusCode, List<string> messages)
        {
            return Task.FromResult(Success(statusCode, messages));
        }
    }

    public class ErrorResult<T> : Result<T>
    {
        public string? Source { get; set; }

        public string? Exception { get; set; }

        public int ErrorCode { get; set; }
    }

    public class Result<T> : Result, IResult<T>
    {
        public Result()
        {
        }

        public T? Data { get; set; }

        public new static Result<T> Fail(int statusCode)
        {
            return new() { Succeeded = false, StatusCode = statusCode };
        }

        public new static Result<T> Fail(int statusCode, string message)
        {
            return new() { Succeeded = false, Messages = new List<string> { message }, StatusCode = statusCode };
        }
        public new static Result<T> Fail(int statusCode, List<string> messages)
        {
            return new() { Succeeded = false, Messages = messages, StatusCode = statusCode };
        }
        public static ErrorResult<T> ReturnError(string message)
        {
            return new() { Succeeded = false, Messages = new List<string> { message }, ErrorCode = 500 };
        }

        public static ErrorResult<T> ReturnError(List<string> messages)
        {
            return new() { Succeeded = false, Messages = messages, ErrorCode = 500 };
        }

        public new static Task<Result<T>> FailAsync(int statusCode)
        {
            return Task.FromResult(Fail(statusCode));
        }

        public new static Task<Result<T>> FailAsync(int statusCode, string message)
        {
            return Task.FromResult(Fail(statusCode, message));
        }

        public new static Task<Result<T>> FailAsync(int statusCode, List<string> messages)
        {
            return Task.FromResult(Fail(statusCode, messages));
        }
        public static Task<ErrorResult<T>> ReturnErrorAsync(string message)
        {
            return Task.FromResult(ReturnError(message));
        }
        public static Task<ErrorResult<T>> ReturnErrorAsync(List<string> messages)
        {
            return Task.FromResult(ReturnError(messages));
        }

        public new static Result<T> Success(int statusCode)
        {
            return new() { Succeeded = true, StatusCode = statusCode };
        }

        public new static Result<T> Success(int statusCode, string message)
        {
            return new() { Succeeded = true, Messages = new List<string> { message }, StatusCode = statusCode };
        }

        public new static Result<T> Success(int statusCode, List<string> messages)
        {
            return new() { Succeeded = true, Messages = messages, StatusCode = statusCode };
        }

        public static Result<T> Success(int statusCode, T data)
        {
            return new() { Succeeded = true, Data = data, StatusCode = statusCode };
        }

        public static Result<T> Success(int statusCode, T data, string message)
        {
            return new() { Succeeded = true, Data = data, Messages = new List<string> { message }, StatusCode = statusCode };
        }

        public static Result<T> Success(int statusCode, T data, List<string> messages)
        {
            return new() { Succeeded = true, Data = data, Messages = messages, StatusCode = statusCode };
        }

        public new static Task<Result<T>> SuccessAsync(int statusCode)
        {
            return Task.FromResult(Success(statusCode));
        }

        public new static Task<Result<T>> SuccessAsync(int statusCode, string message)
        {
            return Task.FromResult(Success(statusCode, message));
        }

        public new static Task<Result<T>> SuccessAsync(int statusCode, List<string> messages)
        {
            return Task.FromResult(Success(statusCode, messages));
        }

        public static Task<Result<T>> SuccessAsync(int statusCode, T data)
        {
            return Task.FromResult(Success(statusCode, data));
        }

        public static Task<Result<T>> SuccessAsync(int statusCode, T data, string message)
        {
            return Task.FromResult(Success(statusCode, data, message));
        }

        public static Task<Result<T>> SuccessAsync(int statusCode, T data, List<string> messages)
        {
            return Task.FromResult(Success(statusCode, data, messages));
        }
    }
}
