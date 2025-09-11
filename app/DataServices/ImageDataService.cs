using Microsoft.EntityFrameworkCore;
using scheapp.app.Helpers;
using scheapp.data.Db.DbContexts;
using scheapp.data.Db.TableModels.Images;
using System;

namespace scheapp.api.DataServices
{
    public interface IImageDataService
    {
        Task<GetImageRS?> GetCustomerImageAsync(int businessId, int id);
        Task<AddImageRS> AddCustomerImageAsync(int businessId, CustomerImageEntity image);
        Task<GetImageRS?> GetProfessionalImageAsync(int businessId, int id);
        Task<AddImageRS> AddProfessionalImageAsync(int businessId, ProfessionalImageEntity image);
    }

    public class ImageDataService : IImageDataService
    {

        private readonly IApiHelper _apiHelper;

        public ImageDataService(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<GetImageRS?> GetCustomerImageAsync(int businessId, int id)
        {
            return await _apiHelper.CallGetApi<GetImageRS>("/Image/Download");
        }
        public async Task<AddImageRS> AddCustomerImageAsync(int businessId, CustomerImageEntity image)
        {
            return await _apiHelper.CallGetApi<AddImageRS>("/Image/Upload");
        }
        public async Task<GetImageRS?> GetProfessionalImageAsync(int businessId, int id)
        {
            return await _apiHelper.CallGetApi<GetImageRS>("/Image/Download");
        }
        public async Task<AddImageRS> AddProfessionalImageAsync(int businessId, ProfessionalImageEntity image)
        {
            return await _apiHelper.CallGetApi<AddImageRS>("/Image/Upload");
        }
    }
    public class UploadImageRQ
    {
        public int BusinessId { get; set; }
        public string UserType { get; set; }
        public int UserTypeId { get; set; }
        public string FileName { get; set; }
        public IFormFile Image { get; set; }
    }
    public class GetImageRS
    {
        public Stream? FileStream { get; set; }
        public string ContentType { get; set; } = "";
        public string FileName { get; set; } = "";
    }
    public class AddImageRS
    {
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; }
    }
}
