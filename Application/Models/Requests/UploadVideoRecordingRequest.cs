using Microsoft.AspNetCore.Http;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class UploadVideoRecordingRequest
    {
        public byte[] FileBytes { get; set; } = [];
    }
}
