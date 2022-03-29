using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePopup : MonoBehaviour
{
    public static bool GameisPaused = false;
    public GameObject minigameUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (GameisPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
            
        }
    }

    void Resume ()
    {
        minigameUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
    }

    void Pause ()
    {
        minigameUI.SetActive(true);
        Time.timeScale = 1f;
        GameisPaused = true;
    }
}
