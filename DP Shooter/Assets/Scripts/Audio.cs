using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip shoot;
    //public AudioClip teleport;
    public AudioClip destroy;
    //public AudioClip died;

    private AudioSource source;

    // Start is called before the first frame update
    private void Start()
    {
        source = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void PlayShoot()
    {
        source.clip = shoot;
        source.Play();
    }

    //public void PlayTeleport()
    //{
    //    source.clip = teleport;
    //    source.Play();
    //}

    public void PlayDestroy()
    {
        source.clip = destroy;
        source.Play();
    }

    //public void PlayDied()
    //{
    //    source.clip = died;
    //    source.Play();
    //}


}

