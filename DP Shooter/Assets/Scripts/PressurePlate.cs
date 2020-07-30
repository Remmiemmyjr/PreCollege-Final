using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************
File:           PressurePlate.cs
Authors:        Emmy Berg
Last Updated:   7/27/2020
Last Version:   2019.3.11

Description:
Pressure plate objects are set up here. If an object 
with authority triggers a pressureplate, the door it 
is linked to will open. This script determines which 
objects have the ability to trigger plates

***************************************************/

public class PressurePlate : MonoBehaviour
{
    public Sprite on;
    public Sprite off;

    private List<GameObject> triggerEntities = new List<GameObject>();
    public GameObject Door;
    GameObject Player;
    GameObject Box;

    Door doorScript;
    Audio aud;

    public bool isPressed;
   
    int entitiesNeeded = 1;

    //Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = off;

        Player = GameObject.FindGameObjectWithTag("Player");

        Box = GameObject.FindGameObjectWithTag("MoveBox");

        doorScript = Door.GetComponent<Door>();

        entitiesNeeded = doorScript.pressurePlates.Length;

        aud = GetComponent<Audio>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Anything that enters is added to a list (Door Open)
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "MoveBox" || trigger.gameObject.tag == "Player")
        {
            triggerEntities.Add(trigger.gameObject);
            Debug.Log("Door Open");
            PlateActive();
        }
    }

    //When an object exits, its removed from the list (Door Closes)
    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "MoveBox" || trigger.gameObject.tag == "Player")
        {
            triggerEntities.Remove(trigger.gameObject);
            //Once the list is 0, the door will close (this is to ensure that it will remain open if more than one object is on it
            if (triggerEntities.Count == 0)
            {
                Debug.Log("Door Closed");
                PlateNotActive();
            }
        }
    }

    //Somethings on the plate
    void PlateActive()
    {
        aud.PlayActivated();
        this.GetComponent<SpriteRenderer>().sprite = on;
        isPressed = true;
        doorScript.PressurePlateChanged();
    }

    //Nothing on the plate
    void PlateNotActive()
    {
        this.GetComponent<SpriteRenderer>().sprite = off;
        isPressed = false;
        doorScript.PressurePlateChanged();
    }
}
