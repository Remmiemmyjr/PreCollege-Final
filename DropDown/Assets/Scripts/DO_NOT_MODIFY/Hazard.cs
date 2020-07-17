/***************************************************
File:           Hazard.cs
Authors:        Christopher Onorati
Last Updated:   7/1/2019
Last Version:   2019.1.4

Description:
  Logic for hazards that kill the player character
  on touch.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using UnityEngine;  //Allows access to various Unity functionality.  Most scripts will be "using" this.

//This script requires the game object to have a Box Collider 2D to work.
[RequireComponent(typeof(BoxCollider2D))]
public class Hazard : MonoBehaviour
{
    /**
    * FUNCTION NAME: OnCollisionEnter2D
    * DESCRIPTION  : The OnCollisionEnter2D function is called the first frame two game objects with 2D colliders hit each other.  Note that if you leave off the 2D at the end,
    *                the function will detect collision with 3D colliders instead.
    * INPUTS       : collision - This class (Collision2D) holds information regarding the game objects involved in the collision.
    * OUTPUTS      : None
    **/
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Check via Unity's tag system to see if the game object that hit the hazard is the player.
        if(collision.gameObject.tag == "Player")
        {
            //Destroy the object that hit the hazard as we know it is the player.
            GameObject.Destroy(collision.gameObject);
        }
    }
}
