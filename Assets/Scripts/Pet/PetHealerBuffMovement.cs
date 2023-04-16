using System.Collections;
using UnityEngine;

public class PetHealerBuffMovement : PetMovement
{
    public float EnemyDistanceRun = 4.0f;
    int shootableLayer = 3; // TODO: use this instead of collider trigger

    void OnTriggerEnter(Collider other)
    {
        // If enemy is in range
        if (other.gameObject.layer == shootableLayer)
        {
            Debug.Log("Bukan muhrim");

            // Calculate enemy distance
            Vector3 dirToEnemy = transform.position - other.transform.position;
            Vector3 newPos = transform.position + dirToEnemy;

            // Run away from enemy
            if (GetComponent<UnityEngine.AI.NavMeshAgent>().enabled)
            {
                nav.SetDestination(newPos);
            }
        }
    }
}