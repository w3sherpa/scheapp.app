using Microsoft.AspNetCore.Mvc;

namespace scheapp.app.Controllers.Data
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class zzzscheapp4b23a66cac6308c0f97f7167e9551b51Controller : ControllerBase
    {
        private readonly ILogger _logger;
        public zzzscheapp4b23a66cac6308c0f97f7167e9551b51Controller(ILogger<zzzscheapp4b23a66cac6308c0f97f7167e9551b51Controller> logger)
        {
            _logger = logger;
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
    }
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
