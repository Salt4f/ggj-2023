using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    private AudioSource _audio;

    public ParticleSystem[] featherParticles;
    public AudioClip jumpSFX;
    public AudioClip deathSFX;

    private uint totalSteps;

    private void Awake()
    {
        var gm = GameManager.Instance;
        gm.player = this;
        gm.Live();
        anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        totalSteps = 0;
    }

    public void Rotate()
    {
        anim.SetTrigger("trigger");
        _audio.PlayOneShot(jumpSFX);
        GameManager.Instance.ui.SetSteps(++totalSteps);
    }

    private void Death()
    {
        anim.SetTrigger("death");
        foreach (var ps in featherParticles)
        {
            ps.Play();
        }
        _audio.PlayOneShot(deathSFX);
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
