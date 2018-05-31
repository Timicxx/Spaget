using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllMightController : MonoBehaviour {

    public float moveSpeed = 5f;
    Animator anim;
    Rigidbody rb;
    PlayerInfo inf;

    void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        inf.Reset();
    }

    void Update() {
        InputController();
        Move();
    }

    private void Move() {
        Vector3 vel = rb.velocity;
        Vector3 rot = transform.localEulerAngles;

        //Move
        if (inf.isMoving) {
            anim.SetBool("Running", true);
            vel.x = moveSpeed * inf.direction;
            if(inf.direction == 1) {
                rot.y = 90;
            } else {
                rot.y = 270;
            }
        } else {
            anim.SetBool("Running", false);
        }
        //Jump
        if (inf.isJumping) {
            rb.AddForce(Vector3.up, ForceMode.Impulse);
            Debug.Log("Triggered Jump!");
            anim.SetTrigger("Jump");
        }
        //Apply
        rb.velocity = vel;
        transform.localEulerAngles = rot;
        //Reset
        inf.Reset();
    }

    private void InputController() {
        Vector2 inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetKeyDown(KeyCode.Space) ? 1 : 0);
        if(inputVector.x != 0) {
            inf.isMoving = true;
            if(inputVector.x > 0) {
                inf.direction = 1;
            } else {
                inf.direction = -1;
            }
        }
        if(inputVector.y != 0) {
            inf.isJumping = true;
        }
    }

    struct PlayerInfo {
        public bool isGrounded;
        public bool isMoving;
        public bool isJumping;
        public bool isFalling;

        public int direction;

        public void Reset() {
            isGrounded = isMoving = isJumping = isFalling = false;
        }
    }
}
