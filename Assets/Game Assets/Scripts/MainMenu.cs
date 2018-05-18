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
        SceneManager.LoadSceneAsync("0");
    }

    void lastKey() {
        if (PlayerPrefs.HasKey("custom")) {
            PlayerPrefs.DeleteKey("custom");
        }
        if (PlayerPrefs.HasKey("lastScene")) {
            PlayerPrefs.DeleteKey("lastScene");
        }
        if (!PlayerPrefs.HasKey("Difficulty")) {
            PlayerPrefs.SetFloat("Difficulty", 1f);
        }
    }

    public void LoadScene() {
        if (NOLOAD) {
            PlayerPrefs.SetString("sceneMapPath", "WEBGL");
            PlayerPrefs.SetInt("custom", 1);
            SceneManager.LoadScene("custom");
            return;
        }

        string[] path = StandaloneFileBrowser.OpenFilePanel("Select Scene Map", "", "png", false);

        if (path.Length == 0) {
            return;
        }

        PlayerPrefs.SetString("sceneMapPath", path[0]);
        
        PlayerPrefs.SetInt("custom", 1);
        SceneManager.LoadScene("custom");
    }

    public void Exit() {
        Application.Quit();
        print("Quit");
    }
}
