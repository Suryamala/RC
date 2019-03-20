using DataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class LabelController : ApiController
    {
        BLLabels blLabel = new BLLabels();
        public IHttpActionResult Get(HttpRequestMessage request)
        {
            List<Label> labels = new List<Label>();
            labels = blLabel.getAllLabels();
            if (labels.Count == 0)
            {
                return InternalServerError();
            }
            return Ok(labels);
            // return Ok(notes);
        }
    }
}
