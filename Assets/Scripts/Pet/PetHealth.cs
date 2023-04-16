using UnityEngine;
using UnityEngine.UI;

public class PetHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;

    Animator anim;
    AudioSource petAudio;
    PetMovement petMovement;
    PetHeal petHeal;
    PetAttack petAttack;
    PetBuff petBuff;
    Slider petHealthSlider;

    [Header("Sound Effects")]
    public AudioClip spawnClip;
    public AudioClip deathClip;

    [Header("UI")]
    public GameObject pawImage;
    public GameObject petHealthSliderObj;

    bool isDead;
    bool isSinking;

    void Start()
    {
        // Set isPetAlive in GameManager to true
        GameManager.gameManager.isPetAlive = true;

        anim = GetComponent <Animator> ();
        petAudio = GetComponent <AudioSource> ();
        petMovement = GetComponent<PetMovement>();
        petHeal = GetComponent <PetHeal> ();
        petAttack = GetComponent<PetAttack>();
        petBuff = GetComponent<PetBuff>();
        AudioClip hurtClip = petAudio.clip;

        currentHealth = startingHealth;

        // Show GameObject UI
        petHealthSliderObj.SetActive(true);
        pawImage.SetActive(true);
        petHealthSlider = petHealthSliderObj.GetComponent<Slider>();
        petHealthSlider.value = currentHealth;

        // Play spawn audio
        petAudio.clip = spawnClip;
        petAudio.Play();
        // Reset to hurt
        petAudio.clip = hurtClip;
    }

    void Update()
    {
      //Check j ika sinking
      if (isSinking)
      {
        //memindahkan object kebawah
        transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
      }
      if (CheatMenu.killpet)
      {
        Death();
        CheatMenu.killpet = false;
      }
    }

    public void TakeDamage(int amount)
    {
        if (!CheatMenu.fullhppet)
        {
            currentHealth -= amount;

            petAudio.Play();

            if (currentHealth <= 0 && !isDead)
            {
                Death();
            }

            // Update slider value
            petHealthSlider.value = currentHealth;
        };
    }

    void Death()
    {
        isDead = true;

        // Set isPetAlive in gameManager to false
        GameManager.gameManager.isPetAlive = false;

        // Hide GameObject UI
        petHealthSliderObj.SetActive(false);
        pawImage.SetActive(false);

        anim.SetTrigger("Die");

        petAudio.clip = deathClip;
        petAudio.Play();

        petMovement.enabled = false;

        // Disable type-specific pet scripts
        if (petHeal != null)
        {
            petHeal.enabled = false;
        }
        else if (petAttack != null)
        {
            petAttack.enabled = false;
        }
        else if (petBuff != null)
        {
            petBuff.RemoveBuff();
            petBuff.enabled = false;
        }

        StartSinking();
    }

    public void StartSinking()
    {
        //disable Navmesh Component
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        //Set rigisbody ke kinematic
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2f);
    }
}
