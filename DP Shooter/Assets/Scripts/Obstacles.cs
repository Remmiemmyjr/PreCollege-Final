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
    
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Bullet")
        {
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
                        var bulletVelocity = trigger.gameObject.GetComponent<Rigidbody2D>().velocity;
                        StartCoroutine(MoveOnHit(bulletVelocity));
                        //BoxCollider2D triggerPlate = gameObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
                        //triggerPlate.isTrigger = true;
                        Debug.Log($"{bulletVelocity}");
                        break;
                    }

                case ObjectType.Teleport:
                    //When set to this, the player will teleport to a calculated offset based off which side the box was hit on by a bullet
                    {
                        //Based off the bullets velocity, the player is teleported with an offset
                        var localBulletDir = trigger.gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
                        var bulletPos = trigger.gameObject.transform.position;
                        //Normalized bullet velocity, subtracting the bullets position by the velocity to create an offset, "localscale" is the size of the player which helps determine how far to offset the player by (radius is 0.5, so we divide by 4 to get it near the box)                                                    
                        PlayerController.Player.transform.position = bulletPos - (Vector3)localBulletDir * (PlayerController.Player.transform.localScale.magnitude / 4f);
                        break;
                    }
            }

            Destroy(trigger.gameObject);
        }
    }

    //Co-Routines run parallel to the main loop, meaning that this code begins when declared and runs while the rest of the code is running from then on.
    //Uses bullet velocity to know how far to move it in a given direction. Moves it 1/10th 10 times.
    IEnumerator MoveOnHit(Vector2 bulletVelocity)
    {
        //Division causes it to increase its distance
        var moveDistance = bulletVelocity / boxDistanceModifier;
        for (int i = 0; i < 10; i++)
        {
            //TODO: CHANGE TRANSFORM!!! Its currently ignoring collision and moves past the walls!!! 
            //Decreases movement gradually
            moveDistance = moveDistance * 0.85f;
            transform.Translate(moveDistance);
            yield return new WaitForSeconds(.05f);
        }
    }
    
}
