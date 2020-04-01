

namespace WebApplication2.Models
{
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class test : DbContext
    {
        private static string Nametest;

        private static string nametest
        {
            get
            {
               return Nametest = ConfigurationManager.ConnectionStrings["test"].ConnectionString;
            }
        }

        public object User { get; internal set; }
    }
}