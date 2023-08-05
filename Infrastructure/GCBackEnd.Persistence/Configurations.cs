using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCBackEnd.Persistence
{
	public static class Configurations
	{
		public static string ConnectionStringPostgreSQL {

			get {

				ConfigurationManager configurationManager = new();
				configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/GCBackEnd.API"));
				configurationManager.AddJsonFile("appsettings.json");
				return configurationManager.GetConnectionString("PostgreSQL");
			}
		}
	}
}
