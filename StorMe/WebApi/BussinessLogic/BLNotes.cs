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
        public List<Note> getAllNotes(String searchString)
        {
            List<Note> notes = new List<Note>();
            if (String.IsNullOrEmpty(searchString))
            {
                notes = dalNotes.getAllNotes();
            }
            else
            {
                notes = dalNotes.getAllNotes(searchString);
            }
            return notes;
        }

        public void addNote(Note newNote)
        {
            dalNotes.addNote(newNote);
        }

        public void updateNote(Note note)
        {
            dalNotes.updateNote(note);
        }

        public void deleteNote(int id)
        {
            dalNotes.deleteNote(id);
        }

        public void addToDoNote(Note newToDoNote)
        {
            dalNotes.addToDoNote(newToDoNote);
        }

        public List<string> getTitleList(String searchString)
        {
             return dalNotes.getTitleList(searchString);
        }
    }
}