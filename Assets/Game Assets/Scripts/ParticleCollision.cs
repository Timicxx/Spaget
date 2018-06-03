using UnityEngine;

public class ParticleCollision : MonoBehaviour {
    public GameObject Mark;
    public GameObject Boss;

    private void OnParticleCollision(GameObject other) {
        if(other.tag == "Mark") {
            Mark.GetComponent<Attack>().Hit();
        }else if(other.tag == "Boss") {
            Boss.GetComponent<BossScript>().Hit();
        } 
    }
}