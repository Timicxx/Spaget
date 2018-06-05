using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {
    Controller2D ctrl2D;
    public float maxJumpHeight = 5f;
    public float minJumpHeight = 1f;
    public float jumpTime = .5f;
    public float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;
    public float moveSpeed = 5f;
    float velocityXSmoothing;
    [HideInInspector]
    public Vector3 velocity;
    Vector2 input;
    [HideInInspector]
    public bool changeGravity = false;

    private void Start() {
        ctrl2D = GetComponent<Controller2D>();
        if (SceneManager.GetActiveScene().name == "3") {
            changeGravity = true;
        } else {
            changeGravity = false;
        }
        gravity = -(2 * maxJumpHeight / Mathf.Pow(jumpTime, 2));
        maxJumpVelocity = Mathf.Abs(gravity * jumpTime);
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    private void Update() {
        if (PauseMenu.isPaused) {
            return;
        }
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        InputController();
        float TargetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, TargetVelocityX, ref velocityXSmoothing, (ctrl2D.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        ctrl2D.Move(velocity * Time.deltaTime);
    }

    private void InputController() {
        Scene CurrentScene = SceneManager.GetActiveScene();

        if (ctrl2D.collisions.above || ctrl2D.collisions.below) {
            if (ctrl2D.collisions.slidingDownSlope) {
                velocity.y += ctrl2D.collisions.slopeNormal.y * -gravity * Time.deltaTime;
            } else {
                velocity.y = 0;
            } 
        }
        if (changeGravity && Input.GetKeyDown("space") == true) {
            gravity *= -1;
        }
        if (Input.GetKeyDown(KeyCode.Space) && ctrl2D.collisions.below && !changeGravity) {
            velocity.y = maxJumpVelocity;
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            if(velocity.y > minJumpVelocity) {
                velocity.y = minJumpVelocity;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.BackQuote) && CurrentScene.name != "gameOver" && CurrentScene.name != "Win") {
            GameObject.FindWithTag("nextLevel").GetComponent<nextLevel>().skipStage();
            return;
        }

    }

    public void HitHandler(RaycastHit2D hit, GameObject trap) {
        if (hit.transform.tag == "Spike" || hit.transform.tag == "Boulder") {
            PlayerPrefs.SetInt("lastScene", SceneManager.GetActiveScene().buildIndex);
            LoadLevel(1);
            hit.transform.gameObject.layer = LayerMask.GetMask("Nothing");
        }
        if (hit.transform.tag == "nextLevel") {
            if(SceneManager.GetActiveScene().name == "2") {
                hit.transform.GetComponent<nextLevel>().DestroyMe();
                return;
            }
            PlayerPrefs.SetInt("lastScene", SceneManager.GetActiveScene().buildIndex);
            hit.transform.GetComponent<nextLevel>().skipStage();
            Destroy(hit.transform.gameObject);
        }
        if (hit.transform.tag == "Trap") {
            Destroy(hit.transform.gameObject);
            if(SceneManager.GetActiveScene().name == "7") {
                GameObject.Find("Sideways Trap").GetComponent<sidewaysSpike>().speed = -19.62f;
                GameObject[] objs = GameObject.FindGameObjectsWithTag("Trap");
                for (int i = 0; i < objs.Length; i++) {
                    Destroy(objs[i]);
                }
                return;
            }
            GameObject fallingTrap = Instantiate<GameObject>(trap);
            if(SceneManager.GetActiveScene().name == "3") {
                fallingTrap.GetComponent<fallingSpikes>().collisionMask = LayerMask.GetMask("Nothing");
            }
            Vector3 pos = GameObject.FindWithTag("Player").transform.position;
            fallingTrap.transform.position = new Vector3(pos.x, pos.y + 5f, 1f);
        }
    }

    public void LoadLevel(int level) {
        StartCoroutine(OperationLoader(level));
    }

    private IEnumerator OperationLoader(int level) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);
        while (!operation.isDone) {
            Debug.Log(Mathf.Clamp01(operation.progress / 0.9f) * 100  + "%");
            yield return null;
        }
    }
}
