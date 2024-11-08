using Microsoft.AspNetCore.Mvc.Rendering;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Models.Data.DspModels;

namespace scheapp.app.Controllers
{
    public static class CommonControllerUtility
    {
        public static async Task<ProfessionalBusinessDetailDsp?> GetLoggedInProfessionalBusinessDetails(IProfessionalDataService _professionalDataService,string loggedInUserName, int? businessId)
        {
            List<ProfessionalBusinessDetailDsp> allProfessionalBusinessDetails = await _professionalDataService.GetProfessionalBusinessDetailDsp(null, null);
            ProfessionalBusinessDetailDsp? professionalBusinessDetailDsp = null;
            if (businessId == null)
            {
                //make sure logged in user has permission to the business profile
                professionalBusinessDetailDsp = allProfessionalBusinessDetails.Where(p => p.Email == loggedInUserName).First();
            }
            else
            {
                professionalBusinessDetailDsp = allProfessionalBusinessDetails.Where(p => p.Email == loggedInUserName && p.BusinessId == businessId).FirstOrDefault();
            }
            return professionalBusinessDetailDsp;
        }

        public static List<SelectListItem> GetTimeList()
        {
            List<SelectListItem> timeList = new List<SelectListItem>();
            string hourString;
            for (int i = 0; i < 10; i++) 
            {
                hourString = $"0{i.ToString()}";
                timeList.Add(new SelectListItem { Text = $"{hourString}:00:00 AM", Value = $"T{hourString}:00" });
                timeList.Add(new SelectListItem { Text = $"{hourString}:30:00 AM", Value = $"T{hourString}:30" });
            }
            for (int i = 10; i < 12; i++)
            {
                timeList.Add(new SelectListItem { Text = $"{i.ToString()}:00:00 AM", Value = $"T{i.ToString()}:00" });
                timeList.Add(new SelectListItem { Text = $"{i.ToString()}:30:00 AM", Value = $"T{i.ToString()}:30" });
            }
            timeList.Add(new SelectListItem { Text = $"12:00:00 AM", Value = $"T12:00" });
            timeList.Add(new SelectListItem { Text = $"12:30:00 AM", Value = $"T12:30" });

            for (int i = 13; i < 22; i++)
            {
                hourString = $"0{(i-12).ToString()}";
                timeList.Add(new SelectListItem { Text = $"{hourString}:00:00 PM", Value = $"T{i}:00" });
                timeList.Add(new SelectListItem { Text = $"{hourString}:30:00 PM", Value = $"T{i}:30" });
            }
            for (int i = 22; i < 24; i++)
            {
                hourString = $"{(i - 12).ToString()}";
                timeList.Add(new SelectListItem { Text = $"{hourString}:00:00 PM", Value = $"T{i}:00" });
                timeList.Add(new SelectListItem { Text = $"{hourString}:30:00 PM", Value = $"T{i}:30" });
            }
            return timeList;
        }
    }
}
