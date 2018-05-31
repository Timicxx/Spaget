using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllMightController : MonoBehaviour {

    public float moveSpeed = 5f;
    Animator anim;
    Rigidbody rb;
    PlayerInfo playerInfo;
    float distToGnd;

    void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerInfo.Reset();
        distToGnd = GetComponent<BoxCollider>().bounds.extents.y;
    }

    void Update() {
        InputController();
        Move();

        playerInfo.Reset();
    }

    private void Move() {
        Vector3 vel = rb.velocity;
        Vector3 rot = transform.localEulerAngles;

        if (playerInfo.isMoving && playerInfo.IsGrounded(transform.position, distToGnd)) {
            vel.x = moveSpeed * playerInfo.direction;
            if(playerInfo.direction == 1) {
                rot.y = 90;
            } else {
                rot.y = 270;
            }
        }
        if (playerInfo.isJumping && playerInfo.IsGrounded(transform.position, distToGnd)) {
            rb.AddForce(Vector3.up, ForceMode.Impulse);
        }
        rb.velocity = vel;
        transform.localEulerAngles = rot;
    }

    private void InputController() {
        Vector2 inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Jump"));
        if(inputVector.x != 0) {
            playerInfo.isMoving = true;
            playerInfo.direction = inputVector.x > 0 ? 1 : -1;
        }
        if(inputVector.y != 0) {
            playerInfo.isJumping = true;
        }
    }

    struct PlayerInfo{
        public bool isJumping;
        public bool isMoving;
        public bool isFalling;

        public int direction;

        public void Reset() {
            isJumping = isMoving = isFalling = false;
            direction = 0;
        }

        public bool IsGrounded(Vector3 pos, float distToGnd) {
            return Physics.Raycast(pos, -Vector3.up, distToGnd + 0.1f);
        }
    }
}
