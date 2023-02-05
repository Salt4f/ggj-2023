using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissiveObstacle : Obstacle
{
    public ParticleSystem[] particles;
    public AudioClip emitSFX;

    private AudioSource _audio;
    private bool canSound;

    private void Start()
    {
        canSound = false;
        _audio = GetComponent<AudioSource>();
    }

    public void EmitParticles()
    {
        if (canSound) _audio.PlayOneShot(emitSFX);
        foreach (var ps in particles)
        {
            ps.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player")) canSound = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) canSound = false;
    }
}
