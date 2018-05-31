using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllMightController : MonoBehaviour {

    public float moveSpeed = 5f;
    public LayerMask collisionMask;
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
        RaycastController();
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
        //Falling
        if (inf.isFalling) {
            anim.SetBool("Falling", true);
        } else {
            anim.SetBool("Falling", false);
        }
        //Jump
        if (inf.isJumping) {
            vel.y += moveSpeed * 5f;
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
        if (Input.GetKeyDown(KeyCode.P)) {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void RaycastController() {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position;

        if(Physics.Raycast(rayOrigin, Vector3.down, out hit, 2f, collisionMask)) {
            if(hit.distance < 0.5f && hit.transform.tag == "Ground") {
                inf.isGrounded = true;
            }
            if(hit.distance > 1f) {
                inf.isFalling = true;
            }
            if(hit.distance < 0.25f) {
                anim.SetTrigger("Land");
            }
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
