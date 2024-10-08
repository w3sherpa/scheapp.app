using System.ComponentModel.DataAnnotations;

namespace scheapp.app.Models.Data.TableModels.Communication
{
    public class CustomerCall
    {

        public int Id { get; set; }
        public int CostomerContactId { get; set; }
        public DateTime CalledAt { get; set; } = DateTime.MinValue;
        public string Message { get; set; } = "";
        public string Status { get; set; } = "";
        public int BusinessId { get; set; }
    }
}
