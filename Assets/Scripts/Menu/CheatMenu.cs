using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheatMenu : MonoBehaviour
{
    public GameObject cheatMenu;
    public Button btn;
    public InputField inputText;
    public static bool isCheatOpened;
    public static bool nodamage = false;
    public static bool hitkill = false;
    public static bool speed2x = false;
    public static bool fullhppet = false;
    public static bool motherlode = false;
    public static bool killpet = false;

    [SerializeField] Animator animPlayer;
    [SerializeField, Range(0.01f, 2f)] float animSpeedControl = 1f;
    // Start is called before the first frame update
    void Start()
    {
        cheatMenu.SetActive(false);
        isCheatOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Comma) && !GameManager.gameManager.isShopping) {
            if (isCheatOpened) {
                ResumeGame();
            } else {
                OpenCheatMenu();
            }
        }
    }

    public void GetInputOnClickHandler() {
        string cheatInput = inputText.text;
        toggleBoolCheat(cheatInput);
        ResumeGame();
    }

    public void toggleBoolCheat(string cheatInput) {
        // make bool hitkill, nodamage, etc cheat true if false and vice versa
        if (cheatInput == "rambo") {
            hitkill = !hitkill;
        } else if (cheatInput == "immortal") {
            nodamage = !nodamage;
        } else if (cheatInput == "kecepatanusainbolt") {
            speed2x = !speed2x;
            animSpeedControl = speed2x ? 2f : 1f;
            animPlayer.SetFloat("animSpeed", animSpeedControl);
        } else if (cheatInput == "fullhppet") {
            fullhppet = !fullhppet;
        } else if (cheatInput == "motherlode") {
            motherlode = !motherlode;
        } else if (cheatInput == "killpet") {
            killpet = !killpet;
        }
    }

    public void OpenCheatMenu() {
        cheatMenu.SetActive(true);
        Time.timeScale = 0f;
        isCheatOpened = true;

        // Focus on input field
        inputText.ActivateInputField();
    }

    public void ResumeGame() {
        cheatMenu.SetActive(false);
        if (!QuestMenu.isQuestOpened && !PauseMenu.isPaused) {
            Time.timeScale = 1f;
        }
        isCheatOpened = false;
    }
}
