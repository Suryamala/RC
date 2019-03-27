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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LabelController : ApiController
    {
        BLLabels blLabel = new BLLabels();

        // GET api/labels
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

        //public void Post([FromBody]Label value)
        //{
        //    using (StorMeDbEntities storDbEntities = new StorMeDbEntities())
        //    {
        //        storDbEntities.Configuration.ProxyCreationEnabled = false;
        //        storDbEntities.Labels.Add(value);
        //        storDbEntities.SaveChanges();
        //    }
        //}
    }
}
