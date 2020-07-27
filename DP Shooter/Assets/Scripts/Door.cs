using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************
File:           Door.cs
Authors:        Emmy Berg
Last Updated:   7/27/2020
Last Version:   2019.3.11

Description:
This script finds what objects can open the door. 
The pressureplates that can open this door are linked in 
here.

***************************************************/

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
            //Hooks up pressureplate to door
            PressurePlate temp = plate.GetComponent<PressurePlate>();
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
        gameObject.SetActive(false);
    }

    void CloseDoor()
    {
        gameObject.SetActive(true);
    }
}
