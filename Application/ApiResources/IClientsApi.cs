using Application.Models.Requests;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApiResources
{
    [Headers("accept: application/json")]
    public interface IClientsApi
    {
        [Get("/api/clients")]
        Task<List<ClientEntity>> GetAll();

        [Get("/api/clients/{id}")]
        Task<ClientEntity> GetById(int id);

        [Put("/api/clients/{id}")]
        Task Put(int id, ClientEntity entity);

        [Post("/api/clients")]
        Task Post([Body]CreateClientRequest command);

        [Delete("/api/clients/{id}")]
        Task Delete(int id);
    }
}
