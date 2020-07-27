﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************
File:           BulletBehavior.cs
Authors:        Emmy Berg
Last Updated:   7/27/2020
Last Version:   2019.3.11

Description:
This script contains all the player/interactable controls, 
like movement and shooting

***************************************************/

public class PlayerController : MonoBehaviour
{
    //Reference to the player 
    internal static GameObject Player;

    //Reference to this script
    public static PlayerController playerScript;

    //References the audio source
    AudioSource aud;                               

    [Header("Player Settings")]

    //Ripple fx that reacts to teleportation
    public GameObject ripplefx;

    //Calls the shoot sfx clip
    public AudioClip shoot;

    //Default speed
    public float playerSpeed = 3.5f;
              


    [Header("Bullet Settings")]

    //Reference to the bullet
    public GameObject Bullet;

    //Location where bullet will be instantiated
    public Transform bulletOrigin;

    //Bullet speed
    public float shootSpeed = 10f;

    //Time til player can shoot
    public float cooldown = 0.5f;
    
    //Reference to mouse
    private Vector3 target; 
    
    //Check to shoot
    bool canShoot = true;

    

    void Start()
    {
        Cursor.visible = false;
        Player = this.gameObject;
        playerScript = this;
        aud = GetComponent<AudioSource>();
        aud.clip = shoot;
    }

    void Update()
    {
        //Gets the mouse's position
        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        target.z = 0f;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        Movement();
    }

    //Instantiates (clones) a new bullet and fires it in the direction of the target point (where the mouse clicked) in a straight line
    void Shoot()
    {
        if(canShoot)
        {
            Vector3 bulletDirection;

            bulletDirection = target - bulletOrigin.position;

            GameObject firedBullet = Instantiate(Bullet, bulletOrigin.position, bulletOrigin.rotation);
            firedBullet.GetComponent<Rigidbody2D>().velocity = bulletDirection.normalized * shootSpeed;

            aud.Play();
            
            canShoot = false;
            StartCoroutine(ShootCoolDown());

            //Debug.Log($"{target}, {bulletDirection}, {bulletDirection.normalized * shootSpeed}");
        }

    }
    private IEnumerator ShootCoolDown()
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }


        void Movement()
    {
        //Changed the transform.position to a velocity so we can acknowledge colliders , transform disreguards any colliders 
        Vector3 pos = new Vector3();

        if (Input.GetKey(KeyCode.W))
        {
            pos.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos.y -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= 1;
        }
        //Uses normalized vector to maintain constant speed in all directions
        GetComponent<Rigidbody2D>().velocity = pos.normalized * playerSpeed;
    }
}
