using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    int count;

    void Start()
    {

    }

    
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Door")
        {
            if (count++ >= 1)
            {
                Destroy(this.gameObject);
            }
        }
    }
    
}
