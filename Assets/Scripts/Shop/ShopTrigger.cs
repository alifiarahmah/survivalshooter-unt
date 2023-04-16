using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopTrigger : MonoBehaviour
{
    // Timer
    public float shopTimer = 30f;
    public GameObject shopTimerUI;
    public GameObject shopUI;

    private TMP_Text timerText;

    void Start()
    {
        timerText = shopTimerUI.transform.GetChild(0).GetComponent<TMP_Text>();
        timerText.text = shopTimer.ToString("F1");
    }

    // Update is called once per frame
    void Update()
    {
      shopTimer -= Time.deltaTime;
      timerText.text = shopTimer.ToString("F1");

      if (shopTimer <= 0f) {
        GameManager.gameManager.isShopOpen = false;
        shopTimerUI.SetActive(false);
        shopUI.SetActive(false);
        GameManager.gameManager.isShopping = false;
        GameManager.gameManager.TriggerSpawningEnemy();
      }
    }
}
