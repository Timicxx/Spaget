using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collision : MonoBehaviour {

    public void HitHandler(RaycastHit2D hit, List<GameObject> traps) {
        if (hit.transform.tag == "Spike") {
            SceneManager.LoadScene("gameOver");
            return;
        }
        if (hit.transform.tag == "nextLevel") {
            hit.transform.GetComponent<nextLevel>().DestroyMe();
            return;
        }
        if (hit.transform.tag == "Trap") {
            if (SceneManager.GetActiveScene().name == "1") {
                Destroy(hit.transform.gameObject);
                GameObject fallingTrap = Instantiate<GameObject>(traps[0]);
                fallingTrap.transform.position = new Vector3(-4.97f, 4.35f, 1f);
                return;
            }
        }
    }
}
