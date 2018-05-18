using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class win : MonoBehaviour { 
    public double time;
    public string url;

    private void Awake() {
        try {
            PlayerPrefs.DeleteKey("goOn");
            PlayerPrefs.DeleteKey("respawned");
        } catch {
            Debug.Log("Guess there's no keys");
        }
    }

    void Update() {
        Debug.Log(url);
        time += Time.deltaTime;
        if (time > 2.0f) {
            if (PlayerPrefs.HasKey("GodMode")) {
                SceneManager.LoadSceneAsync("Menu");
            } else {
                Application.OpenURL(url);
            }
        }
    }
}
