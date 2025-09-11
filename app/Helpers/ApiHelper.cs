using Newtonsoft.Json;
using NuGet.Protocol;
using scheapp.api.DataServices;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace scheapp.app.Helpers
{
    public class ApiHelper : IApiHelper
    {
        private HttpClient _scheAppApiClient;
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public ApiHelper(ILogger<ApiHelper> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _scheAppApiClient = httpClientFactory.CreateClient("ScheduleAppointmentApi");

        }
        public async Task<HttpResponseMessage> CallPostApi<T>(string endpointPath, T payload)
        {
            var json = JsonConvert.SerializeObject(payload);
            var postData = new StringContent(json, Encoding.UTF8, "application/json");
            return await _scheAppApiClient.PostAsync(endpointPath, postData);
        }
        public async Task<G_ResponseModelType> CallPostApi<G_RequestModelType, G_ResponseModelType>(string endpointPath, G_RequestModelType payload)
        {
            G_ResponseModelType responseModel = default;
            var json = JsonConvert.SerializeObject(payload);
            var postData = new StringContent(json, Encoding.UTF8, "application/json");

            var apiResponse = await _scheAppApiClient.PostAsync(endpointPath, postData);
            if (apiResponse != null && apiResponse.IsSuccessStatusCode)
            {
                string responseJson = await apiResponse.Content.ReadAsStringAsync();
                responseModel = JsonConvert.DeserializeObject<G_ResponseModelType>(responseJson);
            }
            return responseModel;
        }
        public async Task<T> CallGetApi<T>(string endpointPathWithParameters)
        {
            try
            {
                T responseModel = default;
                var apiResponse = await _scheAppApiClient.GetAsync(endpointPathWithParameters);
                if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                {
                    string responseJson = await apiResponse.Content.ReadAsStringAsync();
                    responseModel = JsonConvert.DeserializeObject<T>(responseJson);
                }
                else
                {
                    throw new Exception($"Get data failed for {endpointPathWithParameters} with status code of {apiResponse.StatusCode}. View api logs for details.");
                }
                return responseModel;
            }
            catch (Exception ex)
            {
                _logger.LogError("CallGetApi failed!");
                _logger.LogError("{@Exception}", ex);
                throw;
            }
        }

        public async Task<AddImageRS> UploadFile(string uploadPath,int businessId,string userType,int userId, byte[] fileBytes, string fileName, string contentType)
        {
            var response = new AddImageRS();
            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StringContent("BusinessId"), businessId.ToString());
                form.Add(new StringContent("UserType"), businessId.ToString());
                form.Add(new StringContent("UserTypeId"), businessId.ToString());
                form.Add(new StringContent("FileName"), businessId.ToString());
                var byteContentArray = new ByteArrayContent(fileBytes);
                byteContentArray.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType); //example image/png , image/jpg
                form.Add(byteContentArray, "Image", fileName);

                var fileUploadRS = await _scheAppApiClient.PostAsync(uploadPath, form);
                if (fileUploadRS != null && fileUploadRS.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string rsString = await fileUploadRS.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<AddImageRS>(rsString);
                    response.IsSuccess = true;
                }
                else
                {
                    string error = $"Upload file failed with response:{fileUploadRS.StatusCode}";
                    _logger.LogError(error);
                    response.Message = error;
                }
            }
            return response;
        }

        public async Task<GetImageRS> DownloadFile(string downloadPath, int businessId, string userType, int userId)
        {
            var response = new AddImageRS();
            var downloadRS = await _scheAppApiClient.GetAsync($"{downloadPath}?businessId={businessId}&userType={userType}&userId={userId}");
            string contentType = "";
            string fileName = "";
            Stream fileStream = null;
            if (downloadRS.Headers.TryGetValues("ContentType", out var contentTypeValues))
            {
                contentType = contentTypeValues.FirstOrDefault()!;
            }

            if (downloadRS.Headers.TryGetValues("FileName", out var fileNameValues))
            {
                fileName = fileNameValues.FirstOrDefault()!;
            }
            if (downloadRS != null && downloadRS.StatusCode == HttpStatusCode.OK)
            {
                fileStream = await downloadRS.Content.ReadAsStreamAsync().ConfigureAwait(false);
            }
            return new GetImageRS
            {
                FileName = fileName,
                FileStream = fileStream,
                ContentType = contentType,
            };
        }
    }
}
