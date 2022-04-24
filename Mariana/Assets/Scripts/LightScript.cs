using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public GameObject objectToDisable;
    public static bool disabled = false;
	void Start () 
    {
		
	}

	// Update is called once per frame
	void Update () 
    {
		if (Input.GetKeyDown(KeyCode.L))
        {
            objectToDisable.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            objectToDisable.SetActive(true);
        }
            
	}
}
