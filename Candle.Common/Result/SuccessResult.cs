namespace Candle.Common.Result
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message="success") : base(true, message)
        {
        }
    }
}
