using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public GameObject[] questPlaceholders;
    public Quest[] quests;
    public float enemyModifier = 1f;

    public int zombunnyBounty = 5;
    public int zombearBounty = 10;
    public int hellephantBounty = 15;
    public bool isQuestsComplete = false;
    
    protected TMP_Text titlePlaceholder;
    protected TMP_Text coinValuePlaceholder;
    protected Image completeMark;

    public static QuestManager questManager;
    

    protected void Awake()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.isBoss = false;
        }
        if (questManager == null)
        {
            questManager = this;
        }
        else
        {
            Destroy(questManager);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < quests.Length; i++) {
          quests[i] = GenerateQuest();
        }

        ShowQuestToUI();
    }

    Quest GenerateQuest() {
      int targetZombunny = Random.Range(GenerateLowerBound(), GenerateUpperBound()) * 3;
      int targetZombear = Random.Range(GenerateLowerBound(), GenerateUpperBound()) * 2;
      int targetHellephant = Random.Range(GenerateLowerBound(), GenerateUpperBound());

      if (targetZombunny == 0 && targetZombear == 0 && targetHellephant == 0) {
        targetZombunny = 7;
      }
      
      int totalReward = targetZombunny * zombunnyBounty + targetZombear * zombearBounty + targetHellephant * hellephantBounty;

      QuestObjective questObjective = new QuestObjective(targetZombunny, targetZombear, targetHellephant);
      string questName = GenerateQuestTitle(questObjective, new QuestObjective(0,0,0));

      return new Quest(questName, questObjective, totalReward);
    }

    protected void ShowQuestToUI() {
      for (int i = 0; i < questPlaceholders.Length; i++) {
        titlePlaceholder = questPlaceholders[i].transform.GetChild(0).GetComponent<TMP_Text>();
        coinValuePlaceholder = questPlaceholders[i].transform.GetChild(2).GetComponent<TMP_Text>();

        titlePlaceholder.text = quests[i].title;
        coinValuePlaceholder.text = quests[i].questReward.ToString();
      }
    }

    int GenerateLowerBound() {
      return (int) Mathf.Floor(Random.Range(0f,1f) * enemyModifier);
    }

    int GenerateUpperBound() {
      return (int) Mathf.Ceil(Random.Range(1f,3f) * enemyModifier);
    }

    public void CompleteQuest(int questIdx) {
      completeMark = questPlaceholders[questIdx].transform.GetChild(3).GetComponent<Image>();
      var tempColor = completeMark.color;
      tempColor.a = 1f;
      completeMark.color = tempColor;

      GameManager.gameManager.coins += quests[questIdx].questReward;
      quests[questIdx].isCompleted = true;

      isQuestsComplete = isAllQuestComplete();
      Debug.Log(isQuestsComplete);
      if (isQuestsComplete) {
        string sceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("lastScene", sceneName);
        PlayerPrefs.Save();
        Debug.Log("Last Scene: " + sceneName);
        if (sceneName != "Level_Final") {
          PlayerPrefs.SetInt("isWin", 0);
          GameManager.gameManager.SaveTime();
          SceneManager.LoadScene("QuestOver");
        } else {
          PlayerPrefs.SetInt("isWin", 1);
          GameManager.gameManager.SaveTime();
          SceneManager.LoadScene("Ending");
        }
      }
    }

    protected bool isAllQuestComplete() {
      bool isAllComplete = true;
      for (int i = 0; i < quests.Length; i++) {
        if (!quests[i].isCompleted) {
          isAllComplete = false;
          break;
        }
      }

      return isAllComplete;
    }

    public virtual void UpdateQuestStatus(string enemyType) {
      bool isZombunnyAlocated = false;
      bool isZombearAlocated = false;
      bool isHellephantAlocated = false;
      bool isTitanAlocated = false;

      for (int i = 0; i < quests.Length; i++) {
        switch (enemyType) {
          case "Zombunny":
            if (!isZombunnyAlocated && quests[i].questObjective.Zombunny - quests[i].questProgress.Zombunny > 0) {
              quests[i].questProgress.Zombunny += 1;
              isZombunnyAlocated = true;
            }
            break; 
          case "Zombear":
            if (!isZombearAlocated && quests[i].questObjective.Zombear - quests[i].questProgress.Zombear > 0) {
              quests[i].questProgress.Zombear += 1;
              isZombearAlocated = true;
            }
            break; 
          case "Hellephant":
            if (!isHellephantAlocated && quests[i].questObjective.Hellephant - quests[i].questProgress.Hellephant > 0) {
              quests[i].questProgress.Hellephant += 1;
              isHellephantAlocated = true;
            }
            break;
          case "Titan":
            if (!isTitanAlocated && quests[i].questObjective.Titan - quests[i].questProgress.Titan > 0) {
              quests[i].questProgress.Titan += 1;
              isTitanAlocated = true;
            }
            break;
          default:
            Debug.Log("Enemy type is not defined");
            break;
        }

        quests[i].title = GenerateQuestTitle(quests[i].questObjective, quests[i].questProgress);
        ShowQuestToUI();

        if (
        quests[i].questProgress.Zombunny == quests[i].questObjective.Zombunny
        && quests[i].questProgress.Zombear == quests[i].questObjective.Zombear
        && quests[i].questProgress.Hellephant == quests[i].questObjective.Hellephant
        && quests[i].questProgress.Titan == quests[i].questObjective.Titan
        ) {
          CompleteQuest(i);
        }
      }

    }

    string GenerateQuestTitle(QuestObjective questObjective, QuestObjective questProgress) {
      string questName = "Kalahkan ";
      if (questObjective.Zombunny != 0) {
        questName += $"{questProgress.Zombunny}/{questObjective.Zombunny} Zombunny ";
      }
      if (questObjective.Zombear != 0) {
        questName += $"{questProgress.Zombear}/{questObjective.Zombear} Zombear ";
      }
      if (questObjective.Hellephant != 0) {
        questName += $"{questProgress.Hellephant}/{questObjective.Hellephant} Hellephant ";
      }
  
      return questName;
    }
}
