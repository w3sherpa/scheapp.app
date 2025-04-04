﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly ILogger _logger;
        private readonly IProfessionalDataService _professionalDataService;
        private readonly IBusinessDataService _businessDataService;
        public BusinessAdminController(
            ILogger<BusinessAdminController> logger
            , IProfessionalDataService professionalDataService
            , IBusinessDataService businessDataService)
        {
            _logger = logger;
            _professionalDataService = professionalDataService;
            _businessDataService = businessDataService;
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
            try
            {
                var verifiedBusinessProfessional = await GetLoggedInProfessionalBusinessDetails(businessId);
                //if id is still null that mean either user is scheapp admin who passed no id param or business admin who's permission is not set.
                if (verifiedBusinessProfessional == null)
                {
                    return Content("ACCESS DENIED!.");
                }
                else
                {
                    var scheduledAppoitments = await _professionalDataService.GetProfessionalScheduleAppointmentRequestsDetailsByBusinessId(verifiedBusinessProfessional.BusinessId.GetValueOrDefault(),null);

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
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return Content("SORRY, ERROR OCCURED!.");
            }
        }
        public async Task<IActionResult> Appointments(int? businessId,bool? todayOnly,DateOnly? date)
        {
            try
            {
                var verifiedBusinessProfessional = await GetLoggedInProfessionalBusinessDetails(businessId);
                //if id is still null that mean either user is scheapp admin who passed no id param or business admin who's permission is not set.
                if (verifiedBusinessProfessional == null)
                {
                    return Content("ACCESS DENIED!.");
                }
                else
                {
                    AppointmentsVM appointmentsVM = new AppointmentsVM();
                    if (todayOnly.GetValueOrDefault()) date = DateOnly.FromDateTime(DateTime.Now);
                    var scheduledAppoitments = await _professionalDataService.GetProfessionalScheduleAppointmentRequestsDetailsByBusinessId(verifiedBusinessProfessional.BusinessId.GetValueOrDefault(), date);

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

                    appointmentsVM.Appointments = prosche;
                    if((todayOnly == null || todayOnly.GetValueOrDefault()) && date == null)
                    {
                        appointmentsVM.IsAllDateSelected = true;
                        appointmentsVM.IsTodaySelected = false;
                    }
                    else if (todayOnly.GetValueOrDefault())
                    {
                        appointmentsVM.IsAllDateSelected = false;
                        appointmentsVM.IsTodaySelected = true;
                    }
                    else
                    {
                        appointmentsVM.IsDateSelected = true;
                        appointmentsVM.SelectedDate = date.GetValueOrDefault().ToString("yyyy-MM-dd");
                    }
                    
                    ViewBag.BusinessProfessional = verifiedBusinessProfessional;
                    return View(appointmentsVM);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return Content("SORRY, ERROR OCCURED!.");
            }
        }
        public async Task<IActionResult> Professionals(int? businessId)
        {
            try 
            { 
                var verifiedBusinessProfessional = await GetLoggedInProfessionalBusinessDetails(businessId);
                //if id is still null that mean either user is scheapp admin who passed no id param or business admin who's permission is not set.
                if (verifiedBusinessProfessional == null)
                {
                    return Content("ACCESS DENIED!.");
                }
                else
                {
                    List<ProfessionalBusinessDetailDsp> allProfessionalBusinessDetails = await _professionalDataService.GetProfessionalBusinessDetailDsp(null, businessId);

                    ViewBag.BusinessProfessional = verifiedBusinessProfessional;
                    return View(allProfessionalBusinessDetails);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return Content("SORRY, ERROR OCCURED!.");
            }
        }

        public async Task<IActionResult> EdidProfessional(int? businessId, int? professinalId)
        {
            try
            {
                var verifiedBusinessProfessional = await GetLoggedInProfessionalBusinessDetails(businessId);
                //if id is still null that mean either user is scheapp admin who passed no id param or business admin who's permission is not set.
                if (verifiedBusinessProfessional == null)
                {
                    return Content("ACCESS DENIED!.");
                }
                else
                {
                    List<ProfessionalBusinessDetailDsp> allProfessionalBusinessDetails = await _professionalDataService.GetProfessionalBusinessDetailDsp(null, businessId);
                    ViewBag.BusinessProfessional = verifiedBusinessProfessional;
                    return View(allProfessionalBusinessDetails);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return Content("SORRY, ERROR OCCURED!.");
            }
        }
        public async Task<IActionResult> Customers(int? businessId)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return Content("SORRY, ERROR OCCURED!.");
            }
        }
        
        public async Task<IActionResult> Services(int? businessId)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return Content("SORRY, ERROR OCCURED!.");
            }
        }
    }
}
