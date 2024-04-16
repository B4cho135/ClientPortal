using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class UploadPhotoRequest
    {
        public string? Base64 { get; set; }
        public string? SessionId { get; set; }
    }
}
