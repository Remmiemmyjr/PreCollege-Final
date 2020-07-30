using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************
File:           BulletBehavior.cs
Authors:        Emmy Berg
Last Updated:   7/27/2020
Last Version:   2019.3.11

Description:
If a bullet ricochets off the environement, it will be 
destroyed on the next collision it makes

***************************************************/

public class BulletBehavior : MonoBehaviour
{
    int count;
    public Color color = Color.blue;
    public SpriteRenderer sr;
    Animator ani;

    void Start()
    {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Door")
        {
            sr.color = color;
            if (count++ >= 1)
            {
                StartCoroutine(BulletKiller());
                
            }
        }
    }

    private IEnumerator BulletKiller()
    {
        ani.SetBool("Died", true);
        this.gameObject.transform.localScale = Vector3.Lerp(this.gameObject.transform.localScale, new Vector3(0.01f, 0.01f), 2f);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
