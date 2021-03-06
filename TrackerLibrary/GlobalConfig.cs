﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static IDataConnection Connection { get; private set; }
        public static void InitializeConnections(DatabaseType db)
        {
            switch(db)
            {
                case DatabaseType.Sql:
                    SqlConnector sql = new SqlConnector();
                    Connection = sql;
                    break;
                case DatabaseType.TextFile:
                    TextConnector text = new TextConnector();
                    Connection = text;
                    break;
                default:
                    break;

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
