using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;

    public ParticleSystem[] featherParticles;

    private uint totalSteps;

    private void Awake()
    {
        var gm = GameManager.Instance;
        gm.player = this;
        gm.Live();
        anim = GetComponent<Animator>();
        totalSteps = 0;
    }

    public void Rotate()
    {
        anim.SetTrigger("trigger");
        GameManager.Instance.ui.SetSteps(++totalSteps);
    }

    private void Death()
    {
        anim.SetTrigger("death");
        foreach (var ps in featherParticles)
        {
            ps.Play();
        }
        GameManager.Instance.Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Death();
        }
    }
}
