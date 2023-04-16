using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponManager : MonoBehaviour {
    public GameObject gunList;

    void Awake() {
        if (gunList == null){
            gunList = GameObject.Find("GunList");
        }
        Debug.Log("Script successfully loaded");
        gunList.transform.GetChild(0).gameObject.SetActive(true);
        gunList.transform.GetChild(1).gameObject.SetActive(false);
        gunList.transform.GetChild(2).gameObject.SetActive(false);
        gunList.transform.GetChild(3).gameObject.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown("1"))
        {
            gunList.transform.GetChild(0).gameObject.SetActive(true);
            gunList.transform.GetChild(1).gameObject.SetActive(false);
            gunList.transform.GetChild(2).gameObject.SetActive(false);
            gunList.transform.GetChild(3).gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown("2"))
        {
            if (GameManager.gameManager.availableWeapon[1])
            {
                gunList.transform.GetChild(0).gameObject.SetActive(false);
                gunList.transform.GetChild(1).gameObject.SetActive(true);
                gunList.transform.GetChild(2).gameObject.SetActive(false);
                gunList.transform.GetChild(3).gameObject.SetActive(false);
            }
            else
            {
                GameManager.gameManager.hudManager.AlertMessage("Shotgun is not available yet");
            }
        }
        else if (Input.GetKeyDown("3"))
        {
            if (GameManager.gameManager.availableWeapon[2])
            {
                gunList.transform.GetChild(0).gameObject.SetActive(false);
                gunList.transform.GetChild(1).gameObject.SetActive(false);
                gunList.transform.GetChild(2).gameObject.SetActive(true);
                gunList.transform.GetChild(3).gameObject.SetActive(false);
            }
            else
            {
                GameManager.gameManager.hudManager.AlertMessage("Sword is not available yet");
            }
        }
        else if (Input.GetKeyDown("4"))
        {
            if (GameManager.gameManager.availableWeapon[3])
            {
                gunList.transform.GetChild(0).gameObject.SetActive(false);
                gunList.transform.GetChild(1).gameObject.SetActive(false);
                gunList.transform.GetChild(2).gameObject.SetActive(false);
                gunList.transform.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                GameManager.gameManager.hudManager.AlertMessage("Bow is not available yet");
            }
        }
    }
}