using System.Collections.Generic;
using System.Linq;
using KevinV.WhackAMole.Interfaces;
using KevinV.WhackAMole.Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace KevinV.WhackAMole.Utils
{
    public class MoleSpawner : MonoBehaviour, IStartGameObserver, IEndGameObserver
    {
        [SerializeField] private GameObject holeContainer;

        private float spawnInterval = 3f;
        private float currentSpawnInterval;
        private MolePool molePool;
        private List<Transform> holes = new List<Transform>();

        private void Start() 
        {
            for (int i = 0; i < holeContainer.transform.childCount; i++)
            {
                holes.Add(holeContainer.transform.GetChild(i));
            }

            molePool = MolePool.Instance;

            GameManager.Instance?.RegisterObserver(this);

        }

        private void StartSpawning()
        {
            SpawnMole(); //Trigger first spawnmole to instantly start the game and use invokerepeating to handle the recuring spawning
            currentSpawnInterval = spawnInterval;
            InvokeRepeating(nameof(SpawnMole), currentSpawnInterval, currentSpawnInterval);
        }

        private void StopSpawning()
        {
            CancelInvoke();
        }

        private void SpawnMole()
        {
            IMole mole = molePool.GetMole();

            if(mole != null && holes.Any(hole => hole.transform.childCount == 0))
            {   
                GameObject goMole = ((MonoBehaviour)mole).gameObject;

                goMole.SetActive(true);
                goMole.transform.SetParent(LookForRandomUnoccupiedHole());
                mole.Spawn();
            }
        }

        private Transform LookForRandomUnoccupiedHole()
        {
            Transform hole = null;

            while (hole == null || hole.childCount > 0)
            {
                hole = holes[Random.Range(0, holes.Count)];
            }
            return hole;
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

        public void OnStartGame()
        {
            StartSpawning();
        }

        public void OnEndGame()
        {
            StopSpawning();
        }

        public void OnNotify() { }

        private void OnDestroy()
        {
            GameManager.Instance?.UnregisterObserver(this);
        }
    }
}
