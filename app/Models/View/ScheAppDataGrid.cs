using scheapp.app.Models.Data.TableModels.Professionals;

namespace scheapp.app.Models.View
{
    public class ScheAppDataGrid
    {
        public int Total { get; set; }
        public int TotalNotFiltered { get; set; }
        public List<ProfessionalSchedule> Rows { get; set; } = new();
    }


}
