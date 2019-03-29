using DataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApi.DataAccessLayer
{
    public class DALNotes
    {
        // Get all notes list if search string is empty
        public List<Note> getAllNotes()
        {
            using (StorMeDbEntities storDbEntities = new StorMeDbEntities())
            {
                storDbEntities.Configuration.ProxyCreationEnabled = false;
                var notes = storDbEntities.Notes.Include("Label1").Include("ToDoNotes");
                return notes.ToList();
            }
        }

        // Get all notes by search string filter
        public List<Note> getAllNotes(String searchString)
        {
            using (StorMeDbEntities storDbEntities = new StorMeDbEntities())
            {
                storDbEntities.Configuration.ProxyCreationEnabled = false;
                var notes = storDbEntities.Notes.Include("Label1").Include("ToDoNotes")
                    .Where(n => n.Title.Contains(searchString) || n.Label1.Name.Contains(searchString)).Select(n=>n);
                return notes.ToList();
            }
        }

        // Add new note
        public void addNote(Note newNote)
        {
            using (StorMeDbEntities storDbEntities = new StorMeDbEntities())
            {
                storDbEntities.Configuration.ProxyCreationEnabled = false;
                // New note object is created to avoid the insertion of foriegn key fields(Labels) in lables table
                Note note = new Note();
                note.Label = newNote.Label;
                note.Note1 = newNote.Note1;
                note.Title = newNote.Title;
                storDbEntities.Notes.Add(note);
                storDbEntities.SaveChanges();
            }
        }

        // Update note
        public void updateNote(Note note)
        {
            using (StorMeDbEntities storDbEntities = new StorMeDbEntities())
            {
                var noteEntity = storDbEntities.Notes.FirstOrDefault(e => e.Id == note.Id);
                noteEntity.Title = note.Title;
                noteEntity.Label = note.Label;
                noteEntity.Note1 = note.Note1;
                // To avoid the duplication of same To do items in ToDoNotes table, existing ToDos are removed and only status is changed
                foreach (var toDo in note.ToDoNotes) {
                    ToDoNote newToDo;
                    var existingTodo = storDbEntities.ToDoNotes.SingleOrDefault(t => t.Id == toDo.Id);
                    if (existingTodo != null)
                    {
                        newToDo = existingTodo;
                        newToDo.isChecked = toDo.isChecked;
                    }
                    else
                    {
                        newToDo = new ToDoNote();
                    }
                    noteEntity.ToDoNotes.Add(newToDo);
                }
                storDbEntities.SaveChanges();
            }
        }


        // Delete note by id
        public void deleteNote(int id)
        {
            using (StorMeDbEntities storDbEntities = new StorMeDbEntities())
            {
                //first or default e such that e.Id -- lambda function
                Note note = storDbEntities.Notes.FirstOrDefault(e => e.Id == id);
                storDbEntities.Notes.Remove(note);
                storDbEntities.SaveChanges();
            }
        }

        // Add new To do note
        public void addToDoNote(Note newToDoNote)
        {
            using (StorMeDbEntities storDbEntities = new StorMeDbEntities())
            {
                storDbEntities.Configuration.ProxyCreationEnabled = false;
                Note toDo = new Note();
                toDo.Label = newToDoNote.Label;
                toDo.Note1 = newToDoNote.Note1;
                toDo.Title = newToDoNote.Title;
                toDo.ToDoNotes = newToDoNote.ToDoNotes;
                storDbEntities.Notes.Add(toDo);
                storDbEntities.SaveChanges();
            }
        }

        // Get title list with search string filter
        public List<string> getTitleList(String searchString)
        {
            using (StorMeDbEntities storDbEntities = new StorMeDbEntities())
            {
                storDbEntities.Configuration.ProxyCreationEnabled = false;
                var titles = storDbEntities.Notes.Select(n => n.Title).Where(n => n.Contains(searchString));
                return titles.ToList();
            }
        }
    }
}