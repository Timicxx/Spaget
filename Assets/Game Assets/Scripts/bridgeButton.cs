using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeButton : MonoBehaviour {
    Vector3 scale;
    public float scaleSpeed = 0.5f;
    public float openSpeed = 25.0f;
    public float closeSpeed = 50.0f;
    private bool pranked = false;

    private void Awake() {
        openSpeed /= PlayerPrefs.GetFloat("Difficulty");
        closeSpeed *= PlayerPrefs.GetFloat("Difficulty");
    }

    private void Start() {
        scale = transform.localScale;
    }

    private void Update() {
        Vector3 raycastPos = new Vector3(transform.position.x, transform.position.y + 0.52f, transform.position.z);
        if (scale.y <= 1) {
            scale = transform.localScale;
            scale.y += scaleSpeed * Time.deltaTime;
            transform.localScale = scale;
        }

        RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.up / 2);
        Debug.DrawRay(raycastPos, Vector2.up / 2, Color.green);

        if (hit) {
            if(hit.distance <= 0.1f) {
                if(scale.y >= 0.4f) { 
                    scale = transform.localScale;
                    scale.y -= scaleSpeed * Time.deltaTime * 2;
                    transform.localScale = scale;
                }
                if (scale.y <= 0.4f) {
                    OpenBridge();
                }
            }
            if (hit.distance >= 0.2f) {
                CloseBridge();
            }
        }
    }

    private void OpenBridge() {
        GameObject bridge = GameObject.Find("Bridge");
        Vector3 rotation = bridge.transform.eulerAngles;
        if(rotation.z > 180) {
            rotation.z += openSpeed * Time.deltaTime;
            bridge.transform.eulerAngles = rotation;
        }
        if(rotation.z <= 180) {
            pranked = true;
        }
    }

    private void CloseBridge() {
        GameObject bridge = GameObject.Find("Bridge");
        Vector3 rotation = bridge.transform.eulerAngles;
        if (rotation.z > 270 || pranked == true) {
            rotation.z -= closeSpeed * Time.deltaTime;
            bridge.transform.eulerAngles = rotation;
            pranked = false;
        }
    }
}
