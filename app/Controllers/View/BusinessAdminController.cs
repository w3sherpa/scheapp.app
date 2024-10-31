using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scheapp.app.DataServices;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Models.Data.DspModels;
using scheapp.app.Models.Data.TableModels.Businesses;
using scheapp.app.Models.View;

namespace scheapp.app.Controllers.View
{
    [Authorize(Roles = "business_admin,scheapp_admin")]
    public class BusinessAdminController : Controller
    {
        private readonly IProfessionalDataService _professionalDataService;
        public BusinessAdminController(IProfessionalDataService professionalDataService)
        {
            _professionalDataService = professionalDataService;
        }
        private async Task<ProfessionalBusinessDetailDsp?> GetLoggedInProfessionalBusinessDetails(int? businessId)
        {
            List<ProfessionalBusinessDetailDsp> allProfessionalBusinessDetails = await _professionalDataService.GetProfessionalBusinessDetailDsp(null, null);
            ProfessionalBusinessDetailDsp? professionalBusinessDetailDsp = null;
            var emailUsername = User.Identity.Name;
            if (businessId == null)
            {
                //make sure logged in user has permission to the business profile
                professionalBusinessDetailDsp = allProfessionalBusinessDetails.Where(p => p.Email == emailUsername).First();
            }
            else
            {
                professionalBusinessDetailDsp = allProfessionalBusinessDetails.Where(p => p.Email == emailUsername && p.BusinessId == businessId).FirstOrDefault();
            }
            return professionalBusinessDetailDsp;
        }
        public async Task<IActionResult> Index(int? businessId)
        {
            var verifiedBusinessProfessional = await GetLoggedInProfessionalBusinessDetails(businessId);
            //if id is still null that mean either user is scheapp admin who passed no id param or business admin who's permission is not set.
            if (verifiedBusinessProfessional == null)
            {
                return Content("ACCESS DENIED!.");
            }

            var scheduledAppoitments = await _professionalDataService.GetProfessionalScheduleAppointmentRequestsDetailsByBusinessId(verifiedBusinessProfessional.BusinessId.GetValueOrDefault());

            List<ProfessionalScheduleAppointmentVM> prosche = scheduledAppoitments.Select(s => new ProfessionalScheduleAppointmentVM
            {
                StartDT = s.StartDT
                                                                                                    ,
                EndDT = s.EndDT
                                                                                                    ,
                CustomerConfirmed = s.CustomerConfirmed
                                                                                                    ,
                ProfessionalConfirmed = s.ProfessionalConfirmed
                                                                                                    ,
                RequestDate = s.RequestDate
                                                                                                    ,
                ServiceName = s.ServiceName
                                                                                                    ,
                ScheduleAppointmentId = s.ScheduleAppointmentId
                                                                                                    ,
                Customer = $"{s.CustFrist} {s.CustLast}"
                                                                                                    ,
                Professional = $"{s.ProFirst} {s.ProLast}"
            }).ToList();
            ViewBag.BusinessProfessional = verifiedBusinessProfessional;
            return View(prosche);
        }
        public async Task<IActionResult> Professionals(int? businessId)
        {
            var verifiedBusinessProfessional = await GetLoggedInProfessionalBusinessDetails(businessId);
            //if id is still null that mean either user is scheapp admin who passed no id param or business admin who's permission is not set.
            if (verifiedBusinessProfessional == null)
            {
                return Content("ACCESS DENIED!.");
            }


            ViewBag.BusinessProfessional = verifiedBusinessProfessional;
            return View();
        }

        public async Task<IActionResult> Customers(int? businessId)
        {
            var verifiedBusinessProfessional = await GetLoggedInProfessionalBusinessDetails(businessId);
            //if id is still null that mean either user is scheapp admin who passed no id param or business admin who's permission is not set.
            if (verifiedBusinessProfessional == null)
            {
                return Content("ACCESS DENIED!.");
            }


            ViewBag.BusinessProfessional = verifiedBusinessProfessional;
            return View();
        }
        
        public async Task<IActionResult> Services(int? businessId)
        {
            var verifiedBusinessProfessional = await GetLoggedInProfessionalBusinessDetails(businessId);
            //if id is still null that mean either user is scheapp admin who passed no id param or business admin who's permission is not set.
            if (verifiedBusinessProfessional == null)
            {
                return Content("ACCESS DENIED!.");
            }


            ViewBag.BusinessProfessional = verifiedBusinessProfessional;
            return View();
        }
    }
}
