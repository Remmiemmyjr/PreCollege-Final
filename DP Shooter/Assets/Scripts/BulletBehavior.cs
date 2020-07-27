using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************
 * Author: Emmy Berg
 * Date: 7/27/2020
 * Description: If a bullet ricochets off the environement, it will be destroyed on the next collision it makes
 ***************************************/

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
