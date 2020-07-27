using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/****************************************
 * Author: Emmy Berg
 * Date: 7/27/2020
 * Description: This script determines the movement/follow patterns of enemy drones. 
 The type can be switched from basic up/down movement, player tracking, and a hybird version of both
 ***************************************/

public class EnemyBehavior : MonoBehaviour
{
    public enum EnemyType { UpDown, Follow, Hybrid}
    public EnemyType FollowBehavior;

    Rigidbody2D rb;
    Transform Player;
    public Vector2 acc;
    private Vector3 savePos;
    Vector3 toPlayer = new Vector3(0.0f, 0.0f, 0.0f);
    public float accRate = 3f;
    public int maxDistance = 5;
    public float followSpeed = 1.5f;
    bool following = false;
    bool canFollow = true;

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

        //Debug.Log($"{rb.velocity}, {acc}");
        
        switch(FollowBehavior)
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

    void Movement()
    {
        rb.velocity += (acc * Time.deltaTime) * accRate;
        //rb.velocity += vel * Time.deltaTime;
    }
    void FollowCondition()
    {
        //If the player is within a particular range, the enemys ability to track is enable
        if (Vector3.Distance(transform.position, Player.position) <= maxDistance)
        {
            following = canFollow;
        }
    }
    //Takes the players position and subtracts the position of the enemy from it in order to get a straight path to the player
    void Follow()
    {
        toPlayer = Player.position - transform.position;
        toPlayer = toPlayer.normalized * followSpeed;
        rb.velocity = toPlayer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        //If the drone hits something, it will be distracted and start a cooldown routine
        else
        {
            following = false;
            StartCoroutine(FollowCooldown());
            rb.velocity *= 0;
            Debug.Log("Collided");
            acc *= -1;
        }
    }

    //This co-routine constantly checks every whether or not the enemy's position has changed. If it hasnt, this means the enemy is stuck, and the movement will be reversed
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

    //The drone will resort to basic up/down movement, or remain still upon getting hit in order to give the player some breathing room
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


