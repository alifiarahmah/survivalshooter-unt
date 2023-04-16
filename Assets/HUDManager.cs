using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class HUDManager : MonoBehaviour
{

    public AudioSource bgMusic;
    // Shop
    [Header("Shop")]
    public GameObject shopUI;
    public Transform shopLocation;
    public Transform playerLocation;
    public float shopRadius = 10f;

    // Weapons
    [Header("Weapons")]
    public GameObject weaponsUI;
    public GameObject weaponsUIHighlight;
    public GameObject weaponsUISlot;

    // Alert Banner
    [Header("Alert Banner")]
    public GameObject alertBanner;
    public TMP_Text alertText;
    public float fadeTime = 0.5f;
    public float delayTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        bgMusic.volume = PlayerPrefs.GetFloat("musicVolume");
        bgMusic.Play(0);

        for(int i = 0; i<4; i++)
        {
            GameObject slot = weaponsUISlot.transform.GetChild(i).gameObject;
            slot.SetActive(GameManager.gameManager.availableWeapon[i]);
            Debug.Log(GameManager.gameManager.availableWeapon[i].ToString());
        }
        List<bool> availableWeapon = GameManager.gameManager.availableWeapon;
        Debug.Log(availableWeapon[0].ToString() + availableWeapon[1].ToString() + availableWeapon[2].ToString() + availableWeapon[3].ToString());

    }

    // Update is called once per frame
    void Update()
    {
        // Opening Shop
        float distanceToShop = Vector3.Distance(playerLocation.position, shopLocation.position);

        if (Input.GetKeyDown(KeyCode.T) && !GameManager.gameManager.isShopping)
        {
            if (!GameManager.gameManager.isShopOpen)
            {
                AlertMessage("Shop is not open yet");
            }
            else if(distanceToShop > shopRadius)
            {
                AlertMessage("Shop is too far");
            }
            else
            {
                shopUI.SetActive(true); 
                GameManager.gameManager.isShopping = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.T) && GameManager.gameManager.isShopping)
        {
            shopUI.SetActive(false);
            GameManager.gameManager.isShopping = false;
        }
    }

    // Alert Banner
    public void AlertMessage(string message)
    {
        StartCoroutine(FadeAlertMessage(message));
    }

    public IEnumerator FadeAlertMessage(string message)
    {
        alertText.text = message;
        CanvasGroup canvasGroup = alertBanner.GetComponent<CanvasGroup>();

        // Fade in
        for (float t = 0.01f; t < 1f; t += Time.deltaTime / fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, t);
            yield return null;
        }

        // Show for delayTime seconds
        yield return new WaitForSeconds(delayTime);

        // Fade out
        for (float t = 0.01f; t < 1f; t += Time.deltaTime / fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
        }
    }

    public void SetWeapon(int weaponSlot)
    {
        if (GameManager.gameManager.availableWeapon[weaponSlot-1])
        {
            for (int i = 0; i < 4; i++)
            {
                weaponsUIHighlight.transform.GetChild(i).gameObject.SetActive(false);
            }

            GameObject slot = weaponsUIHighlight.transform.GetChild(weaponSlot - 1).gameObject;
            slot.SetActive(true);
        }
    }

    public void NewWeapon(int id)
    {
        GameObject slot = weaponsUISlot.transform.GetChild(id-1).gameObject;
        slot.SetActive(true);
    }
}
