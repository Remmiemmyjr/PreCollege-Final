using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip teleport;

    public AudioClip destroy;

    public AudioClip movebox;

    public AudioClip activated;

    private AudioSource source;

    // Start is called before the first frame update
    private void Start()
    {
        source = transform.GetComponent<AudioSource>();
    }

    public void PlayTeleport()
    {
        source.clip = teleport;
        source.Play();
    }

    public void PlayDestroy()
    {
        source.clip = destroy;
        source.Play();
    }

    public void PlayMoveBox()
    {
        source.pitch = Random.Range(0.5f, 0.8f);
        source.clip = movebox;
        source.Play();
    }

    public void PlayActivated()
    {
        source.clip = activated;
        source.Play();
    }

}

