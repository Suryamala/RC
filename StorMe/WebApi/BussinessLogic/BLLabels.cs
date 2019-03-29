using DataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi;
using WebApi.DataAccessLayer;

namespace WebApi
{
    public class BLLabels
    {
        // DALLabels provides the Data access for labels
        DALLabels dalLabel = new DALLabels();

        // Get all lables 
        public List<Label> getAllLabels()
        {
            return dalLabel.getAllLabels();
        }

    }
}