using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    private Vector3 targetPos;
    public float speed = 20f;

    void Start()
    {
        targetPos = transform.position;
    }

    void Update()
    {
        //float distance = transform.position.z + Camera.main.transform.position.z;
        //targetPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        //targetPos = Camera.main.ScreenToWorldPoint(targetPos);

        //Vector3 followXonly = new Vector3(targetPos.x, targetPos.y, transform.position.z);
        //transform.position = Vector3.Lerp(transform.position, followXonly, speed * Time.deltaTime);

        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = transform.position.z;

        transform.position = targetPos;
    }
}
