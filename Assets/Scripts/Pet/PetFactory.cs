using System;
using UnityEngine;
using UnityEngine.UI;

public class PetFactory : MonoBehaviour
{
    [Header("Pet Prefabs")]
    public GameObject petRabbitPrefab;
    public GameObject petSparrowPrefab;
    public GameObject petCultistPrefab;
    public Transform PlayerTransform;

    [Header("UI")]
    public GameObject pawImage;
    public GameObject petHealthSlider;

    GameObject pet;

    public void FactoryMethod(PetData petData)
    {
        if (petData.name == "rabbit")
        {
            pet = Instantiate(petRabbitPrefab, PlayerTransform);
        }
        else if (petData.name == "sparrow")
        {
            pet = Instantiate(petSparrowPrefab, PlayerTransform);
        }
        else if (petData.name == "cultist")
        {
            pet = Instantiate(petCultistPrefab, PlayerTransform);
        }

        PetMovement component = pet.GetComponent<PetMovement>(); // Get the component you want to modify

        if (component != null) // Check if the component exists
        {
            Debug.Log("adaaaa");
            component.target = PlayerTransform; // Set the speed property on the component
        }

        // Set pawImage and petHealthSlider
        PetHealth petHealth = pet.GetComponent<PetHealth>();
        if (petHealth != null)
        {
            petHealth.pawImage = pawImage;
            petHealth.petHealthSliderObj = petHealthSlider;
        }

        // Integrate new pet with GameManager
        GameManager.gameManager.currentPet = petData;
    }
}
