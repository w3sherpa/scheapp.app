using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scheapp.app.DataServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.Reflection;


namespace scheapp.api.Controllers
{
    [Route("[controller]/[Action]")]
    //[Authorize]
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

                using (var memoryStream = new MemoryStream())
                {
                    imageResponse.FileStream.CopyTo(memoryStream);
                    var imageBytes = memoryStream.ToArray();
                    return File(imageBytes, "image/png");
                }
                return NotFound();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound();
            }
        }
    }

    public class UploadImageRQ
    {
        public int BusinessId { get; set; }
        public string FileName { get; set; }
        public IFormFile Image { get; set; }
    }
}
