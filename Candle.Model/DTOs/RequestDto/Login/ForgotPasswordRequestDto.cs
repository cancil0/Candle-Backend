namespace Candle.Model.DTOs.RequestDto.Login
{
    public class ForgotPasswordRequestDto
    {
        public string Email { get; set; }

        public string MobilePhone { get; set; }

        public string UserName { get; set; }

        public GeneratePinFileResourcesDto FileResources { get; set; }
    }
}
