using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingSpikes : MonoBehaviour {

    public float fallingSpeed = -9.81f;
    Vector3 pos;
    public LayerMask collisionMask;

    void Update () {
        pos = transform.position;
        pos.y += fallingSpeed * Time.deltaTime;
        transform.position = pos;

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(pos.x, pos.y - 0.8f), Vector2.down, 2, collisionMask);
        Debug.DrawRay(new Vector2(pos.x, pos.y - 0.8f), Vector2.down * 2, Color.red);

        if (hit) {
            if(hit.distance <= 0.1f && hit.transform.tag == "Platform") {
                transform.SetParent(hit.transform);
                fallingSpeed = 0f;
                return;
            }
            if(hit.distance <= 0.1f) {
                fallingSpeed = 0f;
            } else {
                fallingSpeed = -9.81f;
            }
        }
    }
}
