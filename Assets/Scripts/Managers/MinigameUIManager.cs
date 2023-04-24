using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KevinV.WhackAMole.Managers
{
    public class MinigameUIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private TMP_Text scoreText;

        public void StartGame()
        {
            GameManager.Instance.StartGame();
        }
    }
}
