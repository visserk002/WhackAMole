using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KevinV.WhackAMole.Objects
{
    public class HighscoreEntry : MonoBehaviour
    {
        private const string SCORE_PRE_TEXT = "Score: ";

        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text scoreText;

        public int score { get; private set; }

        public void SetInformation(HighscoreData hsData)
        {
            nameText.text = hsData.Name;
            scoreText.text = SCORE_PRE_TEXT + hsData.Score;
        }
    }
}
