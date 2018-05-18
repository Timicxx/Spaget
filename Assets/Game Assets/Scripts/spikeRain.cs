using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeRain : MonoBehaviour {

    [SerializeField]
    GameObject trap;
    double time;
    float delay = 1f;

    private void Awake() {
        delay /= PlayerPrefs.GetFloat("Difficulty");
    }

    void Update () {
        time += Time.deltaTime;
        if(time > delay) {
            SpawnSpike();
            time = 0f;
        }
    }

    void SpawnSpike() {
        GameObject fallingTrap = Instantiate<GameObject>(trap);
        Vector3 pos = GameObject.FindWithTag("Player").transform.position;
        fallingTrap.transform.position = new Vector3(pos.x, pos.y + 10f, 1f);
        return;
    }
}
