using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    string message;
    public Text objectiveText;
    // Start is called before the first frame update
    void Start()
    {
        message = "Find First Key in house";
        objectiveText.text = "Objective : " + message;
    }

    public void NewObjective(string newMessage)
    {
        message = newMessage;
        objectiveText.text = "Objective : " + message;
    }
}
