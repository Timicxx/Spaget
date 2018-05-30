using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllMightController : MonoBehaviour {

    public float speed = 2f;
    Rigidbody2D rb;
    Animator anim;
    PlayerInfo playerInfo;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerInfo.Reset();
    }

    void Update() {
        InputController();
    }

    private void InputController() {
        Vector2 inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Jump"));

    }

    struct PlayerInfo{
        public bool isGrounded;
        public bool isJumping;
        public bool isRunning;
        public bool isFalling;

        public void Reset() {
            isGrounded = isJumping = false;
            isRunning = isFalling = false;
        }
    }
}
