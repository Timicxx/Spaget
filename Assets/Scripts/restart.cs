using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour {
    public double time;

    void Update () {
        time += Time.deltaTime;
		if(Input.anyKeyDown == true && time > 2.0f) {
            time = 0.0f;
            if(PlayerPrefs.GetInt("custom") == 1) {
                SceneManager.LoadScene("custom");
                return;
            }
            SceneManager.LoadScene(PlayerPrefs.GetInt("lastScene") - 1);
        }
	}
}
