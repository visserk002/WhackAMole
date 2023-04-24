using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KevinV.WhackAMole.Managers
{
    public class MainMenuUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject highscoresPanel;

        public void StartWhackAMoleGame()
        {
            //SceneLoader.LoadSceneAsync(WhackAMole);
        }

        public void ShowHighscores()
        {
            highscoresPanel.SetActive(true);
        }

        public void CloseHighscoresPanel()
        {
            highscoresPanel.SetActive(false);
        }
    }
}
