using FreelancerCorp.BusinessLayer.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Freelancer
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Freelancer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Freelancer/5
        public void Delete(int id)
        {
        }
    }
}
