using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    // Pet Panel
    [Header("Panel foreach Pet")]
    List<PetData> petData = new List<PetData>();
    List<WeaponData> weaponData = new List<WeaponData>();

    public GameObject petPanelPrefab;
    public GameObject weaponPanelPrefab;
    public GameObject parent;

    public HUDManager hudManager;

    // Coins
    public TMP_Text coins;

    void Start()
    {
        GenerateShopPanel();
    }

    private void Awake()
    {
        petData.Add(new PetData("rabbit", "heal", 3, 50, "rabbit", 1));
        petData.Add(new PetData("sparrow", "attack", 3, 40, "sparrow", 2));
        petData.Add(new PetData("cultist", "buff", 3, 40, "cultist", 3));

        weaponData.Add(new WeaponData("shotgun", 50, "shotgun",2));
        weaponData.Add(new WeaponData("sword", 80, "sword",3));
        weaponData.Add(new WeaponData("bow", 100, "bow",4));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateShopPanel()
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }

        coins.text = GameManager.gameManager.coins.ToString();

        for (int i = 0; i < petData.Count; i++)
        {
            GameObject petPanel = Instantiate(petPanelPrefab, new Vector3(0, 0, 0), petPanelPrefab.transform.rotation, parent.transform);
            petPanel.GetComponent<RectTransform>().localPosition = new Vector3(-140 + 140 * i, 10, 0);
            PetPanel petPanelComponent = petPanel.GetComponent<PetPanel>();

            // Change to pet data
            PetData pet = petData[i];
            petPanelComponent.name.text = pet.name;
            petPanelComponent.buff_type.text = pet.buffType;
            petPanelComponent.price.text = pet.price.ToString();
            petPanelComponent.image.sprite = Resources.Load<Sprite>("UI/Shop/" + pet.filename);
            petPanelComponent.button.onClick.AddListener(() => BuyPet(pet));
        }


        for (int i = 0; i < weaponData.Count; i++)
        {
            GameObject weaponPanel = Instantiate(weaponPanelPrefab, new Vector3(0, 0, 0), weaponPanelPrefab.transform.rotation, parent.transform);
            weaponPanel.GetComponent<RectTransform>().localPosition = new Vector3(-140 + 140 * (i + 3), 10, 0);
            WeaponPanel weaponPanelComponent = weaponPanel.GetComponent<WeaponPanel>();

            // Change to pet data
            WeaponData weapon = weaponData[i];
            weaponPanelComponent.name.text = weapon.name;
            weaponPanelComponent.price.text = weapon.price.ToString();
            weaponPanelComponent.image.sprite = Resources.Load<Sprite>("UI/Shop/" + weapon.filename);
            weaponPanelComponent.button.onClick.AddListener(() => BuyWeapon(weapon));
        }
    }

    public void BuyWeapon(WeaponData weaponData)
    {
        int currentCoins = GameManager.gameManager.coins;


        if (currentCoins < weaponData.price)
        {
            hudManager.AlertMessage("You Don't have enough coins");
        }

        else
        {
            // Update coins
            GameManager.gameManager.coins -= weaponData.price;
            coins.text = GameManager.gameManager.coins.ToString();

            // Update Weapon
            GameManager.gameManager.NewWeapon(weaponData.id);
            this.weaponData.Remove(weaponData);
            GenerateShopPanel();
        }



    }

    public void BuyPet(PetData petData)
    {
        int currentCoins = GameManager.gameManager.coins;
        bool isPetAlive = GameManager.gameManager.isPetAlive;

        if (isPetAlive)
        {
            hudManager.AlertMessage("Your pet is still alive");
        }

        else if (currentCoins < petData.price)
        {
            hudManager.AlertMessage("You Don't have enough coins");
        }

        else
        {
            // Update coins
            GameManager.gameManager.coins -= petData.price;
            coins.text = GameManager.gameManager.coins.ToString();

            // Update pet
            GameManager.gameManager.NewPet(petData);
            GameManager.gameManager.currentPet = petData;
            GameManager.gameManager.isPetAlive = true;
            this.petData.Remove(petData);
            GenerateShopPanel();
        }

    }


}
