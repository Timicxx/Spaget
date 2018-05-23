using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour {
    public float delay = 2f;
    public GameObject boulder;
    private GameObject player;
    Rigidbody2D rb;
    private float time = 0f;
    private Vector3 pos;
    private GameObject initialBoulder;
    private bool Started = false;

    void Start () {
        player = GameObject.FindWithTag("Player");
        initialBoulder = GameObject.Find("Boulder");
        rb = initialBoulder.GetComponent<Rigidbody2D>();
        pos = initialBoulder.transform.position;
    }
	
	void Update () {
        if (player.transform.position.x > 6f && !Started) {
                rb.isKinematic = false;
                Started = true;
        } else {
            spawnBoulders();
        }
	}

    private void spawnBoulders() {
        time += Time.deltaTime;
        if(time > delay) {
            GameObject bd = Instantiate<GameObject>(boulder, pos, Quaternion.identity, null);
            bd.GetComponent<Rigidbody2D>().isKinematic = false;
            time = 0f;
        }
    }
}
