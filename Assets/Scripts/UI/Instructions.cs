using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{

    public GameObject actionInput;
    public GameObject actionText;
    public GameObject key;
    public float distance;
    private float distanceToTarget = 2;

    void Update()
    {
        distance = PlayerDistance.distanceFromTarget;
    }

    private void OnTriggerStay(Collider other)
    {
        if (distance <= distanceToTarget)
        {
            EnableInstructions();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        DisableInstructions();
    }

    public void EnableInstructions()
    {
        actionInput.SetActive(true);
        actionText.SetActive(true);
    }
    public void DisableInstructions()
    {
        actionInput.SetActive(false);
        actionText.SetActive(false);
    }
}
