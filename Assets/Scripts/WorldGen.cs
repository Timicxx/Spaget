using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldGen : MonoBehaviour {

    public Texture2D sceneMap;
    public colorPrefabArray[] colorMappings;

	void Awake () {
        sceneMap = new Texture2D(2, 2);
        string path = PlayerPrefs.GetString("sceneMapPath");
        sceneMap.LoadImage(File.ReadAllBytes(path));

        Camera.main.orthographicSize = sceneMap.width * Screen.height / Screen.width * 0.5f;
        Camera.main.transform.position = new Vector3((sceneMap.width / 2), (sceneMap.height / 2) - 0.5f, -10);
    }

    private void Start() {
        Debug.Log(sceneMap.width + "x" + sceneMap.height);
        Generate();
    }

    private void Generate() {
        for (int i = 0; i < sceneMap.width; i++) {
            for (int j = 0; j < sceneMap.height; j++) {
                Tile(i, j);
            }
        }
    }

    void Tile(int x, int y) {
        Color pixelColor = sceneMap.GetPixel(x, y);

        if(pixelColor.a == 0) {
            return;
        }

        foreach (colorPrefabArray colorMapping in colorMappings) {

            if (colorMapping.color.Equals(pixelColor)) {
                Vector2 pos = new Vector2(x, y);
                GameObject obj = Instantiate<GameObject>(colorMapping.prefab, pos, Quaternion.identity, transform);

                if(obj.transform.name == "Spikes(Clone)") {
                    int polarity = x >= sceneMap.width / 2 ? 1 : -1;
                    obj.transform.rotation = Quaternion.Euler(0, 0, 90 * polarity);
                }
            }
        }
        
    }
}
