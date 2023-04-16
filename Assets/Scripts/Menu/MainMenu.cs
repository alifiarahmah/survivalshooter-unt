using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Settings;
    public AudioSource BgMusic;

    public PlayerDataManager pdm;
    public GameObject scoreboardUI;
    public GameObject mainMenuUI;


    void Start()
    {
        Settings.SetActive(false);
        BgMusic.volume = PlayerPrefs.GetFloat("musicVolume");
        BgMusic.Play(0);

        string openAction = PlayerPrefs.GetString("openMainMenu");
        if (openAction == "openLeaderboard")
        {
            ScoreBoard();
            PlayerPrefs.SetString("openMainMenu", "");
        }

    }

    public string GetNextScene(string nameScene) {
        // load scene
        string sceneNext = "Level_01";
        if (nameScene != null) {
            if (nameScene == "Level_01") {
                sceneNext = "ShopCutscene";
            } else if (nameScene == "Level_02") {
                sceneNext = "Level_Final";
            } else if (nameScene == "Level_Final") {
                sceneNext = "Ending";
            }
        }
        return sceneNext;
    }

    public void NewGame()
    {
        // pdm.Clear(PlayerPrefs.GetInt("savedSlot"));
        pdm.ClearTemp();
        PlayerPrefs.SetInt("activeSlot", 0);
        PlayerPrefs.DeleteKey("isWin");
        PlayerPrefs.SetString("lastScene", "Level_01");
        PlayerPrefs.DeleteKey("timeElapsed");

        SceneManager.LoadScene("Opening");
    }

    public void LoadGame() {
        // TODO : Set player prefs savedSlot when load game choosing slot
        PlayerPrefs.SetString("saveLoadAction", "load");
        SceneManager.LoadScene("SavedSlot");
    }

    public void ScoreBoard()
    {
        scoreboardUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
