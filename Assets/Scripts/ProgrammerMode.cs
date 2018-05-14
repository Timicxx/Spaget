using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgrammerMode : MonoBehaviour {

    [SerializeField]
    GameObject prog;
    bool progActive = false;

    void Start() {
        if (!Debug.isDebugBuild) {
            Destroy(this);
            return;
        }
        prog = Instantiate<GameObject>(prog);
        prog.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            if (progActive) { prog.SetActive(true); } else { prog.SetActive(false); }
            progActive = progActive == true ? false : true;
        }
    }
}
