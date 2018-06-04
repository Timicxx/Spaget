using UnityEngine;

public class ParticleCollision : MonoBehaviour {
    public GameObject Mark;
    public GameObject Boss;

    private void OnParticleCollision(GameObject other) {
        switch (other.tag) {
            case "Mark":
                Mark.GetComponent<Attack>().Hit();
                break;
            case "Boss":
                Boss.GetComponent<BossScript>().Hit();
                break;
        }
    }
}