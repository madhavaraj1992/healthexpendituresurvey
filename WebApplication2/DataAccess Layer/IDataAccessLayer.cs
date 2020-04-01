using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.DataAccess_Layer
{
    public interface IDataAccessLayer
    {
        Task<bool> sample(SampleData sampleData);
    }
}
