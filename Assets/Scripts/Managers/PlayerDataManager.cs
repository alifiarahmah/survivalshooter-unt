using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    private PlayerData data0;
    private PlayerData data1;
    private PlayerData data2;
    private PlayerData data3;
    public List<bool> availableSlot = new List<bool>() { false, false, false };

    void Awake()
    {
        data0 = FileHandler.ReadFromJSONPrefs<PlayerData>("PlayerData0.json");
        if (data0 == null)
        {
            data0 = new PlayerData();
        }
        data1 = FileHandler.ReadFromJSONPrefs<PlayerData>("PlayerData1.json");
        if (data1 == null) {
            data1 = new PlayerData();
        }
        else
        {
            availableSlot[0] = true;
        }

        data2 = FileHandler.ReadFromJSONPrefs<PlayerData>("PlayerData2.json");
        if (data2 == null) {
            data2 = new PlayerData();
        }
        else
        {
            availableSlot[1] = true;
        }

        data3 = FileHandler.ReadFromJSONPrefs<PlayerData>("PlayerData3.json");
        if (data3 == null) {
            data3 = new PlayerData();
        }
        else
        {
            availableSlot[2] = true;
        }
    }

    public PlayerData GetNumData(int data) {
        if (data == 1) {
            return data1;
        } else if (data == 2) {
            return data2;
        } else if (data == 3)
        {
            return data3;
        }
        else
        {
            return data0;
        }
    }

    public void SetCoins(int num_data, int coins) {
        PlayerData data = GetNumData(num_data);
        if (data != null) {
            data.coins = coins;
            SavePlayerData(num_data);
        } else {
            Debug.LogError("PlayerDataManager: data object is null!");
        }
    }

    public void SetLastScene(int num_data, string lastScene) {
        PlayerData data = GetNumData(num_data);
        if (data != null) {
            data.lastScene = lastScene;
            SavePlayerData(num_data);
        } else {
            Debug.LogError("PlayerDataManager: data object is null!");
        }
    }

    public void SetTime(int num_data, float time) {
        PlayerData data = GetNumData(num_data);
        if (data != null) {
            data.time = time;
            SavePlayerData(num_data);
        } else {
            Debug.LogError("PlayerDataManager: data object is null!");
        }
    }

    public void SetName(int num_data, string name)
    {
        PlayerData data = GetNumData(num_data);
        if (data != null)
        {
            data.name = name;
            SavePlayerData(num_data);
        }
        else
        {
            Debug.LogError("PlayerDataManager: data object is null!");
        }
    }

    public void SetPetData(int num_data, PetData petData)
    {
        PlayerData data = GetNumData(num_data);
        if (data != null)
        {
            data.petData = petData;
            SavePlayerData(num_data);
        }
        else
        {
            Debug.LogError("PlayerDataManager: data object is null!");
        }
    }
    public void SetAvailableWeapons(int num_data, List<bool> availableWeapon)
    {
        PlayerData data = GetNumData(num_data);
        if (data != null)
        {
            data.availableWeapon = availableWeapon;
            SavePlayerData(num_data);
        }
        else
        {
            Debug.LogError("PlayerDataManager: data object is null!");
        }
    }

    public int GetCoins(int num_data) {
        PlayerData data = GetNumData(num_data);
        return data.coins;
    }

    public string GetLastScene(int num_data) {
        PlayerData data = GetNumData(num_data);
        return data.lastScene;
    }

    public float GetTime(int num_data) {
        PlayerData data = GetNumData(num_data);
        return data.time;
    }

    public PetData GetPetData(int num_data)
    {
        PlayerData data = GetNumData(num_data);
        return data.petData;
    }
    public List<bool> GetAvailableWeapon(int num_data)
    {
        PlayerData data = GetNumData(num_data);
        return data.availableWeapon;
    }

    public PlayerData GetData(int num_data) {
        PlayerData data = GetNumData(num_data);
        return data;
    }

    public void Clear(int num_data) {
        PlayerData data = GetNumData(num_data);
        data = new PlayerData();
        SavePlayerData(num_data);
    }

    public void ClearTemp()
    {
        PlayerData data0 = new PlayerData();
        data0.timeStr = DateTime.Now.ToString("dd/MM/yyyy");

        FileHandler.SaveToJsonPrefs<PlayerData>(data0, "PlayerData" + 0 + ".json");
    }

    public void ClearAll() {
        data1 = new PlayerData();
        data2 = new PlayerData();
        data3 = new PlayerData();
        data0 = new PlayerData();
        SavePlayerDataAll();
    }

    // Update is called once per frame
    public void OnDestroy() {
        //SavePlayerDataAll();
    }

    public void SavePlayerData(int num_data) {
        PlayerData data = GetNumData(num_data);
        data.timeStr = DateTime.Now.ToString("dd/MM/yyyy");

        FileHandler.SaveToJsonPrefs<PlayerData>(data, "PlayerData" + num_data + ".json");
    }

    public void SavePlayerDataAll() {
        data1.timeStr = DateTime.Now.ToString("dd/MM/yyyy");
        data2.timeStr = DateTime.Now.ToString("dd/MM/yyyy");
        data3.timeStr = DateTime.Now.ToString("dd/MM/yyyy");
        data0.timeStr = DateTime.Now.ToString("dd/MM/yyyy");
        FileHandler.SaveToJsonPrefs<PlayerData>(data1, "PlayerData1.json");
        FileHandler.SaveToJsonPrefs<PlayerData>(data2, "PlayerData2.json");
        FileHandler.SaveToJsonPrefs<PlayerData>(data3, "PlayerData3.json");
        FileHandler.SaveToJsonPrefs<PlayerData>(data0, "PlayerData0.json");
    }
}
