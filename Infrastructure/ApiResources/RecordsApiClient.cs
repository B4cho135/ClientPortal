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

            Clients = RestService.For<IClientsApi>(_httpClient, refitSettings);
            Recordings = RestService.For<IRecordingsApi>(_httpClient, refitSettings);
            Users = RestService.For<IAuthenticationResource>(_httpClient, refitSettings);

            _httpContextAccesor = httpContextAccesor;
        }

        public IClientsApi Clients { get; set; }
        public IRecordingsApi Recordings { get; set; }
        public IAuthenticationResource Users { get; set; }
    }
}
