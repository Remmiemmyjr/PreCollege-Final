using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************
 * Author: Emmy Berg
 * Date: 7/27/2020
 * Description: This script finds what objects can open the door. The pressureplates that can open this door are linked in here.
 ***************************************/

public class Door : MonoBehaviour
{
    public GameObject[] pressurePlates;
    internal List<PressurePlate> listOfPlates = new List<PressurePlate>();

    // Start is called before the first frame update
    void Start()
    {
        UpdatePressurePlates();
    }


    // Update is called once per frame
    void Update()
    {
    }

    private void UpdatePressurePlates()
    {
        listOfPlates.Clear();

        foreach(GameObject plate in pressurePlates)
        {
            PressurePlate temp = plate.GetComponent<PressurePlate>();
            //If a designer forgot to hook up a pressureplate this will ensure regardless its hooked up to the door both ways (makes sure the pressure plate is pointing back to the door)
            temp.Door = this.gameObject;
            listOfPlates.Add(temp);
        }
    }
    //The pressure plate calls this when its been activated
    public void PressurePlateChanged()
    {
        foreach(PressurePlate plate in listOfPlates)
        {
            //Only trigger plates if all have been pressed
            if(!plate.isPressed)
            {
                CloseDoor();
                return;
            }
        }
        OpenDoor();
    }

    void OpenDoor()
    {
        //Play Animation

        gameObject.SetActive(false);
    }

    void CloseDoor()
    {
        gameObject.SetActive(true);
    }
}
