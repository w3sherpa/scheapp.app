using scheapp.app.Models.Data.TableModels.Services;

namespace scheapp.app.Models.View
{
    public class ServicesVM
    {
        public List<Service>? Services { get; set; }
        public int BusinessId { get; set; }
    }
    public class ServiceDurationVM
    {
        public List<ServiceDuration>? ServiceDurations { get; set; }
        public int BusinessId { get; set; }
    }
}
