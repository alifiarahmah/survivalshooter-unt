using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // General
    [Header("General")]
    public int coins = 0;
    public int coinsLatest = 0;

    // Shop Requirements
    [Header("Shop Requirements")]
    public PetData currentPet;
    public bool isPetAlive = false;
    public bool isShopOpen = true; // shop only open after finishing quest
    public bool isShopping = false; // sedang berbelanja
    public bool isEnemyActive = true;
    public bool isBoss = false;
    public bool isMenu = false;
    public PetFactory petFactory;
    public PlayerDataManager pdm;

    // Weapon
    [Header("Weapons")]
    public int currentWeapon;
    public List<bool> availableWeapon = new List<bool>() { true, false, false, false };

    // HUD
    [Header("HUD")]
    public HUDManager hudManager;

    // Enemy
    [Header("Enemy")]
    public GameObject enemySpawner;

    // Score
    [Header("Score")]
    public GameObject score;

    public static GameManager gameManager;

    private void Awake()
    {
        if (!isBoss) {
            enemySpawner.SetActive(isEnemyActive);
            score.SetActive(isEnemyActive);
        }
        if (gameManager == null)
        {
            int slot = PlayerPrefs.GetInt("activeSlot");
            Debug.Log(slot);

            int coins = pdm.GetCoins(slot + 1);
            PetData petData = pdm.GetPetData(slot + 1);
            List<bool> availableWeapon = pdm.GetAvailableWeapon(slot + 1);

            this.coins = coins;
            this.currentPet = petData;
            this.availableWeapon = availableWeapon;

            gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveTime() {
        float timeElapsed = ScoreManager.time;
        if (PlayerPrefs.HasKey("timeElapsed")) {
            PlayerPrefs.SetFloat("timeElapsed", PlayerPrefs.GetFloat("timeElapsed") + timeElapsed);
        } else {
            PlayerPrefs.SetFloat("timeElapsed", timeElapsed);
        }
        Debug.Log("Time Elapsed Quest: " + PlayerPrefs.GetFloat("timeElapsed"));
    }

    public void LoadGameOver() {
        SceneManager.LoadScene("GameOver");
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        coinsLatest = coins;
        // if true set infinity if not set to value before
        if (CheatMenu.motherlode) {
            coins = 999999;
        } else {
            coins = coinsLatest;
        }
    }

    public void NewPet(PetData petData)
    {
        petFactory.FactoryMethod(petData);
    }

    public void NewWeapon(int id)
    {
        hudManager.NewWeapon(id);
        availableWeapon[id - 1] = true;
    }

    public void SetWeapon(int i)
    {
        currentWeapon = i;
        hudManager.SetWeapon(i);
    }

    public void TriggerSpawningEnemy() {
      isEnemyActive = !isEnemyActive;
      enemySpawner.SetActive(isEnemyActive);
      score.SetActive(isEnemyActive);
    }
}
