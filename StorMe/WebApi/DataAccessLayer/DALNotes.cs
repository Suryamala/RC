using DataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Data;
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
          //DALNotes dal = new DALNotes();
          //DataTable dt= dal.connectToDatabase();
          //return dt;
         using (StorMeDbEntities storDbEntities = new StorMeDbEntities())
            {
                storDbEntities.Configuration.ProxyCreationEnabled = false;
                return storDbEntities.Notes.ToList();
            }
        }
    }
}