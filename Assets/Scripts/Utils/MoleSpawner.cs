using System.Collections.Generic;
using System.Linq;
using KevinV.WhackAMole.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace KevinV.WhackAMole.Utils
{
    public class MoleSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject holeContainer;

        private float spawnInterval = 5f;
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

            if(mole != null && holes.Any(hole => hole.transform.childCount == 0))
            {   
                GameObject goMole = ((MonoBehaviour)mole).gameObject;
                goMole.SetActive(true);

                // Set the mole's parent to the hole and preserve its local transform values
                Quaternion localRot = goMole.transform.localRotation;
                Vector3 localScale = goMole.transform.localScale;

                goMole.transform.SetParent(LookForRandomUnoccupiedHole());

                goMole.transform.localRotation = localRot;
                goMole.transform.localScale = localScale;

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
    }
}
