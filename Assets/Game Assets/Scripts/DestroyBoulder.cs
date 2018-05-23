using UnityEngine;

public class DestroyBoulder : MonoBehaviour {
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
