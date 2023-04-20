using KevinV.WhackAMole.Interfaces;
using UnityEngine;

namespace KevinV.WhackAMole.Objects
{
    public class MoleSpawner : MonoBehaviour
    {
        public GameObject molePrefab;
        public GameObject bonusMolePrefab;
        public GameObject subtractorMolePrefab;

        private float spawnInterval = 1f;
        private float currentSpawnInterval;

        private void Start() //TODO remove after game is running from UI
        {
            StartSpawning();
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
            int moleType = Random.Range(0, 3); //TODO hardcoded 3 verwijderen en uit lijst met types halen
            IMole mole;

            switch (moleType)
            {
                case 0:
                    mole = Instantiate(molePrefab, transform).GetComponent<IMole>();
                    break;
                case 1:
                    mole = Instantiate(bonusMolePrefab, transform).GetComponent<IMole>();
                    break;
                case 2:
                    mole = Instantiate(subtractorMolePrefab, transform).GetComponent<IMole>();
                    break;
                default:
                    mole = Instantiate(molePrefab, transform).GetComponent<IMole>();
                    break;
            }

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
