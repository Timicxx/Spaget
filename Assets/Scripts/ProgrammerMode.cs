using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgrammerMode : MonoBehaviour {

    [SerializeField]
    GameObject prog;
    bool progActive = false;

    void Start () {
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            if (progActive) { prog.SetActive(true); } else { prog.SetActive(false); }
            progActive = progActive == true ? false : true;
        }
	}
}
