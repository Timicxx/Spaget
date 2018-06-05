using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    public GameObject particleSys;
    public GameObject hitbox;
    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
    }

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
        if(hearts == null){
            return;
        }
        hearts = GameObject.FindGameObjectsWithTag("Heart").OrderBy(go => go.name).ToList();
        if (hearts.Count == 1) {
            Destroy(hearts[0]);
            PlayerPrefs.SetInt("lastScene", 15);
            GetComponent<Player>().LoadLevel(1);
        } else {
            Destroy(hearts[0]);
            StartCoroutine(MakeInvincible(2));
        }
    }

    private IEnumerator MakeInvincible(float sec) {
        float currentTime = 0f;
        while (currentTime < sec) {
            anim.Play("Blink");
            hitbox.GetComponent<CircleCollider2D>().enabled = false;
            currentTime += Time.deltaTime;
            yield return null;
        }
        hitbox.GetComponent<CircleCollider2D>().enabled = true;
    }
}
