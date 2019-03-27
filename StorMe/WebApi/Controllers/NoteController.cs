using DataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
namespace WebApi.Controllers
{
    //[Authorize]
    [RoutePrefix("api/note")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NoteController : ApiController
    {
        BLNotes blNotes = new BLNotes();
        // GET api/notes
        [HttpGet]
        [Route("all")]
        public IHttpActionResult Get(HttpRequestMessage request, String searchString = null)
        {
            List<Note> notes = new List<Note>();
            notes = blNotes.getAllNotes(searchString);
            return Ok(notes);
        }

        // POST api/values
        [HttpPost]
        [Route("add")]
        public void Post([FromBody]Note newNote)
        {
            blNotes.addNote(newNote);
        }

        // POST api/values
        [HttpPost]
        [Route("addToDoNote")]
        public void addToDoNote([FromBody]Note newNote)
        {
            blNotes.addToDoNote(newNote);
        }

        // PUT api/values/5 edit 
        [HttpPut]
        [Route("updateNote")]
        public IHttpActionResult Put([FromBody]Note note, Int32 id)
        {

           
                blNotes.updateNote(note);
            return Ok(true);
                
                //if (noteEntity == null)
                //{
                //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Note with id = " + id.ToString() + "not found");
                //}
                //else
                //{
                //    return Request.CreateResponse(HttpStatusCode.OK, noteEntity);
                //}
            
        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("delete")]
        public void Delete(Int32 id)
        {
            blNotes.deleteNote(id);
        }

        [HttpGet]
        [Route("titlelist")]
        public List<string> getTitlelist(String searchString = null)
        {
            List<Note> notes = new List<Note>();
            return blNotes.getTitleList(searchString);
        }
    }
}
