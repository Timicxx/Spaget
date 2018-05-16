using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingSpikes : MonoBehaviour {

    public float fallingSpeed = -9.81f;
    Vector3 pos;
    public int verticalRays = 16;
    public LayerMask collisionMask;
    private BoxCollider2D col;
    private Vector2 rayOrigin;
    private float verticalRaySpacing;
    Bounds bounds;

    private void Awake() {
        fallingSpeed *= PlayerPrefs.GetFloat("Difficulty");
    }

    void Start() {
        col = GetComponent<BoxCollider2D>();
    }

    void Update () {
        pos = transform.position;
        pos.y += fallingSpeed * Time.deltaTime;
        transform.position = pos;
        Rays();
    }

    private void Rays() {
        float rayLength = 1f;
        Bounds bounds = col.bounds;
        bounds.Expand(.015f);
        verticalRaySpacing = bounds.size.x / (verticalRays - 1);
        for (int i = 0; i < verticalRays; i++) {
            rayOrigin = new Vector2(bounds.min.x, bounds.min.y);
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.down * rayLength, Color.red);

            if (hit) {
                if (hit.distance <= 0.1f && hit.transform.name == "Ground") {
                    fallingSpeed *= 0f;
                }
                rayLength = hit.distance;
            }
        }
    }
}
