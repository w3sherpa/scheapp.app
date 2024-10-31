using Microsoft.AspNetCore.Mvc;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Models.Data.DspModels;

namespace scheapp.app.Controllers.View
{
    public class ProfessionalsController : Controller
    {
        private readonly ILogger _logger;
        private readonly IProfessionalDataService _professionalDataService;
        public ProfessionalsController(ILogger<ProfessionalsController> logger, IProfessionalDataService professionalDataService)
        {
            _logger = logger;
            _professionalDataService = professionalDataService;
        }
        public async Task<IActionResult> Schedules(int? businessId,int? professionalId)
        {

            try
            {
                var verifiedBusinessProfessional = await CommonUtility.GetLoggedInProfessionalBusinessDetails(_professionalDataService, User.Identity.Name, businessId);
                //if id is still null that mean either user is scheapp admin who passed no id param or business admin who's permission is not set.
                if (verifiedBusinessProfessional == null)
                {
                    return Content("ACCESS DENIED!.");
                }
                else
                {

                    var professionalSchedules = await _professionalDataService.GetProfessionalSchedulesByBusinessId(businessId.GetValueOrDefault());
                    var filtered = professionalSchedules.Where(s=> s.ProfessionalId == professionalId).ToList();
                    ViewBag.BusinessProfessional = verifiedBusinessProfessional;
                    return View(filtered);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return Content("SORRY, ERROR OCCURED!.");
            }

            
        }
    }
}
