using Application.ApiResources;
using Application.Models.Requests;
using ClientPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Diagnostics;

namespace ClientPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecordsApiClient _apiClient;

        public HomeController(ILogger<HomeController> logger, IRecordsApiClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        public async Task<IActionResult> Index(DashboardViewModel viewModel)
        {
            if (viewModel == null)
            {
                viewModel = new DashboardViewModel();
            }

            try
            {
                var config = await _apiClient.Configuration.GetConfiguration();

                viewModel.CaptureFrequency = config.CaptureFrequency * 1000;
                viewModel.RecordVideo = config.VideoCapture;
                viewModel.RecordPhoto = config.CapturePhoto;
            }
            catch (ApiException ex)
            {
                if(ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Authenticate");
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RetrieveRecordedMedia(IFormFile file)
        {
            var sessionId = User.Claims.FirstOrDefault(x => x.Type == "sessionGuid")?.Value;

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                var requesModel = new UploadVideoRecordingRequest()
                {
                    FileBytes = fileBytes,
                    SessionId = sessionId
                };

                await _apiClient.Recordings.UploadVideoRecording(requesModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(string base64Image)
        {
            try
            {
                var sessionId = User.Claims.FirstOrDefault(x => x.Type == "sessionGuid")?.Value;

                await _apiClient.Photos.UploadPhoto(new UploadPhotoRequest()
                {
                    Base64 = base64Image,
                    SessionId = sessionId
                });

                return Json(sessionId);
            }
            catch (ApiException ex)
            {

                throw;
            }
            
        }
    }
}
