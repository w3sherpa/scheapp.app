using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Models.Data;
using System.Text;
using System.Web;

namespace scheapp.app.Controllers.Data
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class zzzscheapp4b23a66cac6308c0f97f7167e9551b51Controller : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICommunicationDataService _communicationDataService;
        public zzzscheapp4b23a66cac6308c0f97f7167e9551b51Controller(
            ILogger<zzzscheapp4b23a66cac6308c0f97f7167e9551b51Controller> logger
            , ICommunicationDataService communicationDataService
            )
        {
            _logger = logger;
            _communicationDataService = communicationDataService;
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
            _logger.LogWarning($"{JsonConvert.SerializeObject(statusCallbackKeyValues)}");

            if (statusCallbackKeyValues.Count > 0)
            {
                var statusCallBackData = JsonConvert.DeserializeObject<TwilioStatusCallback>(JsonConvert.SerializeObject(statusCallbackKeyValues));
                await _communicationDataService.UpdateTwilioCallStatus(statusCallBackData);
            }

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
                ////Timeout magic string is set to the Digits value in TwiML in Robocall Project
                ////Time out url only gets called if dtmf time out occur in twilio
                if (Digits == "TIMEOUT")
                {
                    _logger.LogWarning($"dmtf time out occured for {CallSid}");
                }
                else
                {
                    _logger.LogWarning($"dtmf digits pressed are: {Digits} and call sid is {CallSid}");
                }
                OkObjectResult result = Ok(new TwilioXmlresponse());
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
    public class TwilioXmlresponse { }
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
