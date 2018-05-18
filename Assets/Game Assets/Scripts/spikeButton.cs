using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeButton : MonoBehaviour {
    Vector3 scale;
    public float scaleSpeed = 0.5f;
    public float fallSpeed = -49.5f;
    public GameObject trap;
    private Vector3 trapPos;
    private bool spawned = false;

    private void Start() {
        scale = transform.localScale;
        fallSpeed *= PlayerPrefs.GetFloat("Difficulty");
        trapPos = new Vector3(4.5f, 5.0f, 0.0f);
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
                if (scale.y <= 0.4f && !spawned) {
                    spawned = true;
                    SpawnSpike();
                }
            }
            if (hit.distance >= 0.2f) {
                spawned = false;
            }
        }
    }

    void SpawnSpike() {
        GameObject fallingTrap = Instantiate<GameObject>(trap, trapPos, Quaternion.identity, null);
        fallingTrap.GetComponent<fallingSpikes>().collisionMask = LayerMask.GetMask("Nothing");
        fallingTrap.GetComponent<fallingSpikes>().fallingSpeed = fallSpeed;
        return;
    }
}
