using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public static class DBContextFactory
    {
        public static string GetDBLocation()
        {
            var folderPath = Environment.CurrentDirectory; //TODO bind to appsettings.json
            return Path.Join(folderPath, "shop.db");
        }
        public static string GetDBConnectionString()
        {
            return $"Data Source={GetDBLocation()}";
        }

        public static DbContextOptions<MainContext> GenerateOptions()
        {
            // TODO Make MS SQL
            return new DbContextOptionsBuilder<MainContext>().UseSqlite($"Data Source={GetDBLocation()}").Options;
        }
        public static DbContextOptions<MainContext> GenerateOptions(string location)
        {
            // TODO Make MS SQL
            return new DbContextOptionsBuilder<MainContext>().UseSqlite($"Data Source={location}").Options;
        }
    }
}
