using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenAnimation : MonoBehaviour
{
    public bool isWalking;
    public GameObject player;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = player.GetComponent<Animator>();
        isWalking = anim.GetBool("IsWalking");
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("IsWalking", isWalking);
    }
}
