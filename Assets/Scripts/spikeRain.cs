using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeRain : MonoBehaviour {

    [SerializeField]
    GameObject trap;
    double time;
    float delay = 0.5f;
	
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
        fallingTrap.transform.position = new Vector3(pos.x, pos.y + 5f, 1f);
        return;
    }
}
