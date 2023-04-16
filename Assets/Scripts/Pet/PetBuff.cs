using System.Collections;
using UnityEngine;

public class PetBuff : MonoBehaviour
{
    public int buffAmount = 10;

    PlayerHealth playerHealth;
    PlayerShooting playerShooting;
    PetHealth petHealth;

    // Use this for initialization
    void Start()
    {
        // Mencari script PlayerShooting
        GameObject gunBarrelEnd = GameObject.Find("GunBarrelEnd");
        playerShooting = gunBarrelEnd.GetComponent<PlayerShooting>();

        // Mendapat komponen PetHealth
        petHealth = GetComponent<PetHealth>();

        // Mendapat komponen PlayerHealth
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

        Buff();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Buff()
    {
        playerShooting.damagePerShot += buffAmount;
        Debug.Log("Buff");
    }

    public void RemoveBuff()
    {
        playerShooting.damagePerShot -= buffAmount;
        Debug.Log("Debuff");
    }
}