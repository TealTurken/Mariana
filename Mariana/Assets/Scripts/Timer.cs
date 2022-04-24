using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    bool timerActive;
    public float timeValue=300;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI labelText;
    public TextMeshProUGUI rescueText;
    public GameObject contactUI;

    // Update is called once per frame
    
    
    void Start ()
    {
        timerActive = false;
        timeValue = 300;

        rescueText.gameObject.SetActive(false);
        contactUI.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        labelText.gameObject.SetActive(false);
    }
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.C))
        {
            contactUI.gameObject.SetActive(true);
        }
        
        if (timerActive)
        {
            if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
            else
        {
            timeValue = 0;
        }

        DisplayTime(timeValue);
        }
    }

    void DisplayTime (float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
            rescueText.gameObject.SetActive(true);
        }
        else if (timeToDisplay > 0)
        {
            timeToDisplay += 1;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);   
    }

    public void StartTimer()
    {
        timerActive = true;
        contactUI.gameObject.SetActive(false);
        timerText.gameObject.SetActive(true);
        labelText.gameObject.SetActive(true);
    }
}
