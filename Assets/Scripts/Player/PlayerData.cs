using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string name;
    public string timeStr;
    public string lastScene;
    public int coins;
    public float time;
    public PetData petData;    
    public List<bool> availableWeapon;

    // Start is called before the first frame update
    public PlayerData() {
        name = "save" + DateTime.Now.ToString("ddMMyyyy");
        timeStr = DateTime.Now.ToString("dd/MM/yyyy");
        lastScene = "Level_01";
        coins = 0;
        time = 0;
        availableWeapon = new List<bool>() { true, false, false, false };
    }

    public PlayerData(string lastScene, int coins, float time, string name, string timeStr, PetData petData, List<bool> availableWeapon) {
        this.name = name;
        this.timeStr = timeStr;
        this.lastScene = lastScene;
        this.time = time;
        this.coins = coins;
        this.availableWeapon = availableWeapon;
        this.petData = petData;
    }
}
