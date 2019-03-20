using DataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.DataAccessLayer
{
    public class DALLabels
    {
        public List<Label> getAllLabels()
        {
            using (StorMeDbEntities storDbEntities = new StorMeDbEntities())
            {
                storDbEntities.Configuration.ProxyCreationEnabled = false;
                return storDbEntities.Labels.ToList();
            }
        }
    }
}