using UnityEngine;
using System.Collections;
 
public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
 
    Animator anim;
    GameObject player, pet;
    PlayerHealth playerHealth;
    PetHealth petHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    bool petInRange;
    float timer;
 
 
    void Awake ()
    {
        //Mencari game object dengan tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");

        // Mencari game object dengan tag "Pet"
        pet = GameObject.FindGameObjectWithTag("Pet");

        //mendapatkan komponen player health
        playerHealth = player.GetComponent <PlayerHealth> ();

        // Jika pet ada
        if (pet != null)
        {
            petHealth = pet.GetComponent<PetHealth>();
        }
 
        //mendapatkan komponen Animator
        anim = GetComponent <Animator> ();
        enemyHealth = GetComponent<EnemyHealth>();
    }
 
 
    //Callback jika ada suatu object masuk kedalam trigger
    void OnTriggerEnter (Collider other)
    {
        //Set player in range
        if(other.gameObject == player)
        {
            playerInRange = true;
        } 
        else if (other.gameObject == pet)
        {
            petInRange = true;
        }
    }
 
    //Callback jika ada object yang keluar dari trigger
    void OnTriggerExit (Collider other)
    {
        //Set player not in range
        if(other.gameObject == player)
        {
            playerInRange = false;
        } 
        else if (other.gameObject == pet)
        {
            petInRange = false;
        }
    }
 
 
    void Update ()
    {
        timer += Time.deltaTime;
 
        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            AttackPlayer ();
        }
        if (timer >= timeBetweenAttacks && petInRange && enemyHealth.currentHealth > 0)
        {
            AttackPet();
        }

        //mentrigger animasi PlayerDead jika darah player kurang dari sama dengan 0
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }
 
 
    void AttackPlayer ()
    {
        //Reset timer
        timer = 0f;
 
        // Player Taking Damage
        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }

    void AttackPet()
    {
        // Reset timer
        timer = 0f;

        // Pet Taking Damage
        if (petHealth.currentHealth > 0)
        {
            petHealth.TakeDamage(attackDamage);
        }
    }
}