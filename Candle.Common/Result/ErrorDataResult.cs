namespace Candle.Common.Result
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, string message = "error") : base(data, false, message)
        {
        }
        public ErrorDataResult(string message = "error") : base(default, false, message)
        {
        }
    }
}
