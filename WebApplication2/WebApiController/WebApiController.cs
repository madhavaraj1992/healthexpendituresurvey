using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication2.Business_Layer;
using WebApplication2.Models;

namespace WebApplication2.WebApiController
{
   // [RoutePrefix()]
    public class WebApiController : ApiController
    {
        private IBusinesslayer businesslayer;

        public IBusinesslayer Businesslayer
        {
            get
            {
                if(this.businesslayer == null)
                {
                    this.businesslayer = new Businesslayer();
                }
                return this.businesslayer;
            }
            set
            {
                this.businesslayer = value;
            }
        }

        [HttpGet]
        public async Task<bool> sample(SampleData sampleData)
        {
            await this.Businesslayer.sample(sampleData).ConfigureAwait(false);
            return true;
        }
    }
}
