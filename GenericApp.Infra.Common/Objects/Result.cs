namespace Wis.Common.Objects
{
    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public Result()
        {
            Success = false;
            Message = string.Empty;
        }

        public Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public Result(bool success) : this(success, null)
        { }

        public static Result Successfull(string message)
        {
            return new Result(true, message);
        }

        public static Result Successfull()
        {
            return new Result(true, null);
        }

        public static Result Failed(string message)
        {
            return new Result(false, message);
        }

        public static Result Failed()
        {
            return new Result(false, null);
        }
    }

    public class Result<T> : Result
    {
        public T Data { get; }

        public Result(bool success, string message) : base(success, message)
        { }

        public Result(bool success) : base(success, null)
        { }

        public Result(bool success, T data, string message) : base(success, message)
        {
            Data = data;
        }

        public Result(bool success, T data) : this(success, data, null)
        { }

        public static Result<T> Successfull(T data)
        {
            return new Result<T>(true, data);
        }

        public static new Result<T> Failed()
        {
            return new Result<T>(false, default(T));
        }

        public static new Result<T> Failed(string message)
        {
            return new Result<T>(false, default, message);
        }
    }
}