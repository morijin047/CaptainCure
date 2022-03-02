using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public void UnlockDoor()
    {
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
