using System;
using System.Collections.Generic;
using KevinV.WhackAMole.Interfaces;
using KevinV.WhackAMole.Objects;
using KevinV.WhackAMole.Utils;
using UnityEngine;

namespace KevinV.WhackAMole.Managers
{
    public class GameManager : MonoBehaviour
    {
        private const float SPAWN_INTERVAL_TIME = 10f;
        private const float SPAWN_INTERVAL_MULTIPLIER = 0.8f;

        [SerializeField] private float gameDuration = 60f;
        [SerializeField] private MoleSpawner moleSpawner;
        [SerializeField] private MinigameUIManager minigameUIManager;

        private int score;
        private float timer;
        private float scoreDisabledTime;
        private float spawnIntervalUpdateCount;
        private bool gameIsRunning;
        private bool canScorePoints = true;

        private List<IObserver> observers = new List<IObserver>();

        #region Singleton Implementation
        private static GameManager instance;
        public static GameManager Instance
        {
            get { return instance; }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }

            NormalMole.OnMoleNotWhacked += MoleNotWhacked;
        }
        #endregion

        private void Update()
        {
            GameTimer();
        }

        private void GameTimer()
        {
            if (gameIsRunning)
            {
                // Update game timer & send the remaining time to MinigameUIManager
                timer += Time.deltaTime;
                float timeRemaining = gameDuration - timer;
                minigameUIManager.Timer = timeRemaining;

                if (!canScorePoints && timer >= scoreDisabledTime)
                {
                    EnableScoring();
                }

                // Update spawn interval every 5 seconds
                if (timer >= SPAWN_INTERVAL_TIME * spawnIntervalUpdateCount)
                {
                    moleSpawner.SetSpawnInterval(moleSpawner.GetCurrentSpawnInterval() * SPAWN_INTERVAL_MULTIPLIER);
                    spawnIntervalUpdateCount++;
                }

                if (timer >= gameDuration)
                {
                    EndGame();
                }
            }
        }

        public void StartGame()
        {
            gameIsRunning = true;
            canScorePoints = true;
            spawnIntervalUpdateCount = 0;

            ObserverManager.Instance.NotifyStartGameObservers();
        }

        private void EndGame()
        {
            gameIsRunning = false;
            canScorePoints = false;

            ObserverManager.Instance.NotifyEndGameObservers();
        }

        //regulate th whackmole logic through the gamemanager so we only have one place where the input goes to and let this class handle it
        public void WhackMole(IMole mole)
        {
            if (!mole.whacked)
            {
                mole.Whack();

                UpdateScore(mole.ScoreValue, mole);
            }
        }

        private void MoleNotWhacked(int scoreModifier)
        {
            UpdateScore(scoreModifier);
        }

        public void DisableScoring(float duration)
        {
            canScorePoints = false;
            scoreDisabledTime = timer + duration;
        }

        private void EnableScoring()
        {
            canScorePoints = true;
            scoreDisabledTime = 0;
        }

        private void UpdateScore(int value, IMole mole = null)
        {
            //update the score if possible
            if (canScorePoints)
            {
                score += value;

                //check if the mole that was whacked has the interface IScoreModifier to adjust score based on the logic of that mole
                if (mole != null && mole is IScoreModifier scoreModifier)
                {
                    score = scoreModifier.ModifyScore(score);
                }

                ObserverManager.Instance.NotifyScoreObservers(score);
            }
        }

        private void OnDestroy()
        {
            NormalMole.OnMoleNotWhacked -= MoleNotWhacked;
        }
    }
}
