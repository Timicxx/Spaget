using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour {
    public int stage = 0;

    public void Hit() {
        float health = GameObject.Find("BossHealthBarSlider").GetComponent<Slider>().value;
        ParticleSystem.EmissionModule em = GameObject.Find("BulletHell").GetComponent<ParticleSystem>().emission;

        GameObject.Find("BossHealthBarSlider").GetComponent<Slider>().value -= 0.01f / (PlayerPrefs.GetFloat("Difficulty") * 30f);
        if (health <= 0f) {
            DontDestroyOnLoad(GameObject.Find("zucc"));
            GameObject.Find("zucc").GetComponent<AudioSource>().Play();
            GameObject.FindWithTag("Player").GetComponent<Player>().LoadLevel(17);
        } else if (health <= 0.1f && stage == 3) {
            GameObject.Find("BulletHell").GetComponent<ParticleSystem>().Emit(50);
            stage = 4;
        } else if (health <= 0.5f && stage == 2) {
            GameObject.Find("BulletHell").GetComponent<ParticleSystem>().Emit(30);
            stage = 3;
        } else if (health <= 0.75f && stage == 1) {
            GameObject.Find("BulletHell").GetComponent<ParticleSystem>().Emit(20);
            stage = 2;
        } else if (health <= 0.9f && stage == 0) {
            GameObject.Find("BulletHell").GetComponent<ParticleSystem>().Emit(10);
            stage = 1;
        }
    }
}