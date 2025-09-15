using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using scheapp.app.DataServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.Reflection;
using System.Text;


namespace scheapp.api.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IImageDataService _imageDataService;
        public ImageController(ILogger<ImageController> logger, IImageDataService imageDataService)
        {
            _logger = logger;
            _imageDataService = imageDataService;
        }
        [HttpGet]
        public async Task<IActionResult> GetImage(int professionalId)
        {
            try
            {
                
                var imageResponse = await _imageDataService.GetProfessionalImageAsync(1, professionalId);
                if (imageResponse.FileStream != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        imageResponse.FileStream.CopyTo(memoryStream);
                        var imageBytes = memoryStream.ToArray();
                        return File(imageBytes, "image/png");
                    }
                }
                else
                {
                    string imagePath = GetDefaultImagePath("person.svg");
                    var svgText = System.IO.File.ReadAllText(imagePath, Encoding.UTF8);

                    var ms = new MemoryStream(Encoding.UTF8.GetBytes(svgText));
                    ms.Position = 0;

                    // Return as raw SVG file (browser can display it)
                    return File(ms, "image/svg+xml","person.svg");
                }
                return NotFound();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound();
            }
        }

        private string GetDefaultImagePath(string fileName)
        {
            var exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string separator = Path.DirectorySeparatorChar.ToString();
            var uploadedDirectory = $"{exeDir}{separator}Default{separator}{fileName}";
            return uploadedDirectory;
        }
    }

    public class UploadImageRQ
    {
        public int BusinessId { get; set; }
        public string FileName { get; set; }
        public IFormFile Image { get; set; }
    }
}
