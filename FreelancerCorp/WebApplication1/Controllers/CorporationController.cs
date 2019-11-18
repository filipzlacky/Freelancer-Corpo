using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.Facades;
using FreelancerCorp.WebApi.Models.CorporationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class CorporationController : ApiController
    {
        public UserFacade UserFacade { get; set; }

        // GET: api/Corporation
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Corporation/5
        public async Task<CorporationDTO> Get(int id)
        {
            if (id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var corporation = await UserFacade.GetCorporationAsync(id);
            if (corporation == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            //corporation.Id = 0;
            return corporation;

        }

        // POST: api/Corporation
        public async Task<string> Post([FromBody]CorporationCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            int corporationId = await UserFacade.CreateCorporationAsync(model.CorporationDTO);
            if (corporationId != -1)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Created new corporation with id: {corporationId}";
        }

        // PUT: api/Corporation/5
        public async Task<string> Put(int id, [FromBody]CorporationDTO corporation)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var success = await UserFacade.EditCorporationAsync(corporation);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Updated corporation with id: {id}";
        }

        // DELETE: api/Corporation/5
        public async Task<string> Delete(int id)
        {
            bool success = await UserFacade.DeleteCorporationAsync(id);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Deleted corporation with id: {id}";
        }
    }
}
