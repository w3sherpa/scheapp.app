﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Helpers;
using scheapp.app.Models.Data.Professionals;

namespace scheapp.app.Controllers.Data
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProfessionalsDataController : ControllerBase
    {
        //private readonly ILogger<ProfessionalsDataController> _logger;
        private readonly IHubContext<ScheAppViewUpdateHub> _signalRScheAppHub;
        private readonly IProfessionalDataService _professionalsDataService;

        public ProfessionalsDataController(
            //Logger<ProfessionalsDataController> logger
             IHubContext<ScheAppViewUpdateHub> signalRScheAppHub
            , IProfessionalDataService professionalsDataService)
        {
            //_logger = logger;
            _signalRScheAppHub = signalRScheAppHub;
            _professionalsDataService = professionalsDataService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveProfessionalScheduleAppointmentRequests([FromBody] ProfessionalScheduleAppointmentRequest professionalScheduleAppointmentRequest )
        {
            try
            {
                var res = _professionalsDataService.GetProfessionalScheduleAppointmentRequests();
                await _signalRScheAppHub.Clients.All.SendAsync("UpdateAppointmentsView", "padat", "12132");
                return Ok();
            }
            catch (Exception ex)
            {
                //_logger.LogError("{@Exception}", ex);
                return StatusCode(500, "Error Occured.");
            }
        }
    }
}
