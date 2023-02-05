using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissiveObstacle : Obstacle
{
    public ParticleSystem[] particles;
    public AudioClip emitSFX;

    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void EmitParticles()
    {
        _audio.PlayOneShot(emitSFX);
        foreach (var ps in particles)
        {
            ps.Play();
        }
    }
}
