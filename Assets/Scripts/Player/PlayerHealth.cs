using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
 
 
public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;
 
    void Awake ()
    {
        //Mendapatkan refernce komponen
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerAudio.volume = PlayerPrefs.GetFloat("sfxVolume");
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }
 
 
    void Update ()
    {
        //Jika terkena damaage
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
 
        //Set damage to false
        damaged = false;
    }
 
    //fungsi untuk mendapatkan damage
    public void TakeDamage (int amount)
    {
        damaged = true;
 
        //mengurangi health
        if (!CheatMenu.nodamage) {
            currentHealth -= amount;
        }
        //Merubah tampilan dari health slider
        healthSlider.value = currentHealth;
 
        //Memainkan suara ketika terkena damage
        playerAudio.Play ();
 
        //Memanggil method Death() jika darahnya kurang dari sama dengan 10 dan belu mati
        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }

    public void AddHealth (int amount)
    {
        // Menambah health
        if (currentHealth < startingHealth)
        {
            currentHealth += amount;
        }

        // Update health slider
        healthSlider.value = currentHealth;
    }
 
 
    void Death ()
    {
        isDead = true;
 
        playerShooting.DisableEffects ();
 
        //mentrigger animasi Die
        anim.SetTrigger ("Die");
        PlayerPrefs.SetInt("isWin", 0);
        //Memainkan suara ketika mati
        playerAudio.clip = deathClip;
        playerAudio.Play ();
 
        //mematikan script player movement
        playerMovement.enabled = false;
 
        playerShooting.enabled = false;
        GameManager.gameManager.LoadGameOver();
    }
}