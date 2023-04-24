using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KevinV.WhackAMole.Objects
{
    [System.Serializable]
    public class HighscoreData
    {
        public int Score;
        public string Name;

        public HighscoreData(int score, string name)
        {
            Score = score;
            Name = name;
        }
    }
}
