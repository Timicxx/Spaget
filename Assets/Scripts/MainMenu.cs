using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using SFB;

public class MainMenu : MonoBehaviour {

    public void StartGame() {
        if (PlayerPrefs.HasKey("custom")) {
            PlayerPrefs.DeleteKey("custom");
        }
        SceneManager.LoadScene("0");
    }

    public void LoadScene() {
        string[] path = StandaloneFileBrowser.OpenFilePanel("Select Scene Map", "", "png", false);

        if (path.Length == 0) {
                return;
        }

        PlayerPrefs.SetInt("custom", 1);
        PlayerPrefs.SetString("sceneMapPath", path[0]);
        SceneManager.LoadScene("custom");
    }

    public void Exit() {
        Application.Quit();
        print("Quit");
    }
}
