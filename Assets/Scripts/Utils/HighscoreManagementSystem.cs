using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KevinV.WhackAMole.Objects;
using Newtonsoft.Json;
using UnityEngine;

namespace KevinV.WhackAMole.Utils
{
    public static class HighscoreManagementSystem
    {
        private const string FILE_NAME = "data.json";

        public static void SaveData(int score, string name)
        {
            // Load the existing data
            List<HighscoreData> dataList = LoadData();

            // Create new data entry
            HighscoreData newData = new HighscoreData(score, name);

            // Add new data to existing list
            dataList.Add(newData);

            // Save the updated list to the file
            string json = JsonConvert.SerializeObject(dataList);
            string filePath = Path.Combine(Application.persistentDataPath, FILE_NAME);
            File.WriteAllText(filePath, json);
        }


        public static List<HighscoreData> LoadData()
        {
            string filePath = Path.Combine(Application.persistentDataPath, FILE_NAME);
            List<HighscoreData> highscores = new List<HighscoreData>();

            //See if the json file exists
            if (File.Exists(filePath))
            {
                //Read the data and convert is to a list of HighscoreData objects
                string json = File.ReadAllText(filePath);
                highscores = JsonConvert.DeserializeObject<List<HighscoreData>>(json);

                // Order the list by score, highest to lowest
                highscores = highscores.OrderByDescending(x => x.Score).ToList();
            }

            return highscores;
        }
    }
}
