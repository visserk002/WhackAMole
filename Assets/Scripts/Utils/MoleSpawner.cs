using KevinV.WhackAMole.Interfaces;
using UnityEngine;

namespace KevinV.WhackAMole.Utils
{
    public class MoleSpawner : MonoBehaviour
    {
        private float spawnInterval = 1f;
        private float currentSpawnInterval;
        private MolePool molePool;

        private void Start() 
        {
            molePool = MolePool.Instance;
            StartSpawning(); //TODO remove after game is running from UI
        }

        public void StartSpawning()
        {
            currentSpawnInterval = spawnInterval;
            InvokeRepeating(nameof(SpawnMole), currentSpawnInterval, currentSpawnInterval);
        }

        public void StopSpawning()
        {
            CancelInvoke();
        }

        private void SpawnMole()
        {
            IMole mole = molePool.GetMole();
            mole.Spawn(GetRandomMolePosition());
        }

        private Vector3 GetRandomMolePosition()
        {
            return new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-2f, 2f)); //TODO replace for a good system
        }

        public void SetSpawnInterval(float interval)
        {
            currentSpawnInterval = interval;
            CancelInvoke();
            InvokeRepeating(nameof(SpawnMole), currentSpawnInterval, currentSpawnInterval);
        }

        public float GetCurrentSpawnInterval()
        {
            return currentSpawnInterval;
        }
    }
}
