using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool isPaused = false;
    public GameObject PauseMenuUI;

    private void Awake() {
        isPaused = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        isPaused = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Pause() {
        isPaused = true;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Menu () {
        GameObject.FindWithTag("Player").GetComponent<Player>().LoadLevel(0);
        Time.timeScale = 1f;
        Destroy(this.gameObject);
    }

    public void Quit () {
        PlayerPrefs.DeleteAll();
        Application.Quit();
        Debug.Log("Quit");
    }
}
