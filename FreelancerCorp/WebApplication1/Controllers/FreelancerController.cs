﻿using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.Facades;
using FreelancerCorp.WebApi.Models.FreelancerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class FreelancerController : ApiController
    {
        public UserFacade userFacade { get; set; }

        // GET: api/Freelancer
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Freelancer/5
        public async Task<FreelancerDTO> Get(int id)
        {
            var freelancer = await userFacade.GetFreelancerAsync(id);
            return freelancer;
        }

        // POST: api/Freelancer
        public async Task<string> Post([FromBody]FreelancerCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            int freelancerId = await userFacade.CreateFreelancerAsync(model.freelancerDto);
            if (freelancerId != -1)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Created new freelancer with id: {freelancerId}";
        }

        // PUT: api/Freelancer/5
        public async Task<string> Put(int id, [FromBody]FreelancerDTO freelancer)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var success = await userFacade.EditFreelancerAsync(freelancer);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Updated freelancer with id: {id}";
        }

        // DELETE: api/Freelancer/5
        public async Task<string> Delete(int id)
        {
            bool success = await userFacade.DeleteFreelancerAsync(id);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Deleted freelancer with id: {id}";
        }
    }
}
