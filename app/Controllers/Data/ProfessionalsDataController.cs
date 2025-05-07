using Microsoft.AspNetCore.Mvc;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Models.View;
using scheapp.app.Models.Data.TableModels.Professionals;
using scheapp.app.Models.API;
using Newtonsoft.Json;

namespace scheapp.app.Controllers.Data
{
    [Route("[controller]/[Action]")]
    [ApiController]
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
                        var professionalSchedules = await _professionalsDataService.GetProfessionalSchedulesByBusinessId(verifiedBusinessProfessional.BusinessId.GetValueOrDefault());

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

        [HttpPost]
        public async Task<IActionResult> GetProfessionalsByBusinessId([FromBody] RequestByBusinessId professionalInfoRequest)
        {
            try
            {
                var professionals = ( await _professionalsDataService.GetProfessionals() ).Where(p=>p.BusinessId == professionalInfoRequest.BusinessId);
                return Ok();
            }
            catch (Exception ex)
            {
                //_logger.LogError("{@Exception}", ex);
                return StatusCode(500, "Error Occured.");
            }
        }

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
                await _professionalsDataService.SaveProfessionalSchedules(scheappApiRQ);
                return Ok();
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
                // Before deleting, warn user of list of appointments that will be soft deleted, and if confirmed, another api call
                // to delete the appointments that refs this schedule, and soft delete the shedule.
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
