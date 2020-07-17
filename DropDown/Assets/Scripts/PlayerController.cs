/***************************************************
File:           PlayerController.cs
Authors:        Christopher Onorati
Last Updated:   7/1/2019
Last Version:   2019.1.4

Description:
  This is the controller for the player character.  You
  will need to implement the controller to allow the
  player to move left with the A or Left Arrow Key,
  and move right with the D or Right Arrow Key.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using UnityEngine;  //Allows access to various Unity functionality.  Most scripts will be "using" this.

//Require a Rigidbody 2D component to be on the game object in order to have a player controller.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //Rigidbody2D variable.  Use this to cache the Rigidbody 2D in the Start function below.
    Rigidbody2D m_RigidBody2D;

    //Speed of the player.  This should be used in the FixedUpdate function when applying a velocity to the cached Rigidbody 2D.
    float m_flPlayerSpeed = 7.0f;

    /**
    * FUNCTION NAME: Start
    * DESCRIPTION  : The start function is called the first frame the script is active.  This script is useful to cache components and initialize variables.  You will need to cache the
    *                Rigidbody2D component here.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void Start()
    {
        m_RigidBody2D = gameObject.GetComponent<RigidBody2D>();
        //STUDENT CODE GOES HERE
    }

    /**
    * FUNCTION NAME: FixedUpdate
    * DESCRIPTION  : The FixedUpdate function is used when setting the position of a game object, or using the physics system.  This funciton has the same properties as Update in terms of
    *                attempting to keep it as inexpensive as possible.  You need to write the code to do the following:
    *                
    *                1.  Check if a valid key is pressed (A, D, Left Arrow, Right Arrow).
    *                2.  Apply the velocity to the player.  You can do this by setting the velocity of the Rigidbody.  Note you will need to use a new Vector2 to apply the velocity.
    *                3.  If no valid keys are pressed, set the velocity to be a Vector2 that has 0 in the x, and the current velocity in the y.
    *                
    *                NOTE:  You will want to set the Y velocity in your assignments to be the current Y velocity.
    *                
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void FixedUpdate()
    {
        //STUDENT CODE GOES HERE.
    }
}
