using System.Collections;
using System.Collections.Generic;
using KevinV.WhackAMole.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace KevinV.WhackAMole.Behaviours
{
    [RequireComponent(typeof(Button))]
    public class BackToMainButtonBehaviour : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => BackToMainMenu());
        }

        public void BackToMainMenu()
        {
            SceneLoader.Instance.LoadSceneAsync(Utils.Scene.Main);
        }
    }
}
