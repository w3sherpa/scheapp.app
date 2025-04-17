using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Helpers;
using scheapp.app.Models.Data;
using System.Data.Common;
using System.Data;
using System.Text;
using System.Web;
using scheapp.app.Models.Data.DspModels;

namespace scheapp.app.Controllers.Data
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class zzzscheapp4b23a66cac6308c0f97f7167e9551b51Controller : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICommunicationDataService _communicationDataService;
        private readonly IContactsDataService _contactsDataService;
        private readonly IHubContext<ScheAppViewUpdateHub> _signalRScheAppHub;
        private readonly IConfiguration _configuration;
        public zzzscheapp4b23a66cac6308c0f97f7167e9551b51Controller(
            ILogger<zzzscheapp4b23a66cac6308c0f97f7167e9551b51Controller> logger
            , ICommunicationDataService communicationDataService
            , IContactsDataService contactsDataService
            , IHubContext<ScheAppViewUpdateHub> signalRScheAppHub
,
IConfiguration configuration

            )
        {
            _logger = logger;
            _signalRScheAppHub = signalRScheAppHub;
            _communicationDataService = communicationDataService;
            _contactsDataService = contactsDataService;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult GetMessage()
        {
            //call st api to save to db
            return Ok("Hello from VonageWebHookController");
        }

        [HttpPost]
        public IActionResult LogVonageVoiceCallEvent([FromBody] VonageVoiceApiEvent vonageVoiceCallEvent)
        {
            _logger.LogWarning("{@VonageVoiceApiEvent}", vonageVoiceCallEvent);
            return Ok();
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        //public async Task<HttpResponseMessage> StatusCallback([FromForm] IFormCollection value)
        public async Task<HttpResponseMessage> TwilioStatusCallback()
        {
            string body = await new StreamReader(Request.Body).ReadToEndAsync();
            var requestString = HttpUtility.UrlDecode(body);
            List<string> paramsSent = requestString.Split('&').ToList();
            Dictionary<string, string> statusCallbackKeyValues = new();
            foreach (var param in paramsSent)
            {
                var breaParamKV = param.Split('=');
                statusCallbackKeyValues.Add(breaParamKV[0], breaParamKV[1]);
            }
            if (statusCallbackKeyValues["CallStatus"]== "completed") _logger.LogWarning($"{JsonConvert.SerializeObject(statusCallbackKeyValues)}");

            

            ////Use following code to log call status. The API is not implemented yet
            ////if (statusCallbackKeyValues.Count > 0)
            ////{
            ////    var statusCallBackData = JsonConvert.DeserializeObject<TwilioStatusCallback>(JsonConvert.SerializeObject(statusCallbackKeyValues));
            ////    await _communicationDataService.UpdateTwilioCallStatus(statusCallBackData);
            ////}

            //twilio expected format of response
            //https://www.twilio.com/docs/messaging/twiml#twilios-request-to-your-application
            string XML = "<Response/>";
            return new HttpResponseMessage()
            {
                Content = new StringContent(XML, Encoding.UTF8, "application/xml")
            };

        }

        [HttpGet]
        [Consumes("application/x-www-form-urlencoded")]
        //public async Task<HttpResponseMessage> StatusCallback([FromForm] IFormCollection value)
        public async Task<IActionResult> TwilioDtmfCallback(string Digits, string CallSid)
        {
            try
            {
                ////Time out url only gets called if dtmf time out occur in twilio
                if (Digits == "TIMEOUT")
                {
                    _logger.LogWarning($"dmtf time out occured for {CallSid}");
                }
                else
                {
                    if(Digits == "1")
                    {
                        await _contactsDataService.CustomerConfirmScheduleAppointment(new Models.API.CustomerConfirmationRQ { VoiceApiConversationId = CallSid, CustomerConfirmed = true });
                        ProfessionalScheduleAppointmentRequestsDetailDsp scheduleDetail = new();
                        List<string> businessAdminUsers = new List<string>();
                        using (SqlConnection srcConn = new SqlConnection(_configuration.GetConnectionString("ScheAppMin")))
                        using (SqlCommand srcCmd = new SqlCommand($"exec dsp_GetProfessionalUserIdByConversationId '{CallSid}'", srcConn))
                        {
                            srcConn.Open();
                            using (DbDataReader reader = srcCmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    businessAdminUsers.Add(reader.GetString(0));
                                }
                                if (reader.NextResult())
                                {
                                    reader.Read();
                                    scheduleDetail.StartDT = DateTime.Parse(reader["StartDT"].ToString());
                                    scheduleDetail.EndDT = DateTime.Parse(reader["EndDT"].ToString());
                                    scheduleDetail.RequestDate = DateTime.Parse(reader["RequestDate"].ToString());
                                    scheduleDetail.ProFirst = reader["ProFirst"].ToString();
                                    scheduleDetail.ProMiddle = reader["ProMiddle"].ToString();
                                    scheduleDetail.ProLast = reader["ProLast"].ToString();
                                    scheduleDetail.CustFrist = reader["CustFrist"].ToString();
                                    scheduleDetail.CustLast = reader["CustLast"].ToString();
                                    scheduleDetail.CustMiddle = reader["CustMiddle"].ToString();
                                }
                                
                            }
                        }
                        foreach (var bu in businessAdminUsers)
                        {
                            await _signalRScheAppHub.Clients.Group(bu).SendAsync("UpdateAppointmentsView", "salemvai", JsonConvert.SerializeObject(scheduleDetail));
                        }
                        
                    }
                    _logger.LogWarning($"dtmf digits pressed are: {Digits} and call sid is {CallSid}");
                }
                OkObjectResult result = Ok(new Response());
                result.Formatters.Clear();
                result.Formatters.Add(new Microsoft.AspNetCore.Mvc.Formatters.XmlSerializerOutputFormatter());
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return Ok("error occured");
            }
        }
    }
    public class Response { }
    public class VonageVoiceApiEvent
    {
        public string start_time { get; set; } = string.Empty;
        public string end_time { get; set; } = string.Empty;
        public string duration { get; set; } = string.Empty;
        public string rate { get; set; } = string.Empty;
        public string price { get; set; } = string.Empty;
        public string from { get; set; } = string.Empty;
        public string to { get; set; } = string.Empty;
        public string uuid { get; set; } = string.Empty;
        public string conversation_uuid { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public string detail { get; set; } = string.Empty;
        public string direction { get; set; } = string.Empty;
        public string network { get; set; } = string.Empty;
        public string timestamp { get; set; } = string.Empty;
    }
}
