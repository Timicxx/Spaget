using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorldGen : MonoBehaviour {

    public bool stretch = false;
    public colorPrefabArray[] colorMappings;
    private Texture2D sceneMap;
    private List<Vector3> platformPos = new List<Vector3>();

	void Awake () {
        sceneMap = new Texture2D(2, 2);
        string path = PlayerPrefs.GetString("sceneMapPath");
        if(path == "WEBGL") {
            Sprite sprite = (Sprite)Resources.Load("sceneMap");
            sceneMap = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            var pixels = sprite.texture.GetPixels(
                                         (int)sprite.textureRect.x,
                                         (int)sprite.textureRect.y,
                                         (int)sprite.textureRect.width,
                                         (int)sprite.textureRect.height);
            sceneMap.SetPixels(pixels);
            sceneMap.Apply();
        } else {
            sceneMap.LoadImage(File.ReadAllBytes(path));
        }
    }

    private void Start() {
        Generate();
        CameraRecalculation();
    }

    private void Generate() {
        for (int i = 0; i < sceneMap.width; i++) {
            for (int j = 0; j < sceneMap.height; j++) {
                Tile(i, j);
            }
        }
        try {
            SetPlatformWaypoints();
        } catch {
            Debug.Log("No platform!");
        }
       
    }

    void Tile(int x, int y) {
        Color pixelColor = sceneMap.GetPixel(x, y);

        if(pixelColor.a == 0) {
            return;
        }

        if (pixelColor == Color.white) {
            stretch = true;
            return;
        }

        foreach (colorPrefabArray colorMapping in colorMappings) {
            Vector2 pos = new Vector2(x, y);

            if (colorMapping.color.Equals(pixelColor)) {
                if (colorMapping.color == Color.magenta) {
                    if (GameObject.FindWithTag("Platform") == null) {
                        Instantiate<GameObject>(colorMapping.prefab, Vector2.zero, Quaternion.identity, null);
                    }
                    platformPos.Add(pos);
                    return;
                }

                GameObject obj = Instantiate<GameObject>(colorMapping.prefab, pos, Quaternion.identity, transform);

                if(obj.transform.name == "Spikes(Clone)") {
                    int polarity = x >= sceneMap.width / 2 ? 1 : -1;
                    obj.transform.rotation = Quaternion.Euler(0, 0, 90 * polarity);
                }
            }
        }
    }

    void SetPlatformWaypoints() {
        Vector3[] wayPoints = new Vector3[platformPos.Count];

        for (int i = 0; i < platformPos.Count; i++) {
            wayPoints[i] = platformPos[i];
        }

        GameObject.FindWithTag("Platform").GetComponent<platformController>().localWaypoints = wayPoints;
    }

    void CameraRecalculation() {
        float width = stretch ? (16 / 9) * sceneMap.height : sceneMap.width;
        Camera.main.orthographicSize = width * Screen.height / Screen.width * 0.5f;
        float x = stretch ? (GameObject.FindWithTag("Player").transform.position.x) : (sceneMap.width / 2);
        float y = stretch ? (Camera.main.rect.yMin) : (sceneMap.height / 2);
        Camera.main.transform.position = new Vector3(x, y, -10);
        Camera.main.gameObject.AddComponent<followPlayer>();
    }
}
