using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject Door;
    Door doorScript;
    internal bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        doorScript = Door.GetComponent<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.gameObject.tag == "MoveBox" || trigger.gameObject.tag == "Player")
        {
            Debug.Log("Door Open");
            OpenDoor();
        }
        
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "MoveBox" || trigger.gameObject.tag == "Player")
        {
            Debug.Log("Door Closed");
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        //Play Animation

        isPressed = true;
        doorScript.PressurePlateChanged();
    }

    void CloseDoor()
    {
        isPressed = false;
        doorScript.PressurePlateChanged();
    }
}
