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

        private int score;
        private float timer;
        private float scoreDisabledTime;
        private float spawnIntervalUpdateCount;
        private bool gameIsRunning;
        private bool canScorePoints = true;

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

        private void Start()
        {
            StartGame();
        }

        private void Update()
        {
            GameTimer();
        }

        private void GameTimer()
        {
            if (gameIsRunning)
            {
                // Update game timer
                timer += Time.deltaTime;

                if(!canScorePoints && timer >= scoreDisabledTime)
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

        private void MoleNotWhacked(int scoreModifier)
        {
            UpdateScore(scoreModifier);
        }

        public void StartGame()
        {
            gameIsRunning = true;
            canScorePoints = true;
            spawnIntervalUpdateCount = 0;
            moleSpawner.StartSpawning();
        }

        public void EndGame()
        {
            gameIsRunning = false;
            canScorePoints = false;
            moleSpawner.StopSpawning();

            //TODO Call UIController to show endscreen
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

        public void WhackMole(IMole mole)
        {
            if(!mole.whacked)
            {
                UpdateScore(mole.ScoreValue, mole);

                mole.Whack();
            }
        }

        private void UpdateScore(int value, IMole mole = null)
        {
            //update the score
            if (canScorePoints)
            {
                score += value;

                if (mole != null && mole is IScoreModifier scoreModifier)
                {
                    score = scoreModifier.ModifyScore(score);
                }

                Debug.Log("Score: " + score);
                //TODO update score UI => UIController task.
            }
        }

        private void OnDestroy()
        {
            NormalMole.OnMoleNotWhacked -= MoleNotWhacked;
        }
    }
}
