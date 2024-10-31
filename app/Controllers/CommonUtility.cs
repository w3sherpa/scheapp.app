using Elastic.CommonSchema;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Models.Data.DspModels;

namespace scheapp.app.Controllers
{
    public static class CommonUtility
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
    }
}
