using System.Collections;
using KevinV.WhackAMole.Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KevinV.WhackAMole.Utils
{
    public class SceneLoader : MonoBehaviour
    {
        #region Singleton Implementation
        private static SceneLoader instance;
        public static SceneLoader Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType(typeof(SceneLoader)) as SceneLoader;
                }
                    
                return instance;
            }
            set
            {
                instance = value;
            }
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

        public void LoadScene(Scene scene, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene((int)scene, loadSceneMode);
        }

        public void LoadSceneAsync(Scene scene, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            StartCoroutine(LoadSceneAsyncCoroutine(scene, loadSceneMode));
        }

        private IEnumerator LoadSceneAsyncCoroutine(Scene scene, LoadSceneMode loadSceneMode)
        {
            // Start loading the scene asynchronously
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)scene, loadSceneMode);

            // Wait until the scene is fully loaded
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            // Switch to the new scene
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)scene));
        }
    }

    public enum Scene
    {
        Main,
        WhackAMole
    }
}
