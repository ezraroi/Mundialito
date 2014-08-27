using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mundialito.Logic
{
    public class RandomResults
    {
        private List<string> marks = new List<string>() { "1", "2", "X" };
        private Random rnd = new Random();

        public string GetRandomMark()
        {
            var index = rnd.Next(3);
            return marks[index];
        }

        public KeyValuePair<int, int> GetRandomResult()
        {
            var mark = GetRandomMark();
            switch(mark)
            {
                case "X":
                    var score = rnd.Next(4);
                    return new KeyValuePair<int, int>(score,score);
                case "1":
                    var homeScore = rnd.Next(1, 6);
                    var awayScore = rnd.Next(0, homeScore);
                    return new KeyValuePair<int, int>(homeScore, awayScore);
                case "2":
                    var a = rnd.Next(1, 6);
                    var b = rnd.Next(0, a);
                    return new KeyValuePair<int, int>(b, a);
                default:
                    throw new Exception("Unkown mark " + mark);
                    
            }
        }
    }
}