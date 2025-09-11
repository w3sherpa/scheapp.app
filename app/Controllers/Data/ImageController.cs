using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Reflection;


namespace scheapp.api.Controllers
{
    [Route("[controller]/[Action]")]
    [Authorize]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ILogger _logger;
        public ImageController(ILogger<ImageController> logger)
        {
            _logger = logger;
        }
        [HttpGet("{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string separator = Path.DirectorySeparatorChar.ToString();
            var uploadedDirectory = $"{exeDir}{separator}zzzUploadedImages";
            var filePath = Path.Combine(uploadedDirectory, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Image not found.");
            }

            // Read file as byte[]
            var bytes = System.IO.File.ReadAllBytes(filePath);

            // Return with proper MIME type
            return File(bytes, "image/png");
        }
    }

    public class UploadImageRQ
    {
        public int BusinessId { get; set; }
        public string FileName { get; set; }
        public IFormFile Image { get; set; }
    }
}
