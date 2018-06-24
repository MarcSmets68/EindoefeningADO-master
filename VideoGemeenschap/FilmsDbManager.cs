using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGemeenschap
{
    public class FilmsDbManager
    {
        private static ConnectionStringSettings conFilmSettings = ConfigurationManager.ConnectionStrings["Films"];
        private static DbProviderFactory factory = DbProviderFactories.GetFactory(conFilmSettings.ProviderName);

        public DbConnection GetConnection()
        {
            var conTuin = factory.CreateConnection();
            conTuin.ConnectionString = conFilmSettings.ConnectionString;
            return conTuin;
        }
    }
}
