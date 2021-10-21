namespace Candle.Common.Result
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message = "error") : base(false, message)
        {
        }
    }
}
