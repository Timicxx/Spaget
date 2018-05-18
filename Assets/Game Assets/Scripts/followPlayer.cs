using UnityEngine;
using UnityEngine.SceneManagement;

public class followPlayer : MonoBehaviour {
    public bool notCustom = true;
    GameObject player;
    Vector3 playerLocation;

	void Start () {
        player = GameObject.FindWithTag("Player");	
        if(SceneManager.GetActiveScene().name == "custom") {
            notCustom = false;
        }
	}
	
	void Update () {
        if (!notCustom) {
            Stretch();
        } else {
            playerLocation = player.transform.position;
            playerLocation.y += 4.18f;
            playerLocation.z = -15;
            transform.position = playerLocation;
        }

	}

    void Stretch() {
        try {
            if (GameObject.Find("WorldGen").GetComponent<WorldGen>().stretch) {
                playerLocation = player.transform.position;
                playerLocation.y = 18;
                playerLocation.z = -15;
                transform.position = playerLocation;
            }
        } catch {
            Destroy(this);
        }
    }
}
