using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject Door;
    GameObject Player;
    GameObject Box;
    Door doorScript;
    public bool isPressed;
    private List<GameObject> triggerEntities = new List<GameObject>();
    public int entitiesOn = 0;
    public int entitiesNeeded;

    //Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Box = GameObject.FindGameObjectWithTag("MoveBox");
        doorScript = Door.GetComponent<Door>();
        triggerEntities.Add(Player);
        triggerEntities.Add(Box);
        entitiesNeeded = doorScript.pressurePlates.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject == Player || trigger.gameObject == Box)
        {
            Debug.Log(entitiesNeeded);
            foreach (GameObject entity in triggerEntities)
            {
                entitiesOn++;
            }
            if (entitiesOn > entitiesNeeded)
            {
                Debug.Log("Door Open");
                OpenDoor();
            }
        }

        if (trigger.gameObject.tag == "MoveBox" || trigger.gameObject.tag == "Player")
        {
            Debug.Log("Door Open");
            OpenDoor();
        }

    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        //if (trigger.gameObject == Player || trigger.gameObject == Box)
        //{
        //    foreach (GameObject entity in triggerEntities)
        //    {
        //        entitiesOn--;
        //    }
        //    if (entitiesOn < entitiesNeeded)
        //    {
        //        Debug.Log("Door Open");
        //        OpenDoor();
        //    }
        //}
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
