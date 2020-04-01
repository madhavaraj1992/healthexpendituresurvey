using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.DataAccess_Layer
{
    public class DataAccesslayer :IDataAccessLayer
    {
        public async Task<bool> sample(SampleData sampleData)
        {
            return true;
        }
    }
}