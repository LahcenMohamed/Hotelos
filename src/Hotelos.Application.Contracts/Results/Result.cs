using System.Net;

namespace Hotelos.Application.Contracts.Results
{
    public sealed class Result<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string? Error { get; set; }
        public T? Data { get; set; }

        internal Result()
        {

        }


    }

    public static class Result
    {
        public static Result<T> Success<T>(T data)
        {
            return new Result<T>()
            {
                Data = data,
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = "Successfully",
                Error = null
            };
        }
        public static Result<T> Created<T>(T data)
        {
            return new Result<T>()
            {
                Data = data,
                StatusCode = HttpStatusCode.Created,
                Succeeded = true,
                Message = "Created Successfully",
                Error = null
            };
        }
        public static Result<T> Deleted<T>()
        {
            return new Result<T>()
            {
                Data = default(T),
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = "Deleted Successfully",
                Error = null
            };
        }
        public static Result<T> Unauthorized<T>(string? error)
        {
            return new Result<T>()
            {
                Data = default(T),
                StatusCode = HttpStatusCode.Unauthorized,
                Succeeded = false,
                Message = "Unauthorized",
                Error = error
            };
        }
        public static Result<T> BadRequest<T>(string? error)
        {
            return new Result<T>()
            {
                Data = default(T),
                StatusCode = HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = "there is some problems",
                Error = error
            };
        }
        public static Result<T> UnprocessableEntity<T>(string? error)
        {
            return new Result<T>()
            {
                Data = default(T),
                StatusCode = HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = "UnprocessableEntity",
                Error = error
            };
        }
        public static Result<T> NotFound<T>(string? error)
        {
            return new Result<T>()
            {
                Data = default(T),
                StatusCode = HttpStatusCode.NotFound,
                Succeeded = false,
                Message = "NotFound",
                Error = error
            };
        }

        public static Result<T> Custom<T>(T? data, HttpStatusCode statusCode, bool succeeded, string message, string? error)
        {
            return new Result<T>()
            {
                Data = data,
                StatusCode = statusCode,
                Succeeded = succeeded,
                Message = message,
                Error = error
            };
        }
    }
}
