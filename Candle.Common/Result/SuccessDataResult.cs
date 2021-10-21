namespace Candle.Common.Result
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, string message = "success") : base(data, true, message)
        {
        }
        public SuccessDataResult(string message = "success") : base(default, true, message)
        {
        }
    }
}
