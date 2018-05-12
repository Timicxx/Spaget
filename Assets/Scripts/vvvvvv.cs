using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class vvvvvv : MonoBehaviour {

    public bool gravityChange = false;
	
	void Update () {
		if(SceneManager.GetActiveScene().name == "3") {
            gravityChange = true;
        } else {
            gravityChange = false;
        }
	}
}
