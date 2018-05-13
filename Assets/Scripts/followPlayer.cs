using UnityEngine;

public class followPlayer : MonoBehaviour {
    GameObject player;
    Vector3 playerLocation;

	void Start () {
        player = GameObject.FindWithTag("Player");	
	}
	
	void Update () {
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
