using Newtonsoft.Json;
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
    }
}
