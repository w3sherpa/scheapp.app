﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Helpers;
using scheapp.app.Models.Data;
using scheapp.app.Models.Data.DspModels;
using scheapp.app.Models.View;

namespace scheapp.app.Controllers.Data
{
    [Route("[controller]/[Action]")]
    [ApiController]
    [Authorize(Roles = "business_admin,scheapp_admin")]
    public class AdminDataController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IProfessionalDataService _professionalDataService;
        
        public AdminDataController(
            ILogger<AdminDataController> logger
            , IProfessionalDataService professionalDataService
            , IBusinessDataService businessDataService
            )
        {
            _logger = logger;
            _professionalDataService = professionalDataService;
        }

        [HttpGet]
        public IActionResult GetMessage(string name)
        {
            return Ok($"CommunicationController: Namaste {name}!");
        }
        [HttpPost]
        public async Task<IActionResult> GetProfessionalScheduleAppointmentRequestsDetails([FromBody]BusinessAppointmentRQ requestByBusinesId)
        {
            try
            {
                var scheduledAppoitments = await _professionalDataService.GetProfessionalScheduleAppointmentRequestsDetailsByBusinessId(2,null);

                List<ProfessionalScheduleAppointmentVM> prosche = scheduledAppoitments.Select(s => new ProfessionalScheduleAppointmentVM 
                                                                                                        { StartDT = s.StartDT
                                                                                                        , EndDT = s.EndDT 
                                                                                                        , CustomerConfirmed = s.CustomerConfirmed
                                                                                                        , ProfessionalConfirmed = s.ProfessionalConfirmed
                                                                                                        , RequestDate = s.RequestDate
                                                                                                        , ServiceName = s.ServiceName
                                                                                                        , ScheduleAppointmentId = s.ScheduleAppointmentId
                                                                                                        , Customer = $"{s.CustFrist} {s.CustLast}"
                                                                                                        , Professional = $"{s.ProFirst} {s.ProLast}"
                }).ToList();

                return Ok(new AppointmentListRS { RecordsCount = scheduledAppoitments.Count, Records = prosche });
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return StatusCode(500, "Error Occured.");
            }
        }

    }
    public class AppointmentListRS
    {
        public int RecordsCount { get; set; }
        public List<ProfessionalScheduleAppointmentVM> Records { get; set; } = new ();
    }
    public class BusinessAppointmentRQ
    {
        public int BusinessId { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; } 

    }
}
