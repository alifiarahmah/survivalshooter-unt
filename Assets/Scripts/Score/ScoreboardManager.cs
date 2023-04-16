using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreboardManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject ScoreBoardUI;
    private ScoreData sd = new ScoreData();
    // Start is called before the first frame update
    void Awake() {
        sd.scoreData = FileHandler.ReadListFromJSONPrefs<Score>("ScoreData.json");
    }

    public IEnumerable<Score> GetHighScores() {
        return sd.scoreData.OrderBy(x => x.score);
    }

    public void AddScore(Score score) {
        sd.scoreData.Add(score);
        SaveScore();
    }

    public void AddScore(string name, float time) {
        Score score = new Score(name, time);
        AddScore(score);
    }

    public void Clear() {
        sd.scoreData.Clear();
        SaveScore();
    }

    
    public void OnDestroy() {
        SaveScore();
    }

    public void SaveScore() {
        FileHandler.SaveToJsonPrefs<Score>(sd.scoreData, "ScoreData.json");
    }

    public void GoToMainMenu()
    {
        mainMenuUI.SetActive(true);
        ScoreBoardUI.SetActive(false);
    }
}
