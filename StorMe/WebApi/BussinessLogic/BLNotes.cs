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
        // DALNotes provides the Data Access for following methods 
        DALNotes dalNotes = new DALNotes();

        // Get all notes by search string filter or whole list if search string is empty
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

        // Add note
        public void addNote(Note newNote)
        {
            dalNotes.addNote(newNote);
        }

        // Update note
        public void updateNote(Note note)
        {
            dalNotes.updateNote(note);
        }

        // Delete note
        public void deleteNote(int id)
        {
            dalNotes.deleteNote(id);
        }

        // Add To do note
        public void addToDoNote(Note newToDoNote)
        {
            dalNotes.addToDoNote(newToDoNote);
        }

        // Get title's list for search autocomplete on Title
        public List<string> getTitleList(String searchString)
        {
             return dalNotes.getTitleList(searchString);
        }
    }
}