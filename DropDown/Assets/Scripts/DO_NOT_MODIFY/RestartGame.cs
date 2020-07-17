/***************************************************
File:           RestartGame.cs
Authors:        Christopher Onorati
Last Updated:   7/1/2019
Last Version:   2019.1.4

Description:
  Logic to allow the player(s) to restart the game.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using UnityEngine;  //Allows access to various Unity functionality.  Most scripts will be "using" this.
using UnityEngine.SceneManagement;  //Access to scene manager to reload the game.

public class RestartGame : MonoBehaviour
{
    /**
    * FUNCTION NAME: Update
    * DESCRIPTION  : The update function is called every frame.  This means the more code in this function, the more "expensive" it is.  Having an expensive function
    *                means you may get lag or other problems!
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void Update()
    {
        //Reload the game when the R key is pressed.
        if(Input.GetKeyDown(KeyCode.R))
        {
            //Load the active scene.  Reloading the active scene (what is currently loaded) will effectivly reload the game.
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
