using Application.ApiResources;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApiResources
{
    public class RecordsApiClient : IRecordsApiClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccesor;

        private HttpClient _httpClient;

        public RecordsApiClient(IConfiguration configuration, IHttpContextAccessor httpContextAccesor)
        {
            _configuration = configuration;

            _httpClient = new HttpClient() { BaseAddress = new Uri(configuration["Services:RecordsApi"]) };

            var token = httpContextAccesor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var refitSettings = new RefitSettings()
            {
                AuthorizationHeaderValueGetter = (req, canc) => Task.FromResult(token)
            };

            Clients = RestService.For<IClientsResource>(_httpClient, refitSettings);
            Recordings = RestService.For<IRecordingsResource>(_httpClient, refitSettings);
            Users = RestService.For<IAuthenticationResource>(_httpClient, refitSettings);
            Photos = RestService.For<IPhotosResource>(_httpClient, refitSettings);
            Configuration = RestService.For<IConfigurationResource>(_httpClient, refitSettings);

            _httpContextAccesor = httpContextAccesor;
        }

        public IClientsResource Clients { get; set; }
        public IRecordingsResource Recordings { get; set; }
        public IAuthenticationResource Users { get; set; }
        public IPhotosResource Photos { get; set; }
        public IConfigurationResource Configuration { get; set; }
    }
}
