using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    bool gameOver;
    UIManager UIManager;
    enum State { Key, PadLock };
    State stateOfGame;
    // Start is called before the first frame update
    void Start()
    {
        stateOfGame = State.Key;
        gameOver = false;
        UIManager = gameObject.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Pause();
        }
    }

    public void KeyUnlock()
    {
        UIManager.ChangeObjective("Find last key outside");
        stateOfGame = State.PadLock;
    }


}
