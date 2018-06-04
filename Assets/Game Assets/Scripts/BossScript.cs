using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour {
    [HideInInspector]
    public int stage = 0;
    public GameObject eyes;
    public List<GameObject> attacks = new List<GameObject>();
    float diff = 10f;

    public void Hit() {
        float health = GameObject.Find("BossHealthBarSlider").GetComponent<Slider>().value;
        ParticleSystem attack1 = attacks[0].GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule em = attack1.emission;

        GameObject.Find("BossHealthBarSlider").GetComponent<Slider>().value -= 0.01f / (PlayerPrefs.GetFloat("Difficulty") * 60f);
        if (health <= 0f) {
            DontDestroyOnLoad(GameObject.Find("zucc"));
            GameObject.Find("zucc").GetComponent<AudioSource>().Play();
            GameObject.FindWithTag("Player").GetComponent<Player>().LoadLevel(17);
            return;
        } else if (health <= 0.1f && stage == 3) {
            attack1.Emit(100);
            stage = 4;
        } else if (health <= 0.5f && stage == 2) {
            eyes.SetActive(true);
            attack1.Emit(75);
            em.enabled = false;
            em = attacks[2].GetComponent<ParticleSystem>().emission;
            em.enabled = true;
            attacks[1].GetComponent<ParticleSystem>().Emit(25);
            diff += 5f;
            stage = 3;
        } else if (health <= 0.75f && stage == 1) {
            attack1.Emit(50);
            stage = 2;
        } else if (health <= 0.9f && stage == 0) {
            attack1.Emit(25);
            stage = 1;
        }
    }

    private void incDifficulty() {
        diff += Time.deltaTime / 2f;
        ParticleSystem.EmissionModule em = GameObject.Find("BulletHell").GetComponent<ParticleSystem>().emission;
        em.rateOverTime = diff;
    }

    private void Update() {
        incDifficulty();
    }
}