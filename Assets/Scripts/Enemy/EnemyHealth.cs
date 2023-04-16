using UnityEngine;
 
public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public AudioClip deathClip;
    public enum EnemyType{Zombunny, Zombear, Hellephant, Titan};
    public EnemyType enemyType;
 
    protected Animator anim;
    protected AudioSource enemyAudio;
    protected ParticleSystem hitParticles;
    protected CapsuleCollider capsuleCollider;
    protected bool isDead;
    protected bool isSinking;
 
 
    protected void Awake ()
    {
        //Mendapatkan reference komponen
        anim = GetComponent <Animator>();
        enemyAudio = GetComponent<AudioSource>();
        enemyAudio.volume = PlayerPrefs.GetFloat("sfxVolume");
        hitParticles = GetComponentInChildren<ParticleSystem> ();
        capsuleCollider = GetComponent<CapsuleCollider>();
 
        //Set current health
        currentHealth = startingHealth;
    }
 
 
    protected void Update()
    {
        //Check jika sinking
        if (isSinking)
        {
            //memindahkan object kebawah
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }
 
 
    public virtual void TakeDamage (int amount, Vector3 hitPoint)
    {
        //Check jika dead
        if(isDead)
            return;
 
        //play audio
        enemyAudio.Play ();
 
        //kurangi health
        currentHealth -= amount;
           
        //Ganti posisi particle
        hitParticles.transform.position = hitPoint;
 
        //Play particle system
        hitParticles.Play();
 
        //Dead jika health <= 0
        if(currentHealth <= 0)
        {
            Death();
        }
    }
 
    protected void Death ()
    {
        //set isdead
        isDead = true;
 
        //SetCapcollider ke trigger
        capsuleCollider.isTrigger = true;
 
        //trigger play animation Dead
        anim.SetTrigger("Dead");
 
        //Play Sound Dead
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        UpdateActiveQuestStatus();
    }
 
    public void StartSinking ()
    {
        //disable Navmesh Component
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        //Set rigisbody ke kinematic
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2f);
    }

    void UpdateActiveQuestStatus() {
      switch (enemyType) {
        case EnemyType.Zombunny:
          QuestManager.questManager.UpdateQuestStatus("Zombunny");
          break;
        case EnemyType.Zombear:
          QuestManager.questManager.UpdateQuestStatus("Zombear");
          break;
        case EnemyType.Hellephant:
          QuestManager.questManager.UpdateQuestStatus("Hellephant");
          break;
        case EnemyType.Titan:
          BossQuestManager.questManager.UpdateQuestStatus("Titan");
          break;
        default:
          Debug.Log("enemy type is not defined");
          break;
      }
    }
}