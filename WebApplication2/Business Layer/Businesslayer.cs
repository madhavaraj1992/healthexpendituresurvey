using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication2.DataAccess_Layer;
using WebApplication2.Models;

namespace WebApplication2.Business_Layer
{
    public class Businesslayer : IBusinesslayer
    {
        private IDataAccessLayer dataAccessLayer;

        public IDataAccessLayer DataAccessLayer
        {
            get
            {
                if (this.dataAccessLayer == null)
                {
                    this.dataAccessLayer = new DataAccesslayer();
                }
                return this.dataAccessLayer;
            }
            set
            {
                this.dataAccessLayer = value;
            }
        }
        public async Task<bool> sample(SampleData sampleData)
        {
            await this.DataAccessLayer.sample(sampleData).ConfigureAwait(false);
            return true;
        }
    }
}