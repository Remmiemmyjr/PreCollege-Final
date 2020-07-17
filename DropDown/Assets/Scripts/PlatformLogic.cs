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

using UnityEngine;   //Allows access to various Unity functionality.  Most scripts will be "using" this.

public class PlatformLogic : MonoBehaviour
{
    //Transform variable.  Use this to cache the Transform in the Start function below.
    Transform m_Transform;

    //Speed of the platforms.  This should be used in the FixedUpdate function when modifying the position of the platform.
    //This is public so the platformer spawner can modify the base speed as the game progresses, but is hidden in the inspector so this data does not appear in the editor.
    [HideInInspector]
    public float m_flPlatformSpeed = 1.5f;

    /**
    * FUNCTION NAME: Start
    * DESCRIPTION  : The start function is called the first frame the script is active.  This script is useful to cache components and initialize variables.  You will need to cache the
    *                Transform component here.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void Start()
    {
        //STUDENT CODE GOES HERE
    }

    /**
    * FUNCTION NAME: FixedUpdate
    * DESCRIPTION  : The FixedUpdate function is used when setting the position of a game object, or using the physics system.  This funciton has the same properties as Update in terms of
    *                attempting to keep it as inexpensive as possible.
    *                
    *                For this function you will need to assign a new position every frame.  The position of the game object can be found in the cached Transform component.  You can use the
    *                += operator to assign a new value to the transform.  Remember to use Time.deltaTime to make the speed consistent across all frame rates!
    *                
    *                NOTE:  You will want to use a vector 3 to set the position, as 2D games still have a Z (depth) axis.
    *                
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void FixedUpdate()
    {
        //STUDENT CODE GOES HERE
    }

    /**
    * FUNCTION NAME: Update
    * DESCRIPTION  : The update function is called every frame.  This means the more code in this function, the more "expensive" it is.  Having an expensive function
    *                means you may get lag or other problems!  This code checks if the Y value of the platform has gone so high that it should be destroyed. 
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void Update()
    {
        //Destroy platforms if they go above 6 units in the Y axis.
        if (m_Transform.position.y >= 6.0f)
        {
            //Destroy the Game Object this script is on (AKA the platform).
            GameObject.Destroy(gameObject);
        }
    }
}
