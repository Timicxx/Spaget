using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour {

    public Dropdown drop;

    public void Difficulty() {
        float value;
        switch (drop.value) {
            case 0:
                value = 0.5f;
                break;
            case 1:
                value = 1.0f;
                break;
            case 2:
                value = 1.25f;
                break;
            case 3:
                value = 1.5f;
                break;
            case 4:
                value = 2.0f;
                break;
            case 5:
                value = 5.0f;
                break;
            default:
                value = 1.0f;
                break;
        }
        PlayerPrefs.SetFloat("Difficulty", value);
    }

    public void DeleteAllPlayerPrefs() {
        PlayerPrefs.DeleteAll();
    }
}
