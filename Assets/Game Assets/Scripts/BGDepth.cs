using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGDepth : MonoBehaviour {
    private GameObject player;

	void Start () {
        player = GameObject.FindWithTag("Player");	
	}
	
	void Update () {
        Vector3 localPos = transform.localPosition;
        localPos.x = Mathf.Clamp(-(player.transform.position.x * 0.2f), -1.25f, 1.25f);
        transform.localPosition = localPos;
	}
}
