using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetMovement: MonoBehaviour
{
    public Transform target;
    protected NavMeshAgent nav;
    Animator anim;

    public float rotationSpeed = 10f;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Set destination to player's position
        nav.SetDestination(target.position);

        // Walk animation
        if (nav.velocity.sqrMagnitude > 0)
        {
            anim.SetBool("IsWalking", true);
        } else
        {
            anim.SetBool("IsWalking", false);
        }
    }
}
