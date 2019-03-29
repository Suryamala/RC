using DataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi.Controllers
{
    [RoutePrefix("api/label")]

    // Enabling cors to share cross origin resources
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LabelController : ApiController
    {
        BLLabels blLabel = new BLLabels();

        // GET -- Get all labels 
        [HttpGet]
        [Route("all")]
        public IHttpActionResult Get(HttpRequestMessage request)
        {
            List<Label> labels = new List<Label>();
            labels = blLabel.getAllLabels();
            if (labels.Count == 0)
            {
                return InternalServerError();
            }
            return Ok(labels);
        }
    }
}
