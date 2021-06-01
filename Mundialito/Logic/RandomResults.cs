﻿using System;
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
            Dictionary<int, float> weightsWithZero = new Dictionary<int, float>();
            weightsWithZero.Add(0, 0.3f);
            weightsWithZero.Add(1, 0.4f);
            weightsWithZero.Add(2, 0.35f);
            weightsWithZero.Add(3, 0.20f);
            weightsWithZero.Add(4, 0.1f);

            Dictionary<int, float> weightsNoZero = new Dictionary<int, float>();
            weightsNoZero.Add(1, 0.4f);
            weightsNoZero.Add(2, 0.35f);
            weightsNoZero.Add(3, 0.20f);
            weightsNoZero.Add(4, 0.1f);
            var mark = GetRandomMark();
            switch(mark)
            {
                case "X":
                    var score = weightsWithZero.RandomElementByWeight(e => e.Value).Key;
                    return new KeyValuePair<int, int>(score,score);
                case "1":
                    var homeScore = weightsNoZero.RandomElementByWeight(e => e.Value).Key;
                    var awayScore = rnd.Next(0, homeScore);
                    return new KeyValuePair<int, int>(homeScore, awayScore);
                case "2":
                    var a = weightsNoZero.RandomElementByWeight(e => e.Value).Key;
                    var b = rnd.Next(0, a);
                    return new KeyValuePair<int, int>(b, a);
                default:
                    throw new Exception("Unkown mark " + mark);
                    
            }
        }

    }
}