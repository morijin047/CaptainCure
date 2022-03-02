using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public UnityEvent timerOver;
    float startTime;
    float endTime = 600f;
    bool over;
    // Start is called before the first frame update
    void Start()
    {
        startTime = endTime;
        over = false;
    }

    // Update is called once per frame
    void Update()
    {
        BeginManager();
    }

    public void BeginManager()
    {
        if (!over)
        {
            startTime -= Time.deltaTime;
            string min = ((int)startTime / 60).ToString("D2");
            string seconds = ((int)startTime % 60).ToString("D2");
            timerText.text = min + ":" + seconds;
            if (startTime < 60)
                timerText.color = Color.red;
            if (startTime <= 0)
            {
                over = true;
                timerOver?.Invoke();
            }
        }
    }
}
