using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {
    Controller2D ctrl2D;
    public float jumpHeight = 5f;
    public float jumpTime = .5f;
    public float gravity;
    float jumpVelocity;
    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;
    public float moveSpeed = 5f;
    float velocityXSmoothing;
    Vector3 velocity;
    Vector2 input;
    bool changeGravity;

    private void Start() {
        ctrl2D = GetComponent<Controller2D>();

        gravity = -(2 * jumpHeight / Mathf.Pow(jumpTime, 2));
        jumpVelocity = Mathf.Abs(gravity * jumpTime);
        print("Gravity: " + gravity);
        print("Jump Velocity: " + jumpVelocity);
    }

    private void Update() {
        changeGravity = GetComponent<vvvvvv>().gravityChange;
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(ctrl2D.collisions.above || ctrl2D.collisions.below) {
            velocity.y = 0;
        }

        if(changeGravity && Input.GetKeyDown("space") == true) {
            gravity *= -1;
        }

        if(Input.GetKeyDown("space") == true && ctrl2D.collisions.below && !changeGravity) {
            velocity.y = jumpVelocity;  
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("Menu");
            return;  
        }

        if (Input.GetKeyDown(KeyCode.P) && SceneManager.GetActiveScene().name != "gameOver") {
            GameObject.FindWithTag("nextLevel").GetComponent<nextLevel>().skipStage();
            return;
        }

        float TargetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, TargetVelocityX, ref velocityXSmoothing, (ctrl2D.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        ctrl2D.Move(velocity * Time.deltaTime);
    }

}
