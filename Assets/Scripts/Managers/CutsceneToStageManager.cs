using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneToStageManager : MonoBehaviour
{
    public string nextSceneName;
    public ScoreboardManager sm;

    void OnEnable() {
        int isWin = PlayerPrefs.GetInt("isWin");
        if (isWin == 1)
        {
            // save in scoreboard
            if (sm != null)
            {
                float timeElapsed = PlayerPrefs.GetFloat("timeElapsed");
                Debug.Log("Time Elapsed: " + timeElapsed);
                sm.AddScore(new Score(PlayerPrefs.GetString("username"), timeElapsed));
                // Save the score data
                sm.SaveScore();
            }
            SceneManager.LoadScene("Scoreboard");
        }

        PlayerPrefs.SetString("openMainMenu", "openLeaderboard");
        SceneManager.LoadScene(nextSceneName);
    }


}
