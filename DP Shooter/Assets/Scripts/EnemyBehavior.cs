using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
    Rigidbody2D rb;
    Transform Player;
    public Vector2 acc;
    private Vector3 savePos;
    Vector3 toPlayer = new Vector3(0.0f, 0.0f, 0.0f);
    int maxDistance = 5;
    float followSpeed = 1.5f;
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
        if (Vector3.Distance(transform.position, Player.position) <= maxDistance)
        {
            following = canFollow;
        }
        if (following)
        {
            Follow();
        }
        else
        {
            Movement();
        }

        //Debug.Log($"{rb.velocity}, {acc}");
        

    }

    void Movement()
    {
        rb.velocity += (acc * Time.deltaTime) * 3f;
        //rb.velocity += vel * Time.deltaTime;
    }
    private void Follow()
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
        else
        {
            following = false;
            StartCoroutine(FollowCooldown());
            rb.velocity *= 0;
            Debug.Log("Collided");
            acc *= -1;
        }
    }

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


