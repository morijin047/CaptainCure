using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class Score : MonoBehaviour
{
    const int NUM_OF_KEYS = 2;
    public int keyNumber = 0;
    bool isWin;

    public UnityEvent gameWon;
    public Text scoreText;
    public GameObject screenWin;
    PauseMenu menu;
    public void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        keyNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Key " + keyNumber;
    }

    public void KeyPickedUp()
    {
        keyNumber++;
        if (keyNumber >= NUM_OF_KEYS)
            WinGame();
    }

    public void WinGame()
    {

        screenWin.SetActive(true);
        gameWon?.Invoke();
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void ChangeScene(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Retry()
    {
        Debug.Log("Game Restart");
        screenWin.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
