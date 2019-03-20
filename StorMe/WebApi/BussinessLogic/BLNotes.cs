using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLibrary;
using WebApi.DataAccessLayer;
using System.Data;

namespace WebApi
{
    public class BLNotes
    {

        DALNotes dalNotes = new DALNotes();
        public List<Note> getAllNotes()
        {
            return dalNotes.getAllNotes();
            //List<Note> notes = new List<Note>();
            //DataTable dt= dalNotes.getAllNotes();
            //foreach(DataRow row in dt.Rows)
            //{
            //    Note n = new Note(row);
            //    notes.Add(n);
            //}
            //return notes;
        }
    }
}