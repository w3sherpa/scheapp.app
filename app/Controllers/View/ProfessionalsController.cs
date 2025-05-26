using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Models.Data.DspModels;
using scheapp.app.Models.View;
using System.Reflection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace scheapp.app.Controllers.View
{
    [Authorize(Roles = "business_admin,scheapp_admin,business_professional")]
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
                var verifiedBusinessProfessional = await CommonControllerUtility.GetLoggedInProfessionalBusinessDetails(_professionalDataService, User.Identity.Name, businessId);
                //if id is still null that mean either user is scheapp admin who passed no id param or business admin who's permission is not set.
                if (verifiedBusinessProfessional != null)
                {
                    if(verifiedBusinessProfessional.BusinessId != null)
                    {
                        if( professionalId ==  null)
                        {
                            professionalId = verifiedBusinessProfessional.ProfessionalId;
                        }
                        if (verifiedBusinessProfessional.ProfessionalRole != "business_admin" )
                        {
                            professionalId = verifiedBusinessProfessional.ProfessionalId;
                            businessId = verifiedBusinessProfessional.BusinessId;
                        }
                     
                        var professionalsDetails = (await _professionalDataService.GetProfessionalBusinessDetailDsp(professionalId, businessId)).FirstOrDefault();
                        var professionalSchedules = await _professionalDataService.GetProfessionalSchedules(verifiedBusinessProfessional.BusinessId.GetValueOrDefault(), professionalId);
                        var filtered = professionalSchedules.Where(s => s.ProfessionalId == professionalId).ToList();
                        ProfessionalScheduleVM vm = new ProfessionalScheduleVM();
                        vm.BusinessId = professionalsDetails.BusinessId;
                        vm.FirstName = professionalsDetails.FirstName;
                        vm.LastName = professionalsDetails.LastName;
                        vm.ProfessionalId = professionalsDetails.ProfessionalId;
                        vm.Schedules = filtered;
                        ViewBag.BusinessProfessional = verifiedBusinessProfessional;

                        return View(vm);
                    }
                    else
                    {
                        return Content("PROFSSIONAL NEEDS BUSINESS ID. PLEASE CONACT BUSINESS ADMIN TO ADD YOU TO THE BUSINESS.");
                    }
                }
                else
                {
                    return Content("ACCESS DENIED!.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return Content("SORRY, ERROR OCCURED!.");
            }
        }

        public async Task<IActionResult> CreateSchedule(int? businessId, int? professionalId)
        {
            try
            {
                var verifiedBusinessProfessional = await CommonControllerUtility.GetLoggedInProfessionalBusinessDetails(_professionalDataService, User.Identity.Name, businessId);
                //if id is still null that mean either user is scheapp admin who passed no id param or business admin who's permission is not set.
                if (verifiedBusinessProfessional != null)
                {
                    if (verifiedBusinessProfessional.BusinessId != null)
                    {
                        if(verifiedBusinessProfessional.ProfessionalRole != "business_admin")
                        {
                            if(verifiedBusinessProfessional.ProfessionalId != professionalId)
                            {
                                return Content("YOU ARE NOT ALLOWE TO CREATE SCHEDULE FOR OTHER PROFESSIONALS.");
                            }
                        }
                        var professionalsDetails = (await _professionalDataService.GetProfessionalBusinessDetailDsp(professionalId, businessId)).FirstOrDefault();
                        CreateProfessionalScheduleVM createScheduleVM = new CreateProfessionalScheduleVM();
                        createScheduleVM.FirstName = professionalsDetails.FirstName;
                        createScheduleVM.LastName = professionalsDetails.LastName;
                        createScheduleVM.ProfessionalId = professionalId;
                        createScheduleVM.BusinessId = businessId;
                        ViewBag.BusinessProfessional = verifiedBusinessProfessional;
                        return View(createScheduleVM); 
                    }
                    else
                    {
                        return Content("PROFSSIONAL NEEDS BUSINESS ID. PLEASE CONACT BUSINESS ADMIN TO ADD YOU TO THE BUSINESS.");
                    }
                }
                else
                {
                    return Content("ACCESS DENIED!.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return Content("SORRY, ERROR OCCURED!.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var verifiedBusinessProfessional = await CommonControllerUtility.GetLoggedInProfessionalBusinessDetails(_professionalDataService, User.Identity.Name, 3);
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");
            var exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string separator = Path.DirectorySeparatorChar.ToString();

            var uploadedDirectory = $"{exeDir}{separator}wwwroot{separator}uploaded";
            if(!Directory.Exists(uploadedDirectory))
                Directory.CreateDirectory(uploadedDirectory);
            string fullFilePath = $"{uploadedDirectory}{separator}professional-{verifiedBusinessProfessional.BusinessId}.jpg";
            using var image = await Image.LoadAsync(file.OpenReadStream());

            // Optional processing (e.g., auto-orientation)
            image.Mutate(x => x.AutoOrient());

            // Save as JPEG with compression
            var jpegEncoder = new JpegEncoder
            {
                Quality = 25 // Value between 0 (worst) to 100 (best)
            };

            await image.SaveAsJpegAsync(fullFilePath, jpegEncoder);

            using (var stream = new FileStream(fullFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { file.FileName, path = fullFilePath });
        }
    }
}
