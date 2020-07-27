﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************
 * Author: Emmy Berg
 * Date: 7/27/2020
 * Description: Box classifications are set up here, and the properties for each type are set up here 
 (for move, indestructable, destructable, and teleport boxes)
 ***************************************/

public class Obstacles : MonoBehaviour
{
    Audio aud;
    public enum ObjectType { Destructable, Indestructable, Moveable, Teleport }             //Classification for what type of obstacle this is
    [Header("Object Types")]
    [Tooltip("Use this to declare what type of obstacle this asset is")]
    public ObjectType classification;
    [Tooltip("The lower the number, the farther the moveable box will travel when hit")]
    public float boxDistanceModifier = 75f;                                                 //This number determines how far the box will move when shot
    public float fxSize = 15f;                                                              //How big the ripplefx can grow to
    public Color startColor = Color.blue;                                                   //What color the ripple starts at
    public Color endColor = Color.white;                                                    //The end color for the ripple over time. NEEDS TO HAVE TRANSPARENCY (alter the alpha channel)

    private void Start()
    {
        if(classification == ObjectType.Moveable)
        {
            //In case a designer forgot to tag the object, or the prefab is somehow missing the tag
            gameObject.tag = "MoveBox";
        }

        aud = GetComponent<Audio>();
    }

    private void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Number of Contacts {collision.contactCount}");
        if (collision.gameObject.tag == "Bullet")
        {
            //Gets a list of contacts (what collided with it)
            ContactPoint2D[] colContacts = new ContactPoint2D[collision.contactCount];
            collision.GetContacts(colContacts);

            //Gets the direction of the first bullet to come in contact with it [OUTDATED COMMENT]
            //Normal impulse gets the force of the hit from the first bullet (treated similarly to speed), and is multiplied by the direction to get the velocity [OUTDATED COMMENT]
            Vector2 impactVelocity = colContacts[0].relativeVelocity;
            Vector2 impactDirection = impactVelocity.normalized;
            Debug.Log($"Impact:{impactDirection}, Velocity: {impactVelocity}");


            switch (classification)
            {
                case ObjectType.Destructable:
                    //When set to this, the box is destroyed by the bullet
                    {
                        //Play particle fx
                        aud.PlayDestroy();
                        Destroy(this.gameObject);
                        break;
                    }


                case ObjectType.Indestructable:
                    //When set to this, nothing happens to the box when interacted with
                    {
                        break;
                    }


                case ObjectType.Moveable:
                    //When set to this, the box's position is transformed a particular distance based off the bullets velocity
                    {
                        StartCoroutine(MoveOnHit(impactVelocity));
                        Debug.Log($"{impactVelocity}");
                        break;
                    }

                case ObjectType.Teleport:
                    //When set to this, the player will teleport to a calculated offset based off which side the box was hit on by a bullet
                    {
                        var bulletPos = collision.gameObject.transform.position;
                        //Normalized bullet velocity, subtracting the bullets position by the velocity to create an offset, "localscale" is the size of the player which helps determine how far to offset the player by (radius is 0.5, so we divide by 4 to get it near the box)                                                    
                        Vector3 newPos = bulletPos - (Vector3)impactDirection * (PlayerController.Player.transform.localScale.magnitude / 4f);
                        //Player is teleported INSIDE this co-routine
                        StartCoroutine(TeleportFX(newPos));
                        break;
                    }
                    
            }

            Destroy(collision.gameObject);
        }
        //Ensures that the player cannot push the box themselves
        if(collision.gameObject.tag == "Player")
        {
            Rigidbody2D moveRB = gameObject.GetComponent<Rigidbody2D>();
            moveRB.velocity = new Vector2(0, 0);
            moveRB.bodyType = RigidbodyType2D.Static;
            Debug.Log($"Collided with {collision.gameObject.name}");
        }
    }

    //Co-Routines run parallel to the main loop, meaning that this code begins when declared and runs while the rest of the code is running from then on.
    //Uses bullet velocity to know how far to move it in a given direction. Moves it 1/10th 10 times.
    IEnumerator MoveOnHit(Vector2 bulletVelocity)
    {
        Rigidbody2D moveRB = gameObject.GetComponent<Rigidbody2D>();
        moveRB.bodyType = RigidbodyType2D.Dynamic;
        //Division causes it to increase its distance
        var moveVelocity = bulletVelocity / boxDistanceModifier;
        for (int i = 0; i < 10; i++)
        {
            //FIXED: CHANGE TRANSFORM!!! Its currently ignoring collision and moves past the walls!!! 
            //Decreases movement gradually
            moveVelocity = moveVelocity * 0.85f;
            moveRB.velocity = moveVelocity;
            Debug.Log($"Velocity = {moveVelocity}");
            yield return new WaitForSeconds(.05f);
        }
        moveRB.velocity = new Vector2();
    }

    IEnumerator TeleportFX(Vector3 newPos)
    {
        float timer = 0.0f;
        float duration = 0.35f;
        Vector3 startScale = new Vector3();
        Vector3 endScale = new Vector3(fxSize, fxSize, fxSize);

        //The ripple grows in size and transparency
        GameObject ripplefx = PlayerController.playerScript.ripplefx;
        SpriteRenderer sr = ripplefx.GetComponent<SpriteRenderer>();

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            sr.color = Color.Lerp(startColor, endColor, t);
            ripplefx.transform.localScale = Vector3.Lerp(startScale, endScale, t);            
            yield return null;
        }
        
        //The player is teleported after the ripple plays outward
        newPos.z = PlayerController.Player.transform.position.z; //Ensures we save the accurate z axis of the player, rather than have it get modified
        PlayerController.Player.transform.position = newPos;

        //The ripple plays in reverse
        timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            sr.color = Color.Lerp(endColor, startColor, t);
            ripplefx.transform.localScale = Vector3.Lerp(endScale, startScale, t);
            yield return null;
        }


        yield return null;
    }
                    
}
