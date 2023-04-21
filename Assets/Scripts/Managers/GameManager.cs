using KevinV.WhackAMole.Interfaces;
using KevinV.WhackAMole.Objects;
using KevinV.WhackAMole.Utils;
using UnityEngine;

namespace KevinV.WhackAMole.Managers
{
    public class GameManager : MonoBehaviour
    {
        private const float SPAWN_INTERVAL_TIME = 10f;

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
                    moleSpawner.SetSpawnInterval(moleSpawner.GetCurrentSpawnInterval() / 2f); //TODO test if this doesn't go to fast at the end
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
            mole.Whack();

            //update the score
            if(canScorePoints)
            {
                score += mole.ScoreValue;

                if (mole is IScoreModifier scoreModifier)
                {
                    score = scoreModifier.ModifyScore(score);
                }

                Debug.Log("Score: " + score);
                //TODO update score UI => UIController task.
            }

        }
    }
}
