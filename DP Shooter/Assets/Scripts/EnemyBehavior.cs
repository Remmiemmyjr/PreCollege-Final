using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/***************************************************
File:           EnemyBehavior.cs
Authors:        Emmy Berg
Last Updated:   7/27/2020
Last Version:   2019.3.11

Description:
 This script determines the movement/follow patterns 
 of enemy drones. The type can be switched from basic 
 up/down movement, player tracking, and a hybird version 
 of both
 
***************************************************/
public class EnemyBehavior : MonoBehaviour
{
    public enum EnemyType { UpDown, Follow, Hybrid}
    public EnemyType FollowBehavior;

    Rigidbody2D rb;
    Transform Player;

    public Vector2 acc;
    private Vector3 savePos;
    Vector3 toPlayer = new Vector3(0.0f, 0.0f, 0.0f);

    public int maxDistance = 5;
    public float followSpeed = 1.5f;
    public float accRate = 3f;
    bool following = false;
    bool canFollow = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        savePos = new Vector3();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Unstuck());
    }

    // Update is called once per frame
    void Update()
    {
        Behavior();
        
        //Debug.Log($"{rb.velocity}, {acc}");
    }

    //Calls the appropriate function for each type
    void Behavior()
    {
        switch (FollowBehavior)
        {
            case EnemyType.UpDown:
                Movement();
                break;
            case EnemyType.Follow:
                {
                    FollowCondition();
                    if (following)
                    {
                        Follow();
                    }
                    break;
                }
            case EnemyType.Hybrid:
                {
                    FollowCondition();
                    if (following)
                    {
                        Follow();
                    }
                    else
                    {
                        Movement();
                    }
                    break;
                }
        }
    }

    //Moves up and down, slight accel to show weight
    void Movement()
    {
        rb.velocity += (acc * Time.deltaTime) * accRate;
        //rb.velocity += vel * Time.deltaTime;
    }

    //Determines if enemies in range to follow
    void FollowCondition()
    {
        //If the player is within a particular range, the enemys ability to track is enable
        if (Vector3.Distance(transform.position, Player.position) <= maxDistance)
        {
            following = canFollow;
        }
    }

    //Moves toward the player
    void Follow()
    {
        toPlayer = Player.position - transform.position;
        toPlayer = toPlayer.normalized * followSpeed;
        rb.velocity = toPlayer;
    }

    //Restarts level when touching the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        //When it hits something else, reverse movement
        else
        {
            following = false;
            StartCoroutine(FollowCooldown());
            rb.velocity *= 0;
            Debug.Log("Collided");
            acc *= -1;
        }
    }

    //Checks if the position has changed. If not, reverse movement
    private IEnumerator Unstuck()
    {
        while (true)
        {
            if (savePos == transform.position)
            {
                rb.velocity *= 0;
                acc *= -1;
            }
            savePos = transform.position;
            yield return new WaitForSeconds(0.75f);
        }
    }

    //Waits to follow player again
    private IEnumerator FollowCooldown()
    {
        if(canFollow)
        {
            canFollow = false;
            yield return new WaitForSeconds(3f);
            canFollow = true;
        }
    }
  
}


