using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;

    private uint totalSteps;

    private void Awake()
    {
        GameManager.Instance.player = this;
        //anim = GetComponent<Animator>();
        totalSteps = 0;
    }

    public void Rotate()
    {
        //anim.SetTrigger("trigger");
        GameManager.Instance.ui.SetSteps(++totalSteps);
    }

    private void Death()
    {
        // START DEATH ANIMATION
        Debug.Log("Death");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Death();
        }
    }
}
