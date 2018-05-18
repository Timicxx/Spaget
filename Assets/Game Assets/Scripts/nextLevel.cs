using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour {
    public GameObject obj;
    string[] stop = new string[4];
    public List<Vector3> spawnPos;
    int requiredCollectedBalls = 2;

    private void Awake() {
        requiredCollectedBalls = Mathf.FloorToInt(PlayerPrefs.GetFloat("Difficulty") * requiredCollectedBalls);
    }

    public void DestroyMe() {
        int randomIndex = Mathf.RoundToInt(Random.Range(0, spawnPos.Count));
        Instantiate<GameObject>(obj, spawnPos[randomIndex], Quaternion.identity, null);
        PlayerPrefs.SetInt("respawned", PlayerPrefs.GetInt("respawned") + 1);
        PlayerPrefs.SetInt("goOn", PlayerPrefs.GetInt("goOn") + 1);
        Debug.Log(PlayerPrefs.GetInt("goOn"));
        Destroy(gameObject);
        return;
    }

    public void skipStage() {
        if(SceneManager.GetActiveScene().name == "2") {
            PlayerPrefs.DeleteKey("goOn");
            PlayerPrefs.DeleteKey("respawned");
        }
        GameObject.FindWithTag("Player").GetComponent<Player>().LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update() {
        just();
    }

    private void just() {
        if (SceneManager.GetActiveScene().name == "2" && PlayerPrefs.GetInt("goOn") >= requiredCollectedBalls) {
            if (Input.GetKeyDown(KeyCode.J) == true || stop[0] == "j") {
                stop[0] = "j";
                if (Input.GetKeyDown(KeyCode.U) == true || stop[1] == "u") {
                    stop[1] = "u";
                    if (Input.GetKeyDown(KeyCode.S) == true || stop[2] == "s") {
                        stop[2] = "s";
                        if (Input.GetKeyDown(KeyCode.T) == true || stop[3] == "t") {
                            stop[3] = "t";
                            skipStage();
                        }
                    }
                }
            }
        }
    }
}
