using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossQuestManager : QuestManager
{
  public static BossQuestManager questManager;
  
  void Awake() {
    GameManager gameManager = FindObjectOfType<GameManager>();
    gameManager.isBoss = true;


    if (questManager == null)
    {
        questManager = this;
    }
    else
    {
        Destroy(questManager);
    }
  }
  
  void Start() {
    quests[0] = GenerateQuest();
    ShowQuestToUI();
  }

  Quest GenerateQuest() {
    QuestObjective questObjective = new QuestObjective(0,0,0,0,1);
    string questTitle = GenerateQuestTitle(questObjective, new QuestObjective(0,0,0,0,0));

    return new Quest(questTitle, questObjective, 1200);
  }

  public override void UpdateQuestStatus(string enemyType) {
    quests[0].questProgress.Titan += 1;

    quests[0].title = GenerateQuestTitle(quests[0].questObjective, quests[0].questProgress);
    ShowQuestToUI();

    CompleteQuest(0);
  }

  string GenerateQuestTitle(QuestObjective questObjective, QuestObjective questProgress) {
      string questName = "Kalahkan ";
      if (questObjective.Titan != 0) {
        questName += $"{questProgress.Titan}/{questObjective.Titan} Titan ";
      }

      return questName;
    }
}
