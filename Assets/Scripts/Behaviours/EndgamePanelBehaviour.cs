using System.Collections;
using System.Collections.Generic;
using KevinV.WhackAMole.Interfaces;
using KevinV.WhackAMole.Managers;
using KevinV.WhackAMole.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace KevinV.WhackAMole.Behaviours
{
    public class EndgamePanelBehaviour : MonoBehaviour, IScoreObserver, IEndGameObserver
    {
        private const string END_GAME_PANEL_SCORE_TEXT = "Your score: \n";

        [SerializeField] private GameObject endGamePanel;
        [SerializeField] private TMP_Text scoreTextEndGamePanel;

        private int currentScore;

        private void Start()
        {
            ObserverManager.Instance.RegisterObserver(this);
        }

        private void OnEnable()
        {
            HideEndGamePanel();
        }

        private void ShowEndGamePanel()
        {
            endGamePanel.SetActive(true);
            scoreTextEndGamePanel.text = END_GAME_PANEL_SCORE_TEXT + currentScore;
        }

        private void HideEndGamePanel()
        {
            endGamePanel.SetActive(false);
            scoreTextEndGamePanel.text = END_GAME_PANEL_SCORE_TEXT + 0;
        }

        private void GoBackToMainMenu()
        {
            SceneLoader.Instance.LoadSceneAsync(Utils.Scene.Main);
        }

        public void SaveScoreAndGoBackToMainMenu(TMP_InputField username)
        {
            HighscoreManagementSystem.SaveData(currentScore, username.text);

            GoBackToMainMenu();
        }

        public void OnScoreUpdated(int score)
        {
            currentScore = score;
        }

        public void OnEndGame()
        {
            ShowEndGamePanel();
        }

        public void OnNotify() { }

        private void OnDestroy()
        {
            ObserverManager.Instance?.UnregisterObserver(this);
        } 
    }
}
