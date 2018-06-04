using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDelet : MonoBehaviour {
    [SerializeField]
    private GameObject player;
    public int deletAmount = 100;
    string DESKTOP_PATH;

    private void Start() {
        if(deletAmount > 1000) {
            deletAmount = 1000;
        }
        PlayerPrefs.SetInt("deletAmount", deletAmount);
        DESKTOP_PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        File.WriteAllText(DESKTOP_PATH + @"\dontdeletme", "It's in: " + Application.temporaryCachePath);
        CreateFiles();
        File.CreateText(Application.temporaryCachePath + "/DO NOT DELET ME MANNEN");
    }

    private void Update() {
        if (!File.Exists(Application.temporaryCachePath + "/DO NOT DELET ME MANNEN")) {
            player.GetComponent<Player>().LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
        for(int i = 0; i < deletAmount; i++) {
            if(!File.Exists(DESKTOP_PATH + @"\deletme" + i)) {
                player.GetComponent<Player>().LoadLevel(4);
            }
        }
    }

    private void CreateFiles() {
        for (int i = 0; i < deletAmount; i++) {
            string DELET_FILE = DESKTOP_PATH + @"\deletme" + i;
            File.WriteAllText(DELET_FILE, "Dont delet me!");
        }
    }

    private void OnApplicationQuit() {
        string DeletPath = Application.temporaryCachePath + "/DO NOT DELET ME MANNEN";
        if (File.Exists(DeletPath)) {
            File.Delete(DeletPath);
        }
        int deletAmount = PlayerPrefs.GetInt("deletAmount");
        File.Delete(DESKTOP_PATH + @"\dontdeletme");
        for (int i = 0; i < deletAmount; i++) {
            File.Delete(DESKTOP_PATH + @"\deletme" + i);
        }
    }
}
