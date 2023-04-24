using System.Collections;
using System.Collections.Generic;
using KevinV.WhackAMole.Utils;
using TMPro;
using UnityEngine;

namespace KevinV.WhackAMole.Managers
{
    public class MinigameUIManager : MonoBehaviour
    {
        private const string TIMER_TEXT = "Time left: ";
        private const string SCORE_TEXT = "Score: ";
        private const string END_GAME_PANEL_SCORE_TEXT = "Your score: \n";

        [SerializeField] private TMP_Text timerText;
        [SerializeField] private TMP_Text scoreTextInGame;
        [SerializeField] private GameObject endGamePanel;
        [SerializeField] private GameObject startGamePanel;
        [SerializeField] private TMP_InputField inputUsername;
        [SerializeField] private TMP_Text scoreTextEndGamePanel;

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

        private void OnEnable()
        {
            startGamePanel.SetActive(true);
            endGamePanel.SetActive(false);
        }

        public void StartGame()
        {
            GameManager.Instance.StartGame();
        }

        public void ShowEndGamePanel()
        {
            endGamePanel.SetActive(true);
            scoreTextEndGamePanel.text = END_GAME_PANEL_SCORE_TEXT + score;
        }

        public void BackToMainMenu()
        {
            SceneLoader.Instance.LoadSceneAsync(Utils.Scene.Main);
        }

        public void SaveScore()
        {
            //TODO call saving solution.
            Debug.Log("name: " + inputUsername.text + " and score: " + score);

            BackToMainMenu();
        }
    }
}
