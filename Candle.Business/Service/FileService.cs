using Candle.Business.Abstract;
using Candle.Common.Result;
using Candle.Model.DTOs.ResponseDto.FileResponseDto;
using Candle.Model.Enums;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace Candle.Business.Service
{
    public class FileService : IFileService
    {
        private readonly IMediaService mediaService;
        public FileService()
        {
            mediaService = new MediaService();
        }

        public IDataResult<List<UploadFileResponseDto>> UploadFile(List<IFormFile> files, string userName)
        {
            List<UploadFileResponseDto> uploadFileResponse = new();

            if (files.Count == 0)
            {
                return new ErrorDataResult<List<UploadFileResponseDto>>(uploadFileResponse);
            }

            int index = mediaService.GetMediaMaxIndex(userName) > 0 ? mediaService.GetMediaMaxIndex(userName) : 0;
            index++;
            string fileExtention = string.Empty;
            string fileName = string.Empty;
            var filePath = string.Format("C:\\Projeler\\Candle\\Candle-Frontend\\src\\assets\\userMedias\\{0}", userName);
            var filePathFrontend = string.Format("../../../../assets/userMedias/{0}/", userName);
            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    switch (file.ContentType)
                    {
                        case "image/jpeg":
                            fileExtention = ".jpeg";
                            break;

                        case "image/png":
                            fileExtention = ".png";
                            break;

                        case "image/jpg":
                            fileExtention = ".jpg";
                            break;

                        case "video/mp4":
                            fileExtention = ".mp4";
                            break;

                        default:
                            break;
                    }

                    fileName = index.ToString() + fileExtention;
                    using var stream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create);
                    file.CopyTo(stream);

                    uploadFileResponse.Add(
                        new UploadFileResponseDto() 
                        { 
                            FileName = fileName,
                            Caption = filePathFrontend + fileName,
                            FileSize = file.Length,
                            MediaType = (short)(file.ContentType.Substring(0,5).Contains("image") ? MediaTypes.Photo : MediaTypes.Video),
                            Index = index
                        });

                    index++;
                }
            }

            return new SuccessDataResult<List<UploadFileResponseDto>>(uploadFileResponse);
        }
    }
}
