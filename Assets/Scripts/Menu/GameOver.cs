using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public ScoreboardManager sm;
    public PlayerDataManager pdm;
    public float delaySeconds = 5f;
    void Start() {
        Invoke("LoadMainMenuScene", delaySeconds);
        int isWin = PlayerPrefs.GetInt("isWin");
        if (isWin == 1) {
            // save in scoreboard
            if (sm != null) {
                float timeElapsed = PlayerPrefs.GetFloat("timeElapsed");
                Debug.Log("Time Elapsed: " + timeElapsed);
                sm.AddScore(new Score(PlayerPrefs.GetString("username"), timeElapsed));
                // Save the score data
                sm.SaveScore();
            }
            SceneManager.LoadScene("Scoreboard");
        }
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void TryAgain()
    {
        string nameScene = PlayerPrefs.GetString("lastScene");
        SceneManager.LoadScene(nameScene);
    }

}
