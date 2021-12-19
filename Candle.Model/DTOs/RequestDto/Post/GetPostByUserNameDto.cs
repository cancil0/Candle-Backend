namespace Candle.Model.DTOs.RequestDto.Post
{
    public class GetPostByUserNameDto
    {
        public string UserName { get; set; }

        public string LoggedInUserName { get; set; }

        public int ScrollCount { get; set; }

        public int TakeCount { get; set; }
    }
}
