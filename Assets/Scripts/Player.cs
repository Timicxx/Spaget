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
    bool changeGravity = false;

    private void Start() {
        ctrl2D = GetComponent<Controller2D>();
        if (SceneManager.GetActiveScene().name == "3") {
            changeGravity = true;
        } else {
            changeGravity = false;
        }
        gravity = -(2 * jumpHeight / Mathf.Pow(jumpTime, 2));
        jumpVelocity = Mathf.Abs(gravity * jumpTime);
        print("Gravity: " + gravity);
        print("Jump Velocity: " + jumpVelocity);
    }

    private void Update() {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        InputController();
        float TargetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, TargetVelocityX, ref velocityXSmoothing, (ctrl2D.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        ctrl2D.Move(velocity * Time.deltaTime);
    }

    private void InputController() {
        if (ctrl2D.collisions.above || ctrl2D.collisions.below) {
            velocity.y = 0;
        }
        if (changeGravity && Input.GetKeyDown("space") == true) {
            gravity *= -1;
        }
        if (Input.GetKeyDown("space") == true && ctrl2D.collisions.below && !changeGravity) {
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

    }

    public void HitHandler(RaycastHit2D hit, List<GameObject> traps) {
        if (hit.transform.tag == "Spike") {
            PlayerPrefs.SetInt("lastScene", SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene("gameOver");
            return;
        }
        if (hit.transform.tag == "nextLevel") {
            if(SceneManager.GetActiveScene().name == "2") {
                hit.transform.GetComponent<nextLevel>().DestroyMe();
                return;
            }
            PlayerPrefs.SetInt("lastScene", SceneManager.GetActiveScene().buildIndex);
            hit.transform.GetComponent<nextLevel>().skipStage();
            return;
        }
        if (hit.transform.tag == "Trap") {
            if (SceneManager.GetActiveScene().name == "1") {
                Destroy(hit.transform.gameObject);
                GameObject fallingTrap = Instantiate<GameObject>(traps[0]);
                Vector3 pos = GameObject.FindWithTag("Player").transform.position;
                fallingTrap.transform.position = new Vector3(pos.x, pos.y + 5f, 1f);
                return;
            }
        }
    }

}
