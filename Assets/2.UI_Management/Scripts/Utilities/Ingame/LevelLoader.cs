using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Isometric.UI
{
    public class LevelLoader : MonoBehaviour
    {
        private static int mainMenuIndex = 1;

        public static void LoadLevel(string str)
        {
            if (Application.CanStreamedLevelBeLoaded(str))
                SceneManager.LoadScene(str);
            else
            {
                Debug.LogWarning("Script LevelLoader_ Function LoadLevel Error : invalid scene name");
            }
        }
        public static void LoadLevel(int index)
        {
            if (index >= 0 && index <= SceneManager.sceneCountInBuildSettings)
            {
                if (index == mainMenuIndex)
                {
                    UI_MainMenu.Open();
                }
                SceneManager.LoadScene(index);

            }
            else
            {
                Debug.LogWarning("Script LevelLoader_ Function LoadLevel Error : invalid scene index");
            }
        }

        public static void ReloadLevel()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            LoadLevel(currentScene.buildIndex);
            /*LoadLevel(currentScene.name);*/ //둘이 똑같아~~
        }

        public static void LoadNextLevel()
        {

            int nextSceneIndex = ((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);

            LoadLevel(nextSceneIndex);
        }

        public static void LoadMainMenu()
        {
            LoadLevel("MainMenu");
        }
    }
}