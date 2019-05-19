using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chapter4Recipe5
{

    public class GameManager : MonoBehaviour
    {

        public static GameManager instance = null;

        private const string SCENE_NAME_01 = "Level1";

        public MenuManager menuManager;

        // Create a singleton
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        public void OnPlayGameClicked()
        {
            Debug.Log("OnPlayGame() is starting to load scene");
            StartCoroutine(LoadScene(SCENE_NAME_01));
        }

        IEnumerator LoadScene(string sceneName)
        {
            yield return null;

            AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            asyncOp.allowSceneActivation = false;

            while (!asyncOp.isDone)
            {
                float progress = Mathf.Clamp01(asyncOp.progress / 0.9f);

                // Load is complete at 0.9
                if (asyncOp.progress == 0.9f)
                {
                    // Scene loaded, now activate
                    asyncOp.allowSceneActivation = true;

                }

                yield return null;
            }
        }

        public void OnGameOver()
        {
            Debug.Log("I am OnGameOver()");
            StartCoroutine(UnloadScene());
            menuManager.OnGameOver();
        }

        IEnumerator UnloadScene()
        {
            yield return SceneManager.UnloadSceneAsync(SCENE_NAME_01);

        }
    }
}
