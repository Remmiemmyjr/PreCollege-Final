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
        if (trigger.gameObject.tag == "MoveBox" || trigger.gameObject.tag == "Player")
        {
            triggerEntities.Add(trigger.gameObject);
            Debug.Log("Door Open");
            PlateActive();
        }

        //if (trigger.gameObject == Player)
        //{
        //    Debug.Log($"Entities Needed: {entitiesNeeded}");
        //    triggerEntities.Add(Player);
        //    Debug.Log($"Entities On: {entitiesOn}");


        //}
        //else if (trigger.gameObject == Box)
        //{
        //    Debug.Log($"Entities Needed: {entitiesNeeded}");
        //    triggerEntities.Add(Box);
        //    Debug.Log($"Entities On: {entitiesOn}");

        //    
        //}

        //foreach (GameObject entity in triggerEntities)
        //{
        //    entitiesOn++;
        //}

        //if (entitiesOn >= entitiesNeeded)
        //{
        //    Debug.Log("Door Open");
        //    OpenDoor();
        //}

    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        //if (trigger.gameObject == Player || trigger.gameObject == Box)
        //{
        //    triggerEntities.Remove(trigger.gameObject);
        //    entitiesOn--;

        //    if (triggerEntities.Count == 0)
        //    {
        //        Debug.Log("Door Closed");
        //        entitiesOn = 0;
        //        CloseDoor();
        //    }
        //}
        if (trigger.gameObject.tag == "MoveBox" || trigger.gameObject.tag == "Player")
        {
            triggerEntities.Remove(trigger.gameObject);
            if (triggerEntities.Count == 0)
            {
                Debug.Log("Door Closed");
                PlateNotActive();
            }
        }
    }

    void PlateActive()
    {
        //Play Animation

        isPressed = true;
        doorScript.PressurePlateChanged();
    }

    void PlateNotActive()
    {
        isPressed = false;
        doorScript.PressurePlateChanged();
    }
}
