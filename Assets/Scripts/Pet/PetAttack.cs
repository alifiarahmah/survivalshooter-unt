using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetAttack : MonoBehaviour
{
    public int damage = 20;
    public int timeBetweenAttack = 1;
    public float range = 3f;
    public float detectRadius = 3f;

    float timer;
    PetHealth petHealth;
    Animator anim;
    RaycastHit raycastHit;
    int shootableMask;

    void Start()
    {
        anim = GetComponent<Animator>();
        petHealth = GetComponent<PetHealth>();
        shootableMask = LayerMask.GetMask("Shootable");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        // Spherecast to detect enemy
        if (Physics.SphereCast(transform.position, detectRadius, transform.forward * range, out raycastHit, range, shootableMask))
        {
            // Face the hit enemy
            Vector3 targetDir = raycastHit.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(targetDir);

            // Attack
            EnemyHealth enemyHealth = raycastHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                AttackEnemy(enemyHealth);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        AttackEnemy(enemyHealth);
    }

    public void AttackEnemy(EnemyHealth enemyHealth)
    {
        if (timer >= timeBetweenAttack && enemyHealth != null && petHealth.currentHealth > 0)
        {
            // Reset timer
            timer = 0f;

            anim.SetTrigger("Attack");
            // anim.SetBool("IsAttacking", true);

            //Lakukan Take Damage
            enemyHealth.TakeDamage(damage, new Vector3(0, 0, 0));

            // anim.SetBool("IsAttacking", false);
        }
    }
}
