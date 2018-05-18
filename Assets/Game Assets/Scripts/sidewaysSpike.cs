using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sidewaysSpike : MonoBehaviour {

    public bool collision = false;
    public float speed = -9.81f;
    Vector3 pos;
    public int horizontalRays = 16;
    public LayerMask collisionMask;
    private BoxCollider2D col;
    private Vector2 rayOrigin;
    private float horizontalRaySpacing;
    Bounds bounds;

    private void Awake() {
        speed *= PlayerPrefs.GetFloat("Difficulty");
    }

    void Start() {
        col = GetComponent<BoxCollider2D>();
    }

    void Update () {
        pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        Rays();
    }

    private void Rays() {
        float rayLength = 1f;
        Bounds bounds = col.bounds;
        bounds.Expand(.015f);
        horizontalRaySpacing = bounds.size.y / (horizontalRays - 1);
        for (int i = 0; i < horizontalRays; i++) {
            rayOrigin = new Vector2(bounds.min.x, bounds.max.y);
            rayOrigin += Vector2.down * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.left, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.left * rayLength, Color.red);

            if (hit) {
                if(hit.distance < 0.1f && collision) {
                    speed = 0f;
                }
                rayLength = hit.distance;
            }
        }
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
