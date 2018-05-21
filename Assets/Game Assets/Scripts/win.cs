using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class win : MonoBehaviour { 
    public double time;

    private void Awake() {
        try {
            PlayerPrefs.DeleteKey("goOn");
            PlayerPrefs.DeleteKey("respawned");
        } catch {
            Debug.Log("Guess there's no keys");
        }
    }

    void Update() {
        time += Time.deltaTime;
        if (time > 2.0f) {
            SceneManager.LoadSceneAsync("Menu");
        }
    }
}
