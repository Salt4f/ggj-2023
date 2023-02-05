using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FallingObstacle : Obstacle
{
    private Animator anim;
    private AudioSource _audio;
    private CinemachineImpulseSource source;
    private bool isFalling;

    public Collider triggerCollider;
    public string fallStateName = "Fall";
    public AudioClip groundSFX;

    private void Start()
    {
        isFalling = false;
        anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        source = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        if (!isFalling) return;
        var state = anim.GetCurrentAnimatorStateInfo(0);
        if (!state.IsName(fallStateName)) return;
        if (state.normalizedTime >= 1f)
        {
            isFalling = false;
            source.GenerateImpulse();
            _audio.PlayOneShot(groundSFX);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("trigger");
            triggerCollider.enabled = false;
            isFalling = true;
        }
    }
}
