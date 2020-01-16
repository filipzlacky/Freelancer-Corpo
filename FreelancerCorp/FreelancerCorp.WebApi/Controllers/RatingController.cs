using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.Facades;
using FreelancerCorp.WebApi.Models.RatingsModel;

namespace WebApplication1.Controllers
{
    public class RatingController : ApiController
    {
        public RatingFacade RatingFacade { get; set; }
        // GET: api/Offer
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Offer/5
        public async Task<int> Get(int id)
        {
            if (id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var rating = await RatingFacade.GetRatingsAsync(id);
            if (rating == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            //offer.Id = 0;
            return rating.Id;
        }

        // POST: api/Offer
        public async Task<string> Post([FromBody]RatingDTO model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            int ratingId = await RatingFacade.CreateRatingAsync(model);
            if (ratingId != -1)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Created new rating with id: {ratingId}";
        }

        // PUT: api/Offer/5
        public async Task<string> Put(int id, [FromBody]RatingDTO rating)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var success = await RatingFacade.EditRatingAsync(rating);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Updated rating with id: {id}";
        }

        // DELETE: api/Offer/5
        public async Task<string> Delete(int id)
        {
            bool success = await RatingFacade.DeleteRatingAsync(id);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Deleted rating with id: {id}";
        }
    }
}