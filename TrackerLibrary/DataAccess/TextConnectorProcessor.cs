using System;
using System.Collections.Generic;
using System.Text;

// Load the text file
// Convert the text to List<PrizeModel>
// find the max ID
// Add the new record the new ID
// Convert the prizes to list<string>
// Save the list<string> to the text file

namespace TrackerLibrary.DataAccess.TextConnector
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName) // e.g. PrizeModels.csv
        {
            string userProfilePath = System.Environment.GetEnvironmentVariable("USERPROFILE");
            return $"{userProfilePath}\\data\\TournamentTracker\\{fileName}";
        }
        public static List<string> LoadFile(this string file)
        {

        }
    }
}
