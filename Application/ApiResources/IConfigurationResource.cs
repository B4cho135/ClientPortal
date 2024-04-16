using Application.Models.Responses;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApiResources
{
    public interface IConfigurationResource
    {
        [Get("/api/configuration")]
        Task<RecorderConfigurationResponseModel> GetConfiguration();
    }
}
