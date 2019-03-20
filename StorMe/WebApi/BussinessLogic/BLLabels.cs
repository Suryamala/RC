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
        DALLabels dalLabel = new DALLabels();
        public List<Label> getAllLabels()
        {
            return dalLabel.getAllLabels();
        }

    }
}