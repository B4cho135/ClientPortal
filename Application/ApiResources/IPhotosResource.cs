using Application.Models.Requests;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApiResources
{
    public interface IPhotosResource
    {
        [Post("/api/photos")]
        Task UploadPhoto(UploadPhotoRequest request);
    }
}
