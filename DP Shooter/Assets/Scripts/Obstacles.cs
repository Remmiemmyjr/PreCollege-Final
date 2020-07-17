using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    
    public enum ObjectType { Destructable, Indestructable, Moveable, Teleport }
    [Header("Object Types")]
    [Tooltip("Use this to declare what type of obstacle this asset is")]
    public ObjectType classification;
    [Tooltip("The lower the number, the farther the moveable box will travel when hit")]
    public float boxDistanceModifier = 75f;

    [Header("Useless")]
    private Color debugCollisionColor = Color.red;
    //public string WARNING = ("EVERYTHING BELOW IS USELESS");
    private LayerMask groundLayer;
    
    //Collision Types
    internal bool onBottom;
    internal bool onTop;
    //public bool onWall;
    internal bool rightWall;
    internal bool leftWall;

    //Control Collision (Booleans to turn them on/off)
    internal bool usingBottom = true;
    internal bool usingTop = true;
    internal bool usingRight = true;
    internal bool usingLeft = true;

    //Collision Values
    private float collisionRadius = 0.25f;
    private Vector2 bottomOffset, topOffset, rightOffset, leftOffset;

    private void Start()
    {
        if(classification == ObjectType.Moveable)
        {
            gameObject.tag = "MoveBox";
        }
        
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
            //Gets the direction of the first bullet to come in contact with it 

            //Normal impulse gets the force of the hit from the first bullet (treated similarly to speed), and is multiplied by the direction to get the velocity
            Vector2 impactVelocity = colContacts[0].relativeVelocity;
            Vector2 impactDirection = impactVelocity.normalized;
            Debug.Log($"Impact:{impactDirection}, Velocity: {impactVelocity}");


            switch (classification)
            {
                case ObjectType.Destructable:
                    //When set to this, the box is destroyed by the bullet
                    {
                        //Run Animation
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
                        //var bulletVelocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                        StartCoroutine(MoveOnHit(impactVelocity));
                        //BoxCollider2D triggerPlate = gameObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
                        //triggerPlate.isTrigger = true;
                        Debug.Log($"{impactVelocity}");
                        break;
                    }

                case ObjectType.Teleport:
                    //When set to this, the player will teleport to a calculated offset based off which side the box was hit on by a bullet
                    {
                        //Based off the bullets velocity, the player is teleported with an offset
                        //var localBulletDir = collision.gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
                        var bulletPos = collision.gameObject.transform.position;
                        //Normalized bullet velocity, subtracting the bullets position by the velocity to create an offset, "localscale" is the size of the player which helps determine how far to offset the player by (radius is 0.5, so we divide by 4 to get it near the box)                                                    
                        PlayerController.Player.transform.position = bulletPos - (Vector3)impactDirection * (PlayerController.Player.transform.localScale.magnitude / 4f);
                        break;
                    }
            }

            Destroy(collision.gameObject);
        }
        else
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
    
}
