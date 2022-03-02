using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
    public UnityEvent KeyPicked;
    public Outline outlineScript;
    Instructions instructions;


    private void Start()
    {
        instructions = GetComponent<Instructions>();
    }
    // Start is called before the first frame update
    private void Update()
    {

    }

    public void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.F))
        {
            gameObject.SetActive(false);
            KeyPicked?.Invoke();
            instructions.DisableInstructions();
            return;
        }
        outlineScript.enabled = true;
    }
    public void OnTriggerExit(Collider other)
    {
        outlineScript.enabled = false;
    }
}
