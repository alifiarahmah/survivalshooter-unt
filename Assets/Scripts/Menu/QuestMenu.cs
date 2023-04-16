using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestMenu : MonoBehaviour
{
    public GameObject questMenu;
    public static bool isQuestOpened;
    // Start is called before the first frame update
    void Start()
    {
        OpenQuestMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !GameManager.gameManager.isShopping) {
            if (isQuestOpened) {
                ResumeGame();
            } else {
                OpenQuestMenu();
            }
        }
    }

    public void OpenQuestMenu() {
        questMenu.SetActive(true);
        Time.timeScale = 0f;
        isQuestOpened = true;
    }

    public void ResumeGame() {
        questMenu.SetActive(false);
        if (!CheatMenu.isCheatOpened && !PauseMenu.isPaused) {
            Time.timeScale = 1f;
        }
        isQuestOpened = false;
    }
}
