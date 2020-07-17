using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            TrackerLibrary.GlobalConfig.InitializeConections(true, true);

            Debug.WriteLine("ABC");
            //Application.Run(new TournamentDashboardForm());
            Application.Run(new CreatePrizeForm());

        }
    }
}
