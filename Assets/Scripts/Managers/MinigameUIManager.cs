using System.Collections;
using System.Collections.Generic;
using KevinV.WhackAMole.Interfaces;
using KevinV.WhackAMole.Utils;
using TMPro;
using UnityEngine;

namespace KevinV.WhackAMole.Managers
{
    public class MinigameUIManager : MonoBehaviour, IScoreObserver
    {
        private const string TIMER_TEXT = "Time left: ";
        private const string SCORE_TEXT = "Score: ";

        [SerializeField] private TMP_Text timerText;
        [SerializeField] private TMP_Text scoreTextInGame;
        [SerializeField] private GameObject startGamePanel;

        private int score;

        public float Timer
        {
            set
            {
                timerText.text = TIMER_TEXT + value.ToString("0");
            }
        }

        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
                scoreTextInGame.text = SCORE_TEXT + score.ToString();
            }
        }

        private void Start()
        {
            ObserverManager.Instance.RegisterObserver(this);
        }

        private void OnEnable()
        {
            startGamePanel.SetActive(true);
        }

        public void StartGame()
        {
            GameManager.Instance.StartGame();
        }

        public void OnScoreUpdated(int score)
        {
            Score = score;
        }

        public void OnNotify() { }

        private void OnDestroy()
        {
            ObserverManager.Instance?.UnregisterObserver(this);
        }
    }
}
