using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    AmmoSystem ammo;
    Timer timer;
    Objective objective;
    PauseMenu pauseMenu;
    Instructions instructionsPickUp;
    // Start is called before the first frame update
    void Start()
    {
        ammo = gameObject.GetComponent<AmmoSystem>();
        timer = gameObject.GetComponent<Timer>();
        objective = gameObject.GetComponent<Objective>();
        pauseMenu = gameObject.GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeObjective(string newMsg)
    {
        objective.NewObjective(newMsg);
    }

    public void Pause()
    {
        pauseMenu.Pause();
    }

    public void GameOver()
    {
        pauseMenu.GameOver();
    }
}
