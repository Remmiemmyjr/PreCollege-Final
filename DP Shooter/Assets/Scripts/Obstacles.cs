using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    //Object Classification
    public enum ObjectType { Destructable, Indestructable, Moveable, Teleport }
    public ObjectType classification;

    //Gizmo Properties
    private Color debugCollisionColor = Color.red;
    public LayerMask groundLayer;

    //Collision Types
    internal bool onBottom;
    internal bool onTop;
    //public bool onWall;
    internal bool rightWall;
    internal bool leftWall;

    //Control Collision (Booleans to turn them on/off)
    public bool usingBottom = true;
    public bool usingTop = true;
    public bool usingRight = true;
    public bool usingLeft = true;

    //Collision Values
    public float collisionRadius = 0.25f;
    public float moveValue = 75f;
    public Vector2 bottomOffset, topOffset, rightOffset, leftOffset;

    private void Start()
    {

    }

    private void Update()
    {
        UsingCollision();
    }

    void UsingCollision()
    {
        if (usingBottom)
        {
            onBottom = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        }

        if (usingTop)
        {
            onTop = Physics2D.OverlapCircle((Vector2)transform.position + topOffset, collisionRadius, groundLayer);
        }

        if (usingRight)
        {
            rightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        }

        if (usingLeft)
        {
            leftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, topOffset, rightOffset, leftOffset };
        if (usingBottom)
        {
            Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        }
        if (usingTop)
        {
            Gizmos.DrawWireSphere((Vector2)transform.position + topOffset, collisionRadius);
        }
        if (usingRight)
        {
            Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        }
        if (usingLeft)
        {
            Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Bullet")
        {
            switch (classification)
            {
                case ObjectType.Destructable: //Destroys block when hit with bullet
                    //Run Animation
                    Destroy(this.gameObject);
                    break;
                case ObjectType.Indestructable:
                    break;
                case ObjectType.Moveable:
                    {
                        //Based off the bullets velocity, the box is moved
                        var bulletVelocity = trigger.gameObject.GetComponent<Rigidbody2D>().velocity;
                        Debug.Log($"{bulletVelocity}");
                        StartCoroutine(MoveOnHit(bulletVelocity));
                        break;
                    }

                case ObjectType.Teleport:
                    //Teleport player to the object
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
        var moveDistance = bulletVelocity / moveValue;
        for (int i = 0; i < 10; i++)
        {

            //Decreases movement gradually
            moveDistance = moveDistance * 0.85f;
            transform.Translate(moveDistance);
            yield return new WaitForSeconds(.05f);
        }
    }
}
