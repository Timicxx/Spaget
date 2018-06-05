using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class just : MonoBehaviour {
    public GameObject Line;
    Vector3 initialPos;
    int lines = 0;

    private void Start() {
        PlayerPrefs.SetInt("respawned", 0);
        initialPos = GameObject.Find("Line").transform.position;
    }

    void Update () {
        int respawned = PlayerPrefs.GetInt("respawned");
        if (respawned != lines) {
            initialPos.y -= 5;
            lines++;
            Instantiate<GameObject>(Line, initialPos, Quaternion.identity, GameObject.Find("Lines").transform);
        }
    }
}
