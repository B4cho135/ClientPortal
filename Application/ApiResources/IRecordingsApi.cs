using Application.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApiResources
{
    public interface IRecordingsApi
    {
        [Post("/api/Recordings")]
        Task UploadVideoRecording(UploadVideoRecordingRequest request);
    }
}
