using System.Collections;
using System.Collections.Generic;
using KevinV.WhackAMole.Objects;
using KevinV.WhackAMole.Utils;
using UnityEngine;

namespace KevinV.WhackAMole.Behaviours
{
    public class HighscoreBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject highscoreEntryPrefab;
        [SerializeField] private Transform highscoreContainer;

        private void OnEnable()
        {
            PopulateFields();
        }

        private void PopulateFields()
        {
            List<HighscoreData> highscores = HighscoreManagementSystem.LoadData();

            foreach (HighscoreData hsData in highscores)
            {
                GameObject entryObject = Instantiate(highscoreEntryPrefab, highscoreContainer);
                HighscoreEntry entry = entryObject.GetComponent<HighscoreEntry>();

                if (entry != null)
                {
                    entry.SetInformation(hsData);
                }
            }
        }
    }
}
