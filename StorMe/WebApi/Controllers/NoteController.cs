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
    //[Authorize]
    //[RoutePrefix("api/Notes")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NoteController : ApiController
    {
        BLNotes blNotes = new BLNotes();
        // GET api/notes
        //[HttpGet]
        //[Route("all")]
        //public List<Note> Get()
        //{ 
        //    return blNotes.getAllNotes();
        //}
        public IHttpActionResult Get(HttpRequestMessage request)
        {
            List<Note> notes = new List<Note>();
            notes = blNotes.getAllNotes();
            if (notes.Count == 0)
            {
                return InternalServerError();
            }
            return Ok(notes);
           // return Ok(notes);
        }

        // GET api/notes/5 -- Remove code
        //public Note Get(int id)
        //{
        //    using (StorMeDbEntities storDbEntities = new StorMeDbEntities())
        //    {
        //        storDbEntities.Configuration.ProxyCreationEnabled = false;
        //        //first or default e such that e.Id -- lambda function
        //        return storDbEntities.Notes.FirstOrDefault(e => e.Id == id);
        //    }
        //}

        // POST api/values
        public void Post([FromBody]Note value)
        {
            using (StorMeDbEntities storDbEntities = new StorMeDbEntities())
            {
                storDbEntities.Configuration.ProxyCreationEnabled = false;
                storDbEntities.Notes.Add(value);
                storDbEntities.SaveChanges();
            }
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            using (StorMeDbEntities storDbEntities = new StorMeDbEntities())
            {
                //first or default e such that e.Id -- lambda function
                Note note = storDbEntities.Notes.FirstOrDefault(e => e.Id == id);
                storDbEntities.Notes.Remove(note);
                storDbEntities.SaveChanges();
            }
        }
    }
}
