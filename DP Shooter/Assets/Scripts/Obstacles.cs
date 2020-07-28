using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************
 * Author: Emmy Berg
 * Date: 7/27/2020
 * Description: 
 ***************************************/
/***************************************************
File:           BulletBehavior.cs
Authors:        Emmy Berg
Last Updated:   7/27/2020
Last Version:   2019.3.11

Description:
Box classifications are set up here, and the properties 
for each type are set up here (for move, indestructable, 
destructable, and teleport boxes)

***************************************************/

public class Obstacles : MonoBehaviour
{
    Audio aud;

    //Object Type
    public enum ObjectType { Destructable, Indestructable, Moveable, Teleport } 

    [Header("Object Types")]
    [Tooltip("Use this to declare what type of obstacle this asset is")]
    public ObjectType classification;
    [Tooltip("The lower the number, the farther the moveable box will travel when hit")]

    //How far the box moves
    public float boxDistanceModifier = 75f;

    //How big the ripplefx can grow to
    public float fxSize = 15f;

    //What color the ripple starts at
    public Color startColor = Color.blue;

    //The end color for the ripple over time. NEEDS TO HAVE TRANSPARENCY (alter the alpha channel)
    public Color endColor = Color.white;

    public ParticleSystem destroyParticle;

    public ParticleSystem teleportParticle;

    private void Start()
    {
        if(classification == ObjectType.Moveable)
        {
            //In case a designer forgot to tag the object, or the prefab is somehow missing the tag
            gameObject.tag = "MoveBox";
        }

        aud = GetComponent<Audio>();
    }

    //Determines what happens when shot
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Number of Contacts {collision.contactCount}");
        if (collision.gameObject.tag == "Bullet")
        {
            //Gets a list of contacts (what collided with it)
            ContactPoint2D[] colContacts = new ContactPoint2D[collision.contactCount];
            collision.GetContacts(colContacts);

            //The direction it goes in is based off velocity
            Vector2 impactVelocity = colContacts[0].relativeVelocity;
            Vector2 impactDirection = impactVelocity.normalized;
            Debug.Log($"Impact:{impactDirection}, Velocity: {impactVelocity}");


            switch (classification)
            {
                case ObjectType.Destructable:
                    //Bullet destroys the box
                    {
                        StartCoroutine(WaitToDestroy());
                        
                        break;
                    }


                case ObjectType.Indestructable:
                    //Nothing happens
                    {
                        break;
                    }


                case ObjectType.Moveable:
                    //Box's position is transformed based off bullets vel
                    {
                        StartCoroutine(MoveOnHit(impactVelocity));
                        Debug.Log($"{impactVelocity}");
                        break;
                    }

                case ObjectType.Teleport:
                    //Player is teleported to the box
                    {
                        var bulletPos = collision.gameObject.transform.position;

                        //NOTE FOR EMMY: Normalized bullet velocity, subtracting the bullets position by the velocity to create an offset, "localscale" is the size of the player which helps determine how far to offset the player by (radius is 0.5, so we divide by 4 to get it near the box) 
                        
                        Vector3 newPos = bulletPos - (Vector3)impactDirection * (PlayerController.Player.transform.localScale.magnitude / 4f);

                        //Player is teleported INSIDE this co-routine
                        StartCoroutine(TeleportFX(newPos));
                        aud.PlayTeleport();
                        break;
                    }
                    
            }

            Destroy(collision.gameObject);
        }
        //Player cant push the box
        if(collision.gameObject.tag == "Player")
        {
            Rigidbody2D moveRB = gameObject.GetComponent<Rigidbody2D>();
            moveRB.velocity = new Vector2(0, 0);
            moveRB.bodyType = RigidbodyType2D.Static;
            Debug.Log($"Collided with {collision.gameObject.name}");
        }
    }

    //NOTE FOR EMMY: Co-Routines run parallel to the main loop, meaning that this code begins when declared and runs while the rest of the code is running from then on.


    IEnumerator WaitToDestroy()
    {
        destroyParticle.Play();
        aud.PlayDestroy();
        this.gameObject.transform.localScale = Vector3.Lerp(this.gameObject.transform.localScale, new Vector3(0.01f, 0.01f), 2f);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
    

    //Uses bullet velocity to know which way to move
    IEnumerator MoveOnHit(Vector2 bulletVelocity)
    {
        Rigidbody2D moveRB = gameObject.GetComponent<Rigidbody2D>();
        moveRB.bodyType = RigidbodyType2D.Dynamic;
        //Division causes it to increase its distance
        var moveVelocity = bulletVelocity / boxDistanceModifier;
        for (int i = 0; i < 10; i++)
        { 
            //Decreases movement gradually
            moveVelocity = moveVelocity * 0.85f;
            moveRB.velocity = moveVelocity;
            Debug.Log($"Velocity = {moveVelocity}");
            yield return new WaitForSeconds(.05f);
        }
        moveRB.velocity = new Vector2();
    }

    //Teleports player, and plays ripple fx
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
        newPos.z = PlayerController.Player.transform.position.z; 
        PlayerController.Player.transform.position = newPos;
        teleportParticle.Play();

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
