using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LoginInterface
{
    class sqlconnection
    {
        

        // Define your connection string
        string connectionString = "Data Source=YourServer;Initial Catalog=YourDatabase;Integrated Security=True";

        // Create a connection object
        SqlConnection connection = new SqlConnection(connectionString);

    }
}
