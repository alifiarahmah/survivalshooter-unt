using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSlotMenu : MonoBehaviour
{
    // Start is called before the first frame update
    string action;

    public PlayerDataManager pdm;
    List<bool> availableSlot;
    public GameObject saveSlotUIHighlight;

    private void Awake()
    {

        action = PlayerPrefs.GetString("saveLoadAction");

        if (action == "nextQuest")
        {
            saveGame(-1);
            loadGame(-1);

            continueNextStage();
        }
    }

    void Start()
    {
        availableSlot = pdm.availableSlot;
        Debug.Log(availableSlot[0].ToString() + availableSlot[1].ToString() + availableSlot[2].ToString());

        for(int i = 0; i<3; i++)
        {
            GameObject slot = saveSlotUIHighlight.transform.GetChild(i).gameObject;
            slot.SetActive(!availableSlot[i]); 
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickBackButton()
    {
        if (action == "save")
        {
            continueNextStage();
        }
        if (action == "load")
        {
            backToMainMenu();
        }

    }
    void continueNextStage()
    {
        string nameScene = PlayerPrefs.GetString("lastScene");
        LoadNextScene(nameScene);
    }

    void backToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void onClickSaveSlot(int slot)
    {
        PlayerPrefs.SetInt("SavedSlot", slot);

        if (action == "save")
        {
            saveGame(slot);
            loadGame(slot);
        }
        if (action == "load" && availableSlot[slot])
        {
            loadGame(slot);
        }
    }

    void saveGame(int slot)
    {
        // save data
        float timeElapsed = PlayerPrefs.GetFloat("timeElapsed");
        string nameScene = PlayerPrefs.GetString("lastScene");
        string name = PlayerPrefs.GetString("username");
        PetData petData = GameManager.gameManager.currentPet;
        int coins = GameManager.gameManager.coins;
        List<bool> availableWeapon = GameManager.gameManager.availableWeapon;

        pdm.SetTime(slot+1, timeElapsed);
        pdm.SetLastScene(slot + 1, nameScene);
        pdm.SetName(slot + 1, name);
        pdm.SetCoins(slot + 1, coins);
        pdm.SetPetData(slot + 1, petData);
        pdm.SetAvailableWeapons(slot + 1, availableWeapon);

        pdm.SavePlayerData(slot+1);
    }

    void loadGame(int slot)
    {
        float time = pdm.GetTime(slot + 1);
        int isWin = 0;
        string lastScene = pdm.GetLastScene(slot + 1);

        PlayerPrefs.SetFloat("timeElapsed", time);
        PlayerPrefs.SetInt("isWin", isWin);
        PlayerPrefs.SetString("lastScene", lastScene);
        PlayerPrefs.SetInt("activeSlot", slot);

        SceneManager.LoadScene(GetNextScene(lastScene));
    }

    public string GetNextScene(string nameScene)
    {
        // load scene
        string sceneNext = "Level_01";
        if (nameScene != null)
        {
            if (nameScene == "Level_01")
            {
                sceneNext = "ShopCutscene";
            }
            else if (nameScene == "Level_02")
            {
                sceneNext = "Level_Final";
            }
            else if (nameScene == "Level_Final")
            {
                sceneNext = "GameOver";
            }
        }

        return sceneNext;
    }

    public void LoadNextScene(string nameScene)
    {
        // load scene
        string sceneLoaded = GetNextScene(nameScene);
        if (sceneLoaded != "") SceneManager.LoadScene(sceneLoaded);
    }

}
