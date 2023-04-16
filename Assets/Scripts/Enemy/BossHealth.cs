using UnityEngine;
using UnityEngine.UI;
 
public class BossHealth : EnemyHealth
{
    public Slider healthSlider;
 
    public override void TakeDamage (int amount, Vector3 hitPoint)
    {
        //Check jika dead
        if(isDead)
            return;
 
        //play audio
        enemyAudio.Play ();
 
        //kurangi health
        currentHealth -= amount;

        healthSlider.value = currentHealth;
           
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

    void Death ()
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

  void UpdateActiveQuestStatus() {
    BossQuestManager.questManager.UpdateQuestStatus("Titan");
  }
}