﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    internal static GameObject Player;     //Reference to the player 

    [Header("Player Settings")]
    public float playerSpeed = 3.5f;

    [Header("Bullet Settings")]
    public float shootSpeed = 10f;
    public GameObject Bullet;
    public Transform bulletOrigin;          //Saved transform that allows us to shoot/instantiate bullets from a particular location/offset
    private Vector3 target;                 //Position of the mouse


    void Start()
    {
        Cursor.visible = false;
        Player = this.gameObject;
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
        Vector3 bulletDirection;

        bulletDirection = target - bulletOrigin.position;

        GameObject firedBullet = Instantiate(Bullet, bulletOrigin.position, bulletOrigin.rotation);
        firedBullet.transform.Rotate(bulletDirection);
        firedBullet.GetComponent<Rigidbody2D>().velocity = bulletDirection.normalized * shootSpeed;

        //Debug.Log($"{target}, {bulletDirection}, {bulletDirection.normalized * shootSpeed}");
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
