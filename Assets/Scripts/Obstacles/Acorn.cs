using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorn : Obstacle
{
    private Animator anim;

    public Collider triggerCollider;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("trigger");
            triggerCollider.enabled = false;
        }
    }
}
