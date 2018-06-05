using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGDepth : MonoBehaviour {
    private GameObject player;
    public float scrollSpeed = 0.2f;

    void Start () {
        player = GameObject.FindWithTag("Player");	
	}
	
	void Update () {
        Vector3 localPos = transform.localPosition;
        localPos.x = -(player.transform.position.x * scrollSpeed);
        transform.localPosition = localPos;
	}
}
