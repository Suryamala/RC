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

        //public DataTable connectToDatabase()
        //{

        //    DataSet ds = new DataSet();
        //    string connetionString =@"Data Source=SURYAMALA\SQLEXPRESS;Initial Catalog=storMeDb;User ID=Suryamala;Password=suryamala";
        //    var query = "SELECT Id AS ID, Title AS TITEL, Label AS LABEL, Note AS NOTE FROM [StorMeDb].[dbo].[Notes]";
        //    using (SqlCommand cmd = new SqlCommand(query, new SqlConnection(connetionString)))
        //    {
        //        cmd.Connection.Open();
        //        DataTable table = new DataTable();
        //        table.Load(cmd.ExecuteReader());
        //        ds.Tables.Add(table);
        //        cmd.Dispose();
        //        cmd.Connection.Close();
        //    }
        //    return ds.Tables[0];
        //}

        public List<Note> getAllNotes()
        {
            using (StorMeDbEntities storDbEntities = new StorMeDbEntities())
            {
                storDbEntities.Configuration.ProxyCreationEnabled = false;
                var notes = storDbEntities.Notes.Include("Label1").Include("ToDoNotes");
                return notes.ToList();
            }
        }

        public List<Note> getAllNotes(String searchString)
        {
            using (StorMeDbEntities storDbEntities = new StorMeDbEntities())
            {
                storDbEntities.Configuration.ProxyCreationEnabled = false;
                //var notes = from n in storDbEntities.Notes
                //            join l in storDbEntities.Labels on n.Label equals l.Id
                //            select n;
                //var notes = from n in storDbEntities.Notes
                // join l in storDbEntities.Labels on n.Label equals l.Id
                //          select n;

                var notes = storDbEntities.Notes.Include("Label1").Include("ToDoNotes")
                    .Where(n => n.Title.Contains(searchString) || n.Label1.Name.Contains(searchString)).Select(n=>n);
            
                //var notes = from n in storDbEntities.Notes
                //            join l in storDbEntities.Labels on n.Label equals l.Id //new { n.Label } equals new { LabelId = (Int32)(l.Id)} //
                //            select (i=>new { LabelId = l.Id, LabelName = l.Name, n.Id, n.Label, n.Note1, n.Title, n.Label1});
                //if (!String.IsNullOrEmpty(searchString))
                //{
                //    notes = notes.Where(n => n.Title.Contains(searchString) || n.Label1.Name.Contains(searchString));
                //}

                return notes.ToList();
            }
        }

        public void addNote(Note newNote)
        {
            using (StorMeDbEntities storDbEntities = new StorMeDbEntities())
            {
                    // Your code...
                    // Could also be before try if you know the exception occurs in SaveChanges
                    storDbEntities.Configuration.ProxyCreationEnabled = false;
                //var note = new Note { Label1 = newNote.Label1 };
                // storDbEntities.Notes.Add(note);
                Note note = new Note();
                note.Label = newNote.Label;
                note.Note1 = newNote.Note1;
                note.Title = newNote.Title;
                    storDbEntities.Notes.Add(note);
                    storDbEntities.SaveChanges();

            }
        }

        public void updateNote(Note note)
        {

            using (StorMeDbEntities storDbEntities = new StorMeDbEntities())
            {
                var noteEntity = storDbEntities.Notes.FirstOrDefault(e => e.Id == note.Id);
                noteEntity.Title = note.Title;
                noteEntity.Label = note.Label;
                //noteEntity.Label1 = note.Label1;
                noteEntity.Note1 = note.Note1;
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
                // storDbEntities.ToDoNotes = note.ToDoNotes;
                storDbEntities.SaveChanges();
                //return noteEntity;
            }
        }

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

        public void addToDoNote(Note newToDoNote)
        {
            using (StorMeDbEntities storDbEntities = new StorMeDbEntities())
            {
                    // Your code...
                    // Could also be before try if you know the exception occurs in SaveChanges
                    storDbEntities.Configuration.ProxyCreationEnabled = false;
                    storDbEntities.Notes.Add(newToDoNote);
                    storDbEntities.SaveChanges();
            }
        }

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