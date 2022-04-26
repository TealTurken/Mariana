using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    private GameObject allLights;
    private GameObject spareLights;
    private GameObject roomLights;
    private GameObject hallwayLights;
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
        if (chance >= 1 && chance <= 10) TurnOffHallwayLights();    // 2/4
        if (chance == 3 || chance == 4) TurnOffRoomLights();       // 3/4
        if (chance == 5 || chance == 6) TurnOffSpareLights();      // 4/4

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
        Debug.Log("All Lights are on");
    }

    public void TurnOffAllLights()
    {
        allLights.SetActive(false);
        alarm.SetActive(true);
        Debug.Log("All Lights are out");
    }
    public void TurnOffSpareLights()
    {
        spareLights.SetActive(false);
        alarm.SetActive(true);
        Debug.Log("The Spare Lights are out");
    }
    public void TurnOffRoomLights()
    {
        roomLights.SetActive(false);
        alarm.SetActive(true);
        Debug.Log("The Room Lights are out");
    }
    public void TurnOffHallwayLights()
    {
        hallwayLights.SetActive(false);
        alarm.SetActive(true);
        Debug.Log("The Hallway Lights are out");
    }

}
