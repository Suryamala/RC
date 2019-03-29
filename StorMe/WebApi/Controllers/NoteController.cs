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
    //Note: 1) Authorization is not yet done
        //  2) Backend validations are not yet done
        //  3) Response messages(constants) are not handled as that will need to create new service

    //[Authorize] 
    [RoutePrefix("api/note")]

    // Enabling cors to share cross origin resources
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NoteController : ApiController
    {
        // BLNotes provides the business logic of the following methods 
        BLNotes blNotes = new BLNotes();
        
        // GET -- Get all notes list and search string is optional(can be null)
        [HttpGet]
        [Route("all")]
        public IHttpActionResult Get(HttpRequestMessage request, String searchString = null)
        {
            List<Note> notes = new List<Note>();
            notes = blNotes.getAllNotes(searchString);
            return Ok(notes);
        }

        // POST -- Add new simple note
        [HttpPost]
        [Route("add")]
        public void Post([FromBody]Note newNote)
        {
            blNotes.addNote(newNote);
        }

        // POST -- Add new To Do note
        [HttpPost]
        [Route("addToDoNote")]
        public void addToDoNote([FromBody]Note newNote)
        {
            blNotes.addToDoNote(newNote);
        }

        // PUT -- Update note(both simple note and To Do note)  
        [HttpPut]
        [Route("updateNote")]
        public IHttpActionResult Put([FromBody]Note note, Int32 id)
        {
            blNotes.updateNote(note);
            return Ok(true); 
        }

        // DELETE -- Delete note by id 
        [HttpDelete]
        [Route("delete")]
        public void Delete(Int32 id)
        {
            blNotes.deleteNote(id);
        }

        // GET -- Get title list to auto complete the title search
        [HttpGet]
        [Route("titlelist")]
        public List<string> getTitlelist(String searchString = null)
        {
            List<Note> notes = new List<Note>();
            return blNotes.getTitleList(searchString);
        }
    }
}
