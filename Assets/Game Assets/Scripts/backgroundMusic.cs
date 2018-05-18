using UnityEngine;
using UnityEngine.SceneManagement;

public class backgroundMusic : MonoBehaviour {

    private void Awake() {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Music");
        if(objects.Length > 1) {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update() {
        if(SceneManager.GetActiveScene().buildIndex == 0) {
            Destroy(this.gameObject);
        }
    }
}
