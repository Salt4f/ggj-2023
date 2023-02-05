using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissiveObstacle : Obstacle
{
    public ParticleSystem[] particles;
    private AudioSource _audio;

    private void Start()
    {
        //_audio = GetComponent<AudioSource>();
    }

    public void EmitParticles()
    {
        //_audio.Play();
        foreach (var ps in particles)
        {
            ps.Play();
        }
    }
}
