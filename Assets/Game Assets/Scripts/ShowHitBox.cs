using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHitBox : MonoBehaviour {
    private SpriteRenderer sr;

    private void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update () {
        if (Input.GetKey(KeyCode.LeftShift)) {
            sr.enabled = true;
        } else {
            sr.enabled = false;
        }
	}
}
