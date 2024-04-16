using Application.Models.Requests;
using Application.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApiResources
{
    public interface IAuthenticationResource
    {
        [Post("/api/authenticate/register")]
        Task<UserResponseModel> Register(RegisterUserRequest request);
    }
}
