using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika
{
    public class DB
    {
        public string connectionString { get; set; }
        public string connection;

        public void getConnection()
        {
            connection = @"Data Source=DB\DB.db";
            connectionString = connection;
        }
    }
}
