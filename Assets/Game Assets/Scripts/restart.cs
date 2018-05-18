using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour {
    public double time;

    private void Awake() {
        try {
            PlayerPrefs.DeleteKey("goOn");
            PlayerPrefs.DeleteKey("respawned");
        } catch {
            Debug.Log("Guess there's no keys");
        }
        
    }

    void Update () {
        time += Time.deltaTime;
		if(Input.anyKeyDown == true && time > 2.0f) {
            time = 0.0f;
            if(PlayerPrefs.GetInt("custom") == 1) {
                SceneManager.LoadScene("custom");
                return;
            }
            int amountToGoBack = (PlayerPrefs.GetFloat("Difficulty") > 3) ? 3 : (PlayerPrefs.GetInt("lastScene") - 1);
            SceneManager.LoadScene(amountToGoBack);
        }
	}
}
