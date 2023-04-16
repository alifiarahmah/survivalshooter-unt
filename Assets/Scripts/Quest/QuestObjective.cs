using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestObjective
{
    private int zombunny;
    public int Zombunny {
      get { return zombunny; }
      set { zombunny = value; }
    }
    private int zombear;
    public int Zombear {
      get { return zombear; }
      set { zombear = value; }
    }
    private int hellephant;
    public int Hellephant {
      get { return hellephant; }
      set { hellephant = value; }
    }

    private int zomhellephant;
    public int Zomhellephant {
      get { return zomhellephant; }
      set { zomhellephant = value; }
    }

    private int titan;
    public int Titan {
      get { return titan; }
      set { titan = value; }
    }

    public QuestObjective(int zombunny, int zombear, int hellephant, int zomhellephant = 0, int titan = 0) {
      this.zombunny = zombunny;
      this.zombear = zombear;
      this.hellephant = hellephant;
      this.zomhellephant = zomhellephant;
      this.titan = titan;
    }
}
