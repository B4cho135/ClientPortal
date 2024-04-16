using Application.ApiResources;
using Application.Models;
using Application.Models.Requests;
using ClientPortal.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Refit;
using System.Security.Claims;
using System.Text;

namespace ClientPortal.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IRecordsApiClient _apiClient;

        public AuthenticateController(IHttpClientFactory httpClientFactory, IConfiguration configuration, IRecordsApiClient apiClient)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _apiClient = apiClient;
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            try
            {
                //var user = await _apiClient.Users.Register(new RegisterUserRequest()
                //{
                //    Email = viewModel.Email,
                //    IsAdmin = false,
                //    Password = viewModel.Password,
                //    Username = viewModel.UserName
                //});

                await _apiClient.Clients.Post(new CreateClientRequest()
                {
                    Age = viewModel.Age,
                    FullName = viewModel.FullName,
                    PhoneNumber = viewModel.PhoneNumber,
                    UserId = "1e124803-049b-468f-a6e9-34103b6c9d6c",
                });

                return RedirectToAction("Login", "Authenticate");
            }
            catch(ApiException ex)
            {
                return View(viewModel);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Authenticate");
        }

        public IActionResult Login()
        {
            if(User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            var httpClient = _httpClientFactory.CreateClient();

            httpClient.BaseAddress = new Uri(_configuration["Services:RecordsApi"]);

            var content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");

            var result = await httpClient.PostAsync("/api/authenticate/login", content);

            if(result.IsSuccessStatusCode)
            {
                var tokenJson = await result.Content.ReadAsStringAsync();

                var token = JsonConvert.DeserializeObject<AuthToken>(tokenJson);

                if (token.Expiration?.AddHours(4) > DateTime.Now)
                {
                    List<Claim> claims = new()
                    {
                        new Claim(ClaimTypes.NameIdentifier, viewModel.UserName),
                        new Claim("access_token", token.Token)
                    };

                    ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    return RedirectToAction("Index", "Home");
                }
            }

            viewModel.Password = string.Empty;

            return View(viewModel);
        }
    }
}
