using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour {
    string currentScene;
    [SerializeField]
    GameObject obj;
    string[] stop = new string[4];
    public List<Vector3> spawnPos;

    private void Start() {
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void DestroyMe() {
        if(SceneManager.GetActiveScene().name == "2") {
            int randomIndex = Mathf.RoundToInt(Random.Range(0, spawnPos.Count));
            Instantiate<GameObject>(obj, spawnPos[randomIndex], Quaternion.identity, null);
            PlayerPrefs.SetInt("respawned", PlayerPrefs.GetInt("respawned") + 1);
            Destroy(gameObject);
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Destroy(gameObject);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.S) == true || stop[0] == "j") {
            stop[0] = "j";
            if (Input.GetKeyDown(KeyCode.T) == true || stop[1] == "u") {
                stop[1] = "u";
                if (Input.GetKeyDown(KeyCode.O) == true || stop[2] == "s") {
                    stop[2] = "s";
                    if (Input.GetKeyDown(KeyCode.P) == true || stop[3] == "t") {
                        stop[3] = "t";
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                }
            }
        }
    }
}
