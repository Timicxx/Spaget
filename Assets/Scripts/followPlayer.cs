using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour {
    GameObject player;
    Vector3 playerLocation;

	void Start () {
        player = GameObject.FindWithTag("Player");	
	}
	
	void Update () {
        playerLocation = player.transform.position;
        playerLocation.z = -15;
        transform.position = playerLocation;
	}
}
