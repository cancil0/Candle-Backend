namespace Candle.Model.DTOs.ResponseDto.FileResponseDto
{
    public class UploadFileResponseDto
    {
        public string Caption { get; set; }

        public string FileName { get; set; }

        public long FileSize { get; set; }

        public short MediaType { get; set; }

        public int Index { get; set; }
    }
}
