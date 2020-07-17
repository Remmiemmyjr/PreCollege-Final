using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject Door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.gameObject.tag == "MoveBox")
        {
            Debug.Log("Door Open");
            OpenDoor();
        }
        
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "MoveBox")
        {
            Debug.Log("Door Closed");
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        //Play Animation
        Door.SetActive(false);
    }

    void CloseDoor()
    {
        Door.SetActive(true);
    }
}
