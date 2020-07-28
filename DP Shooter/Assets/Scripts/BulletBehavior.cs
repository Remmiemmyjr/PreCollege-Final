using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************
File:           BulletBehavior.cs
Authors:        Emmy Berg
Last Updated:   7/27/2020
Last Version:   2019.3.11

Description:
If a bullet ricochets off the environement, it will be 
destroyed on the next collision it makes

***************************************************/

public class BulletBehavior : MonoBehaviour
{
    int count;
    public Color color = Color.blue;
    public SpriteRenderer sr;

    void Start()
    {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Door")
        {
            sr.color = color;
            if (count++ >= 1)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
