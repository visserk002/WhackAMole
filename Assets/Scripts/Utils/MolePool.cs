using System.Collections.Generic;
using KevinV.WhackAMole.Interfaces;
using UnityEngine;

namespace KevinV.WhackAMole.Utils
{
    public class MolePool : MonoBehaviour
    {
        [SerializeField] private GameObject molePrefab;
        [SerializeField] private GameObject bonusMolePrefab;
        [SerializeField] private GameObject subtractorMolePrefab;
        [SerializeField] private GameObject disablerMolePrefab;

        private List<IMole> pool;

        private float normalMoleCount = 12;
        private float bonusMoleCount = 2;
        private float subtractorMoleCount = 4;
        private float disablerMoleCount = 2;

        #region Singleton Implementation
        private static MolePool instance;
        public static MolePool Instance
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
            FillPoolWithMoles();
        }

        private void FillPoolWithMoles()
        {
            pool = new List<IMole>();

            for (int i = 0; i < normalMoleCount; i++)
            {
                IMole mole = InstantiateMole(molePrefab);
                pool.Add(mole);
            }
            for (int i = 0; i < bonusMoleCount; i++)
            {
                IMole mole = InstantiateMole(bonusMolePrefab);
                pool.Add(mole);
            }
            for (int i = 0; i < subtractorMoleCount; i++)
            {
                IMole mole = InstantiateMole(subtractorMolePrefab);
                pool.Add(mole);
            }
            for (int i = 0; i < disablerMoleCount; i++)
            {
                IMole mole = InstantiateMole(disablerMolePrefab);
                pool.Add(mole);
            }
        }

        private IMole InstantiateMole(GameObject prefab)
        {
            IMole mole = Instantiate(prefab).GetComponent<IMole>();
            prefab.SetActive(false);
            prefab.transform.position = Vector3.zero;
            return mole;
        }

        public IMole GetMole()
        {
            float moleType = Random.value;
            IMole mole;

            if (moleType < normalMoleCount/pool.Count)
            {
                // normal mole
                mole = GetMoleFromPool(MoleType.Normal);
            }
            else if (moleType < (normalMoleCount + bonusMoleCount) / pool.Count)
            {
                // bonus mole
                mole = GetMoleFromPool(MoleType.Bonus);
            }
            else if(moleType < (normalMoleCount + bonusMoleCount + subtractorMoleCount) / pool.Count)
            {
                // subtractor mole
                mole = GetMoleFromPool(MoleType.Subtractor);
            }
            else
            {
                //disabler mole
                mole = GetMoleFromPool(MoleType.Disabler);
            }

            if (mole == null)
            {
                Debug.LogWarning("MolePool: Out of moles!");
            }

            return mole;
        }

        private IMole GetMoleFromPool(MoleType moleType)
        {
            IMole mole;
            mole = pool.Find(x => !x.IsActive() && x.GetMoleType() == moleType);
            return mole;
        }
    }
}
