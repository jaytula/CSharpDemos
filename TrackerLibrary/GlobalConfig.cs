using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static List<IDataConnection> Connections { get; private set; } = new List<IDataConnection>();
        public static void InitializeConections(bool database, bool textFiles)
        {
            if (database)
            {
                // TODO - Set up the SQL Connector properly
                SqlConnector sql = new SqlConnector();
                Connections.Add(sql);
            }

            if (textFiles)
            {
                // TODO - Create the Text Connection
                TextConnector text = new TextConnector();
                Connections.Add(text);
            }
        }

        public static string CnnString(string name)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            Debug.WriteLine(builder.ConnectionString);
            builder.ConnectionString = "server=(local);user id=ab;" + "password= a!Pass113;initial catalog=AdventureWorks";
            builder.UserID = "sa";
            builder.InitialCatalog = "Tournaments";
            builder.Password = "myStrong!PassWord";
            return builder.ConnectionString;
        }
    }
}
