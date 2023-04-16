using System.Collections;
using UnityEngine;

public class PetHeal : MonoBehaviour
{
    public int healAmount = 10;
    public int timeBetweenHeal = 10;      // heal every 10 seconds
    public AudioClip healClip;

    GameObject player;
    AudioSource petAudio;
    PlayerHealth playerHealth;
    PetHealth petHealth;
    float timer;

    // Use this for initialization
    void Start()
    {
        //Mencari game object dengan tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

        //Mendapat komponen pet health
        petHealth = GetComponent<PetHealth>();

        // Mendapat heal clip
        healClip = GetComponent<AudioClip>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        // Heal every timeBetweenHeal if 
        if (timer >= timeBetweenHeal && petHealth.currentHealth > 0)
        {
            HealPlayer();
        }
    }

    void HealPlayer()
    {
        // Reset timer
        timer = 0f;

        // Heal player
        if (playerHealth.currentHealth < playerHealth.startingHealth)
        {
            playerHealth.AddHealth(healAmount);
        }

        // Play healing clip
        Debug.Log("Heal player");
        // petAudio.clip = healClip;
        // petAudio.Play();
    }
}