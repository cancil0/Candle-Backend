using Candle.Application.System;
using Candle.Business.Abstract;
using Candle.Common.Result;
using Candle.Model.DTOs.ResponseDto.FileResponseDto;
using Candle.Model.Enums;
using Candle.Model.Enums.EnumExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace Candle.Business.Service
{
    public class FileService : IFileService
    {
        private readonly IMediaService mediaService;
        private readonly IUserService userService;
        private readonly IConfiguration configuration;
        private readonly IConfigurationSection photoFilePath;
        public FileService(IConfiguration configuration)
        {
            mediaService = new MediaService();
            userService = new UserService();
            this.configuration = configuration;
            this.photoFilePath = configuration.GetSection("FilePath");
        }

        public IDataResult<List<UploadFileResponseDto>> UploadFile(List<IFormFile> files, string userName)
        {
            if (files.Count == 0)
            {
                return new ErrorDataResult<List<UploadFileResponseDto>>();
            }

            List<UploadFileResponseDto> uploadFileResponse = new();
            int index = mediaService.GetMediaMaxIndex(userName);
            index++;
            string fileExtention = string.Empty;
            string fileName = string.Empty;
            var filePath = string.Format("{0}{1}", photoFilePath.GetSection("postPhotoPathProd").Value, userName);
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
                            MediaType = file.ContentType[..5].Contains("image") ? MediaTypes.Photo.GetValue().ToShort() : MediaTypes.Video.GetValue().ToShort(),
                            Index = index
                        });

                    index++;
                }
            }

            return new SuccessDataResult<List<UploadFileResponseDto>>(uploadFileResponse);
        }

        public Common.Result.IResult UploadProfilePhoto(IFormFile file, string userName)
        {
            if (string.IsNullOrEmpty(file.FileName))
            {
                return new ErrorResult();
            }

            string fileExtention = string.Empty;
            
            var filePath = string.Format("{0}{1}", photoFilePath.GetSection("postPhotoPathProd").Value, userName);

            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

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

                default:
                    break;
            }
            var filePathFrontend = string.Format("../../../../assets/userMedias/{0}/{0}-profilephoto{1}", userName, fileExtention);
            string fileName = string.Format("{0}-profilephoto{1}", userName, fileExtention);
            using var stream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create);
            file.CopyTo(stream);
            userService.UpdateProfilePhotoPath(userName, filePathFrontend);
            return new SuccessResult();
        }
    }
}
