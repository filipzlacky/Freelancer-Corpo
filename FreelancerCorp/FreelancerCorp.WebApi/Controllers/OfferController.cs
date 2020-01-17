using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.Facades;
using FreelancerCorp.WebApi.Models.OffersModel;

namespace FreelancerCorp.WebApi.Controllers
{
    public class OfferController : ApiController
    {
        public OfferFacade OfferFacade { get; set; }
        // GET: api/Offer
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Offer/5
        public async Task<OfferDTO> Get(int id)
        {
            if (id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var offer = await OfferFacade.GetOfferAsync(id);
            if (offer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            //offer.Id = 0;
            return offer;
        }

        // POST: api/Offer
        public async Task<string> Post([FromBody]OfferCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            int offerId = await OfferFacade.CreateOfferAsync(model.OfferDTO);
            if (offerId != -1)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Created new offer with id: {offerId}";
        }

        // PUT: api/Offer/5
        public async Task<string> Put(int id, [FromBody]OfferDTO offer)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var success = await OfferFacade.EditOfferAsync(offer);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Updated offer with id: {id}";
        }

        // DELETE: api/Offer/5
        public async Task<string> Delete(int id)
        {
            bool success = await OfferFacade.DeleteOfferAsync(id);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Deleted offer with id: {id}";
        }
    }
}
