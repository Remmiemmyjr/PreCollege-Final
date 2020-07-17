/***************************************************
File:           QuitGame.cs
Authors:        Christopher Onorati
Last Updated:   6/3/2019
Last Version:   2019.1.4

Description:
  Logic to allow the player(s) to quit the game.  You
  only need to write two lines of code!

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using UnityEngine;  //Allows access to various Unity functionality.  Most scripts will be "using" this.

public class QuitGame : MonoBehaviour
{
    /**
    * FUNCTION NAME: Update
    * DESCRIPTION  : The update function is called every frame.  This means the more code in this function, the more "expensive" it is.  Having an expensive function
    *                means you may get lag or other problems!  You need to write two lines of code in this function to get the game to exit when you press escape.
    *                Remember, this will not work in Unity, only in your exported game.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void Update()
    {
        //STUDENT CODE GOES HERE
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
