using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mundialito.DAL
{
    public class DataBaseConnectionProvider
    {
        private static string manualConnetionString = String.Empty;

        public static void SetManualString(string connetion)
        {
            manualConnetionString = connetion;
        }

        public static string GetConnection()
        {
            return String.IsNullOrEmpty(manualConnetionString) ? "name=DefaultConnection" : manualConnetionString;
        }
    }
}