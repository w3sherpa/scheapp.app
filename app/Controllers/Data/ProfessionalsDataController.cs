﻿using Microsoft.AspNetCore.Mvc;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Models.View;
using scheapp.app.Models.Data.TableModels.Professionals;
using scheapp.app.Models.API;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace scheapp.app.Controllers.Data
{
    [Route("[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class ProfessionalsDataController : ControllerBase
    {
        private readonly ILogger<ProfessionalsDataController> _logger;
        private readonly IProfessionalDataService _professionalsDataService;

        public ProfessionalsDataController(
            ILogger<ProfessionalsDataController> logger
            , IProfessionalDataService professionalsDataService)
        {
            _logger = logger;
            _professionalsDataService = professionalsDataService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProfessionals(int? businessId)
        {
            try
            {
                var verifiedBusinessProfessional = await CommonControllerUtility.GetLoggedInProfessionalBusinessDetails(_professionalsDataService, User.Identity.Name, businessId);
                if (verifiedBusinessProfessional != null)
                {
                    if (verifiedBusinessProfessional.BusinessId != null)
                    {
                        var professionals = await _professionalsDataService.GetProfessionals(verifiedBusinessProfessional.BusinessId.GetValueOrDefault());
                        return Ok(professionals);
                    }
                    else
                    {
                        return NotFound("PROFSSIONAL NEEDS BUSINESS ID. PLEASE CONACT BUSINESS ADMIN TO ADD YOU TO THE BUSINESS.");
                    }
                }
                else
                {
                    return StatusCode(404, "Business Professional Not Verified");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return StatusCode(500, "Error Occured.");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetSchedules(int? businessId, int? professionalId)
        {
            try
            {
                var verifiedBusinessProfessional = await CommonControllerUtility.GetLoggedInProfessionalBusinessDetails(_professionalsDataService, User.Identity.Name, businessId);
                //if id is still null that mean either user is scheapp admin who passed no id param or business admin who's permission is not set.
                if (verifiedBusinessProfessional != null)
                {
                    if (verifiedBusinessProfessional.BusinessId != null)
                    {
                        var professionalSchedules = await _professionalsDataService.GetProfessionalSchedules(verifiedBusinessProfessional.BusinessId.GetValueOrDefault(), professionalId);

                        ScheAppDataGrid scheAppDataGrid = new ScheAppDataGrid();
                        scheAppDataGrid.Total = professionalSchedules.Count;
                        scheAppDataGrid.TotalNotFiltered = professionalSchedules.Count;
                        scheAppDataGrid.Rows = professionalSchedules;
                        return Ok(scheAppDataGrid);
                    }
                    else
                    {
                        return NotFound("PROFSSIONAL NEEDS BUSINESS ID. PLEASE CONACT BUSINESS ADMIN TO ADD YOU TO THE BUSINESS.");
                    }
                }
                else
                {
                    return StatusCode(404, "Business Professional Not Verified");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return StatusCode(500, "Error Occured.");
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> GetProfessionalsByBusinessId([FromBody] RequestByBusinessId professionalInfoRequest)
        //{
        //    try
        //    {
        //        var professionals = ( await _professionalsDataService.GetProfessionals() ).Where(p=>p.BusinessId == professionalInfoRequest.BusinessId);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        //_logger.LogError("{@Exception}", ex);
        //        return StatusCode(500, "Error Occured.");
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> GetProfessionalsByDate([FromBody] RequestByDate businessInfoRQ)
        {
            try
            {

                return Ok();
            }
            catch (Exception ex)
            {
                //_logger.LogError("{@Exception}", ex);
                return StatusCode(500, "Error Occured.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetProfessionalsByServiceId([FromBody] RequestByServiceId businessInfoRQ)
        {
            try
            {

                return Ok();
            }
            catch (Exception ex)
            {
                //_logger.LogError("{@Exception}", ex);
                return StatusCode(500, "Error Occured.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetServicesByProfessionalId([FromBody] RequestByProfessionalId businessInfoRQ)
        {
            try
            {

                return Ok();
            }
            catch (Exception ex)
            {
                //_logger.LogError("{@Exception}", ex);
                return StatusCode(500, "Error Occured.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveProfessionalSchedules([FromBody] SaveProfessionalScheduleRQ req)
        {
            try
            {
                List<string> startDateParts = req.StartDateTime.Split(" ").ToList();
                List<string> endDateParts = req.EndDateTime.Split(" ").ToList();
                ProfessionalSchedule scheappApiRQ = new ProfessionalSchedule();
                scheappApiRQ.ProfessionalId = req.ProfessionalId;
                scheappApiRQ.BusinessId = req.BusinessId;
                scheappApiRQ.DaysOfWeek = req.DaysOfWeek;
                scheappApiRQ.StartDate = DateOnly.Parse(startDateParts[0]);
                scheappApiRQ.StartTime = startDateParts[1];
                scheappApiRQ.EndDate = DateOnly.Parse(endDateParts[0]);
                scheappApiRQ.EndTime = endDateParts[1];
                HttpResponseMessage httpResponseMessage = await _professionalsDataService.SaveProfessionalSchedules(scheappApiRQ);
                return Ok( new GenericApiResponse { Status = (int)httpResponseMessage.StatusCode, Message = httpResponseMessage.ReasonPhrase});
            }
            catch (Exception ex)
            {
                //_logger.LogError("{@Exception}", ex);
                return StatusCode(500, "Error Occured.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProfessionalSchedules([FromBody] DeleteProfessionalScheduleRQ req)
        {
            try
            {
                var apiResponse = await _professionalsDataService.DeleteProfessionalSchedules(req);
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
