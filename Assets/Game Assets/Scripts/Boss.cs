using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    [Range(0f, 0.05f)]
    public float inc_val;
    public float extra_x = 0f;
    public float extra_y = 0f;

    float xoff1 = 0f;
    float xoff2 = 1000f;
    float diff = 10f;

    void Update () {
        incDifficulty();
        float perlin1 = map(Mathf.PerlinNoise(xoff1, xoff1), 0, 1, -4f, 4f);
        float perlin2 = map(Mathf.PerlinNoise(xoff2, xoff2), 0, 1, -2f, 2f);

        transform.localPosition = new Vector2(perlin1 + extra_x, perlin2 + extra_y);

        xoff1 += inc_val;
        xoff2 += inc_val;
	}

    private void incDifficulty() {
        diff += Time.deltaTime/5f;
        ParticleSystem.EmissionModule em = GameObject.Find("BulletHell").GetComponent<ParticleSystem>().emission;
        em.rateOverTime = diff;
    }

    private float map(float s, float a1, float a2, float b1, float b2) {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
