using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    private GameObject allLights;
    private GameObject spareLights;
    private GameObject roomLights;
    private GameObject hallwayLights;
    private GameObject audioToDisable;
    public GameObject alarm;
    private static bool disabled = false;
    private int chance;
	void Start () 
    {
        allLights = GameObject.Find(name: "All Lights");
        spareLights = GameObject.Find(name: "Spare Lights");
        roomLights = GameObject.Find(name: "Room Lights");
        hallwayLights = GameObject.Find(name: "Hallway Lights");
	}

	// Update is called once per frame
	void Update () 
    {
        // Chance of lights turning off. EDIT THESE VALUES TO CHANGE THE CHANCE
        chance = Random.Range(0, 99999);
        
        // Each below has a 1 in 4 chances of triggering
        if (chance == 0) TurnOffAllLights();            // 1/4
        if (chance == 33333) TurnOffHallwayLights();    // 2/4
        if (chance == 66666) TurnOffRoomLights();       // 3/4
        if (chance == 99999) TurnOffSpareLights();      // 4/4

		if (Input.GetKeyDown(KeyCode.Q))
        {
            TurnOffAllLights();
            alarm.SetActive(true);
            //audioToDisable.SetActive(true);

        }
            
        if (Input.GetKeyDown(KeyCode.K))
        {
            alarm.SetActive(false);
            allLights.SetActive(true);
            spareLights.SetActive(true);
            roomLights.SetActive(true);
            hallwayLights.SetActive(true);
            //audioToDisable.SetActive(false);
        }
	}

    public void TurnOnAllLights()
    {
        alarm.SetActive(false);
        allLights.SetActive(true);
        spareLights.SetActive(true);
        roomLights.SetActive(true);
        hallwayLights.SetActive(true);
    }

    public void TurnOffAllLights()
    {
        allLights.SetActive(false);
        alarm.SetActive(true);
    }
    public void TurnOffSpareLights()
    {
        spareLights.SetActive(false);
        alarm.SetActive(true);
    }
    public void TurnOffRoomLights()
    {
        roomLights.SetActive(false);
        alarm.SetActive(true);
    }
    public void TurnOffHallwayLights()
    {
        hallwayLights.SetActive(false);
        alarm.SetActive(true);
    }

}
