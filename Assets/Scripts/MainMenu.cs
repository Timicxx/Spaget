using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using SFB;

public class MainMenu : MonoBehaviour {

    bool NOLOAD = false;

    private void Awake() {
        #if UNITY_WEBGL
            NOLOAD = true;
        #endif
    }

    public void StartGame() {
        lastKey();
        SceneManager.LoadScene("0");
    }

    void lastKey() {
        if (PlayerPrefs.HasKey("custom")) {
            PlayerPrefs.DeleteKey("custom");
        }
        if (PlayerPrefs.HasKey("lastScene")) {
            PlayerPrefs.DeleteKey("lastScene");
        }
    }

    public void LoadScene() {
        if (!NOLOAD) {
            string[] path = StandaloneFileBrowser.OpenFilePanel("Select Scene Map", "", "png", false);

            if (path.Length == 0) {
                return;
            }

            PlayerPrefs.SetInt("custom", 1);
            PlayerPrefs.SetString("sceneMapPath", path[0]);
            SceneManager.LoadScene("custom");
        }
    }

    public void Exit() {
        Application.Quit();
        print("Quit");
    }
}
