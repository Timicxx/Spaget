using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class programmerValues : MonoBehaviour {
    public float[] values;
    Player player;

	void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
	}
	
	void Update () {
        values[0] = player.moveSpeed;
        values[1] = player.jumpHeight;
        values[2] = player.jumpTime;
        values[3] = player.gravity;

        displayValue(0, "moveSpeed");
        displayValue(1, "jumpHeight");
        displayValue(2, "jumpTime");
        displayValue(3, "gravity");
    }

    void displayValue(int index, string valueName) {
        GameObject obj = GameObject.Find(valueName);
        print(valueName);
        obj.GetComponent<TextMeshProUGUI>().text = valueName + " = " + values[index].ToString();
    }
}
