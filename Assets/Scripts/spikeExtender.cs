using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeExtender : MonoBehaviour {

    public float spikeSpeed = 0.25f;
    int polarity;

    private void Start(){
        Vector3 screenPoints = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, -15));

        polarity = transform.position.x >= screenPoints.x ? -1 : 1;
    }

    void Update () {
        Vector3 pos = transform.position;
        pos.x += Time.deltaTime * spikeSpeed * polarity;
        transform.position = pos;
	}
}
