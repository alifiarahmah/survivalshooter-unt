using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// When in pet detect enemy (shootable), pet will chase
public class PetAttackerMovement : PetMovement
{
    public float detectRadius = 4f; // radius to detect enemy that will be chased
    public float range = 4f;
    public float maxDistanceToEnemy = 2f;

    RaycastHit raycastHit;
    int shootableMask;

    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
    }

    void Update()
    {
        if (Physics.SphereCast(transform.position, detectRadius, transform.forward * range, out raycastHit, range, shootableMask))
        {
            // Follow enemy
            nav.SetDestination(raycastHit.transform.position);

            // Get a random point within a sphere with radius equal to distance
            Vector3 destination = Random.insideUnitSphere * maxDistanceToEnemy;
            destination += transform.position; // Add the agent's current position

            // Find the closest point on the NavMesh to the destination
            if (NavMesh.SamplePosition(destination, out NavMeshHit navMeshHit, maxDistanceToEnemy, NavMesh.AllAreas))
            {
                // Set the agent's destination to the closest point on the NavMesh
                nav.SetDestination(navMeshHit.position);
            }
        }
    }
}
