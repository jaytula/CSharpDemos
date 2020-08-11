using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;

namespace TrackerUI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize the database connections
            TrackerLibrary.GlobalConfig.InitializeConnections(DatabaseType.TextFile);

            string dburi = Environment.GetEnvironmentVariable("DBURI");
            Debug.WriteLine(dburi);
            Debug.WriteLine(GlobalConfig.CnnString(""));
            Application.Run(new CreateTournamentForm());
            // Application.Run(new CreateTeamForm());

        }
    }
}
