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

        private readonly static DateTime generalBetsCloseTime = new DateTime(2014,6,12).Subtract(TimeSpan.FromDays(1)).ToUniversalTime();//DateTime.Parse(WebConfigurationManager.AppSettings["TournamentStartDate"]).Subtract(TimeSpan.FromDays(1)).ToUniversalTime();
        private readonly static DateTime generalBetsResolveTime = new DateTime(2014, 7, 13).ToUniversalTime();//DateTime.Parse(WebConfigurationManager.AppSettings["TournamentEndDate"]).ToUniversalTime();


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

