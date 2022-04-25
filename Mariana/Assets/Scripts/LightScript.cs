using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public GameObject objectToDisable;
    public GameObject audioToDisable;
    public static bool disabled = false;
	void Start () 
    {
		
	}

	// Update is called once per frame
	void Update () 
    {
		if (Input.GetKeyDown(KeyCode.Q))
        {
            objectToDisable.SetActive(false);
            audioToDisable.SetActive(true);

        }
            
        if (Input.GetKeyDown(KeyCode.K))
        {
            objectToDisable.SetActive(true);
            audioToDisable.SetActive(false);
        }
	}
}
