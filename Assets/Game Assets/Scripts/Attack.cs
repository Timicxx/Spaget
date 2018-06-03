using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    public GameObject particleSys;
	
	void Update () {
        ParticleSystem.EmissionModule em = particleSys.GetComponent<ParticleSystem>().emission;
        if (Input.GetKey(KeyCode.Z)) {
            em.enabled = true;
        } else {
            em.enabled = false;
        }
	}

    public void Hit() {
        List<GameObject> hearts = new List<GameObject>();
        foreach(Transform child in GameObject.Find("PlayerHealthBar").transform) {
            hearts.Add(child.gameObject);
        }
        hearts = GameObject.FindGameObjectsWithTag("Heart").OrderBy(go => go.name).ToList();
        if (hearts.Count == 1) {
            Destroy(hearts[0]);
            PlayerPrefs.SetInt("lastScene", 16);
            GetComponent<Player>().LoadLevel(1);
        } else {
            Destroy(hearts[0]);
        }
    }
}
