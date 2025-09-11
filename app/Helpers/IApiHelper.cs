using scheapp.api.DataServices;

namespace scheapp.app.Helpers
{
    public interface IApiHelper
    {
        Task<T> CallGetApi<T>(string endpointPathWithParameters);
        Task<HttpResponseMessage> CallPostApi<T>(string endpointPath, T payload);
        Task<G_ResponseModelType> CallPostApi<G_RequestModelType, G_ResponseModelType>(string endpointPath, G_RequestModelType payload);
        Task<AddImageRS> UploadFile(string uploadPath, int businessId, string userType, int userId, byte[] fileBytes, string fileName, string contentType);
    }
}