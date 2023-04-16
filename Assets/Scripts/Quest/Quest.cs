using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Quest
{
    public string title;
    public QuestObjective questObjective;
    public QuestObjective questProgress;
    public int questReward;
    public bool isCompleted;

    public Quest(string title, QuestObjective questObjective, int questReward) {
      this.title = title;
      this.questObjective = questObjective;
      this.questProgress = new QuestObjective(0,0,0,0,0);
      this.questReward = questReward;
      this.isCompleted = false;
    }
}
