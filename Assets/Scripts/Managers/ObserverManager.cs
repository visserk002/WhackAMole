using System.Collections;
using System.Collections.Generic;
using KevinV.WhackAMole.Interfaces;
using KevinV.WhackAMole.Objects;
using UnityEngine;

namespace KevinV.WhackAMole.Managers
{
    public class ObserverManager : MonoBehaviour
    {
        private List<IObserver> observers = new List<IObserver>();

        #region Singleton Implementation
        private static ObserverManager instance;
        public static ObserverManager Instance
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

            DontDestroyOnLoad(this.gameObject); 
        }
        #endregion

        #region Observer pattern
        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void UnregisterObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyScoreObservers(int newScore)
        {
            foreach (IObserver observer in observers)
            {
                if (observer is IScoreObserver scoreObserver)
                {
                    scoreObserver.OnScoreUpdated(newScore);
                }
            }
        }

        public void NotifyEndGameObservers()
        {
            foreach (IObserver observer in observers)
            {
                if (observer is IEndGameObserver endGameObserver)
                {
                    endGameObserver.OnEndGame();
                }
            }
        }

        public void NotifyStartGameObservers()
        {
            foreach (IObserver observer in observers)
            {
                if (observer is IStartGameObserver startGameObservers)
                {
                    startGameObservers.OnStartGame();
                }
            }
        }
        #endregion
    }
}
