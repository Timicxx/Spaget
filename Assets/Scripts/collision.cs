using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collision : MonoBehaviour {

    public void HitHandler(RaycastHit2D hit, List<GameObject> traps) {
        if (hit.transform.tag == "Spike") {
            PlayerPrefs.SetInt("lastScene", SceneManager.GetActiveScene().buildIndex);
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
                Vector3 pos = GameObject.FindWithTag("Player").transform.position;
                fallingTrap.transform.position = new Vector3(pos.x, pos.y + 5f, 1f);
                return;
            }
        }
    }
}
