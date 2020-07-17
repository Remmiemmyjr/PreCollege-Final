/***************************************************
File:           PlatformSpawnerLogic.cs
Authors:        Christopher Onorati
Last Updated:   7/1/2019
Last Version:   2019.1.4

Description:
  This is the script that manages the spawning of platforms.
  You will need to implement the logic to spawn a platform
  after a random amount of time between 1.5 and 2.5 seconds.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using UnityEngine;   //Allows access to various Unity functionality.  Most scripts will be "using" this.

public class PlatformSpawnerLogic : MonoBehaviour
{
    //Game Object (prefab) to use as the platform.  This is public so this property can be set in the editor.  You do ==NOT== need to set it, though.  I have done so for you.
    public GameObject m_PlatformPrefab;

    //Transform variable.  Use this to cache the Transform in the Start function below.
    Transform m_Transform;

    //Keeps track of how many platforms have been spawned.  You do not need to use this variable for the student code.
    int m_iSpawnedPlatformCount = 0;

    //How long to wait before the next spawn.  Default is to wait .5 seconds before the first spawn.  You will need to assign this after a platform spawns.
    float m_flSpawnDelay = 0.5f;

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
    * FUNCTION NAME: Update
    * DESCRIPTION  : The update function is called every frame.  This means the more code in this function, the more "expensive" it is.  Having an expensive function
    *                means you may get lag or other problems! You will need to write the following code:
    *                
    *                1.  Subtract Time.deltaTime from the Spawn Delay timer every update.
    *                2.  If the spawn Delay goes below or equals 0, then...
    *                3.  Spawn a platform between a random range of -4 and 4.
    *                4.  Reset the spawn delay timer to a random number between 1.5 and 2.5 seconds.
    *                
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void Update()
    {
        //STUDENT CODE GOES HERE
    }

    /**
    * FUNCTION NAME: SpawnPlatform
    * DESCRIPTION  : This function will be called by you in the Update function, though you do ==NOT== need to modify it.  Note you must pass the x position of the next platform to spawn
    *                as a parameter when calling this function.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void SpawnPlatform(int xPosition)
    {
        //Create a platform at the position of the spawner.
        GameObject newPlatform = GameObject.Instantiate(m_PlatformPrefab, m_Transform.position, Quaternion.identity);

        //Set the X position based on the offset passed into this function.
        newPlatform.transform.position = new Vector3(xPosition, newPlatform.transform.position.y, newPlatform.transform.position.z);

        //Another platform has been spawned.  Keep track of this for the slow speed up over time.
        m_iSpawnedPlatformCount += 1;

        //Speed multiplier.  Min value should be one.
        float flSpeedMultiplier = Mathf.Clamp(m_iSpawnedPlatformCount / 10.0f, 1, 5);

        //Scale the speed of the newly created platform based on how many have been made.
        newPlatform.GetComponent<PlatformLogic>().m_flPlatformSpeed *= flSpeedMultiplier;
    }
}
