using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestOver : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerDataManager pdm;
    void Start() {
    }

    public void SaveAndNext()
    {
        // TODO : Set player prefs savedSlot when load game choosing slot
        PlayerPrefs.SetString("saveLoadAction", "save");
        SceneManager.LoadScene("SavedSlot");
    }

    public void Next()
    {
        PlayerPrefs.SetString("saveLoadAction", "nextQuest");
        SceneManager.LoadScene("SavedSlot");

        //string nameScene = PlayerPrefs.GetString("lastScene");
        //LoadNextScene(nameScene);
    }

    public string GetNextScene(string nameScene) {
        PlayerPrefs.SetInt("isWin", 0);
        // load scene
        string sceneNext = "Level_01";
        if (nameScene != null) {
            if (nameScene == "Level_01") {
                sceneNext = "ShopCutscene";
            } else if (nameScene == "Level_02") {
                sceneNext = "Level_Final";
            }
        }
        return sceneNext;
    }

    public void LoadNextScene(string nameScene) {
        // load scene
        string sceneLoaded = GetNextScene(nameScene);
        if (sceneLoaded != "") SceneManager.LoadScene(sceneLoaded);
    }
}
