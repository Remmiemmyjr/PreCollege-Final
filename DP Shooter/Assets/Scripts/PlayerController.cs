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

    //Reference to animator
    Animator ani;

    //References the audio source
    AudioSource aud;                               

    [Header("Player Settings")]

    //Ripple fx that reacts to teleportation
    public GameObject ripplefx;

    //Calls the shoot sfx clip
    public AudioClip shoot;

    //Default speed
    public float playerSpeed = 3.5f;

    //Players scale (helps with flipping)
    public float playerScale = 0.5f;
              


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
    public bool canShoot = true;


    void Start()
    {
        Cursor.visible = false;

        Player = this.gameObject;
        playerScript = this;

        aud = GetComponent<AudioSource>();
        
        aud.clip = shoot;
    
        ani = GetComponent<Animator>();
        
    }

    void Update()
    {
        //Gets the mouse's position
        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        target.z = 0f;

        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            Shoot();
            
            FindObjectOfType<Cinemachine.CinemachineImpulseSource>().GenerateImpulse();
            //Debug.Log(FindObjectOfType<Cinemachine.CinemachineImpulseSource>().name);
            //FindObjectOfType<Cinemachine.CinemachineImpulseSource>();
            //GameObject.Find("CM vcam1").GetComponent<Cinemachine.CinemachineImpulseSource>().GenerateImpulse();
        }

        FacingCursor();
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

            aud.pitch = Random.Range(0.8f, 1.2f);
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

    void FacingCursor()
    {
        Vector3 facingDirection;

        facingDirection = target - bulletOrigin.position;

        if (facingDirection.x > 0 && Mathf.Abs(facingDirection.x) > Mathf.Abs(facingDirection.y))
        {
            SetFaceDir(FacingDir.right, false);
        }
        if (facingDirection.x < 0 && Mathf.Abs(facingDirection.x) > Mathf.Abs(facingDirection.y))
        {
            SetFaceDir(FacingDir.left, false);
        }
        if (facingDirection.y > 0 && Mathf.Abs(facingDirection.x) < Mathf.Abs(facingDirection.y))
        {
            SetFaceDir(FacingDir.up, false);
        }
        if (facingDirection.y < 0 && Mathf.Abs(facingDirection.x) < Mathf.Abs(facingDirection.y))
        {
            SetFaceDir(FacingDir.down, false);
        }
    }

    void Movement()
    {
        bool didGetInput = false;
        //Changed the transform.position to a velocity so we can acknowledge colliders , transform disreguards any colliders 
        Vector3 pos = new Vector3();

        if (Input.GetKey(KeyCode.W)) //UP
        {
            pos.y += 1;
            didGetInput = true;
            SetFaceDir(FacingDir.up, true);
        }
        if (Input.GetKey(KeyCode.S)) //DOWN
        {
            pos.y -= 1;
            didGetInput = true;
            SetFaceDir(FacingDir.down, true);
        }
        if (Input.GetKey(KeyCode.D)) //RIGHT
        {
            pos.x += 1;
            
            didGetInput = true;
            SetFaceDir(FacingDir.right, true);
        }
        if (Input.GetKey(KeyCode.A)) //LEFT
        {
            pos.x -= 1;
           
            didGetInput = true;
            SetFaceDir(FacingDir.left, true);
        }
        
        //Uses normalized vector to maintain constant speed in all directions
        GetComponent<Rigidbody2D>().velocity = pos.normalized * playerSpeed;

        if (didGetInput == false)
        {
            //SetFaceDir(facingDir.none, false);
            ani.SetBool("IsMoving", false);
        }
    }

    enum FacingDir { right, left, up, down, none }

    void SetFaceDir(FacingDir facing, bool moving)
    {
        ani.SetBool("Side", facing == FacingDir.left || facing == FacingDir.right);
        ani.SetBool("Up", facing == FacingDir.up);
        ani.SetBool("Down",  facing == FacingDir.down);
        ani.SetBool("IsMoving", moving);
        Debug.Log($"{facing}, {moving}");

        if(facing == FacingDir.left)
            transform.localScale = new Vector2(-playerScale, playerScale);
        else
            transform.localScale = new Vector2(playerScale, playerScale);
    }
}
