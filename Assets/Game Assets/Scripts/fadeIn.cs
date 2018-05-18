using UnityEngine;
using TMPro;

public class fadeIn : MonoBehaviour {

    void Update () {
        Color color = GetComponent<TextMeshProUGUI>().color;
        color.a += Time.deltaTime * (1f/1.5f);
        GetComponent<TextMeshProUGUI>().color = color;
    }
}
