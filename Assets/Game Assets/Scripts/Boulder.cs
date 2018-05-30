using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour {
    public float delay = 2f;
    public GameObject boulder;
    private float time = 0f;
    private Vector3 pos = new Vector3(15f, 9f, 15f);
    private bool Started = false;
	
	void Update () {
        if(transform.position.x > 6f) {
            Started = true;
        }
        spawnBoulders();
	}

    private void spawnBoulders() {
        if (Started) {
            time += Time.deltaTime;
            if(time > delay) {
                Instantiate<GameObject>(boulder, pos, Quaternion.identity, null);
                time = 0f;
            }
        }
    }
}
