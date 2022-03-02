using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistance : MonoBehaviour
{
    public static float distanceFromTarget;
    public float target;

    // Update is called once per frame
    void Update()
    {
        SetDistance();
    }

    public void SetDistance()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            target = hit.distance;
            distanceFromTarget = target;
        }
    }
}
