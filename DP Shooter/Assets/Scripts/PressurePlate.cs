using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************
 * Author: Emmy Berg
 * Date: 7/27/2020
 * Description: Pressure plate objects are set up here. If an object with authority triggers a pressureplate, 
 the door it is linked to will open. This script determines which objects have the ability to trigger plates
 ***************************************/

public class PressurePlate : MonoBehaviour
{
    public GameObject Door;
    GameObject Player;
    GameObject Box;
    Door doorScript;
    public bool isPressed;
    private List<GameObject> triggerEntities = new List<GameObject>();
   
    int entitiesNeeded = 1;

    //Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Box = GameObject.FindGameObjectWithTag("MoveBox");
        doorScript = Door.GetComponent<Door>();
        entitiesNeeded = doorScript.pressurePlates.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        //If one of these gameobjects triggers the pressureplate, they are added to a list
        if (trigger.gameObject.tag == "MoveBox" || trigger.gameObject.tag == "Player")
        {
            triggerEntities.Add(trigger.gameObject);
            Debug.Log("Door Open");
            PlateActive();
        }
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        //Once the object that was on is removed from the pressureplate, that object is then remvoed from the list
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

    void PlateActive()
    {
        isPressed = true;
        doorScript.PressurePlateChanged();
    }

    void PlateNotActive()
    {
        isPressed = false;
        doorScript.PressurePlateChanged();
    }
}
