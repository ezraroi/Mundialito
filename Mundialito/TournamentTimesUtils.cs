using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Mundialito
{
    public class TournamentTimesUtils
    {

        private static DateTime generalBetsCloseTime = DateTime.Parse(WebConfigurationManager.AppSettings["TournamentStartDate"]).Subtract(TimeSpan.FromDays(1)).ToUniversalTime();
        private static DateTime generalBetsResolveTime = DateTime.Parse(WebConfigurationManager.AppSettings["TournamentEndDate"]).ToUniversalTime();


        public static DateTime GeneralBetsCloseTime 
        {
            get
            {
                return generalBetsCloseTime;
            }   
        }

        public static DateTime GeneralBetsResolveTime
        {
            get
            {
                return generalBetsResolveTime;
            }
        }
    }
}

