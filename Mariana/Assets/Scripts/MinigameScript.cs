using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class MinigameScript : MonoBehaviour
{
    public GameObject objectToDisable;
    public GameObject audioToDisable;
    public static bool disabled = false;
    public TextMeshProUGUI repairText;
    public List<Button> buttons; //list of buttons
    public List<Button> shuffledButtons; // shuffled version of this list 
    int counter = 0; // keeps track of how many buttons were pressed in sequence

    // Start is called before the first frame update
    void Start()
    {
        RestartTheGame(); //in the beginning just restart game session
        repairText.gameObject.SetActive(false);
    }

   public void RestartTheGame()
   {
       counter = 0;
       shuffledButtons=buttons.OrderBy(a => Random.Range(0,100)).ToList(); // shuffle the buttons with random numbers from 0 too 100
       for (int i=1;i<5;i++)
       {
           shuffledButtons[i - 1].GetComponentInChildren<Text>().text = i.ToString(); // set the text on buttons to correct number
           shuffledButtons[i - 1].interactable = true; // set all buttons presentable
           shuffledButtons[i - 1].image.color = new Color32(177,220,233,255); // our inital color
       }
   }

   public void pressButton(Button button) // function for buttons to call on click
   {
     if(int.Parse(button.GetComponentInChildren<Text>().text)-1==counter) // check if the number on the button -1 is equal to counter so we know that this button is pressed in correct order
     {
       counter++; // increases the counter
       button.interactable = false; // disables the button
       button.image.color = Color.green; // sets color to green when correct
       if(counter==4) // check if all buttons are pressed already
       {
           StartCoroutine(presentResult(true)); // present result for winning
           repairText.gameObject.SetActive(true);
           objectToDisable.SetActive(true);
           audioToDisable.SetActive(false);
       }
     }
     else
     {
         StartCoroutine(presentResult(false)); // in case the button was not pressed in correct sequence- present result for losing
     }
       
   }

   public IEnumerator presentResult(bool win) // coroutine for game result presentation
   {
       if(!win) // if player lost
       {
           foreach(var button in shuffledButtons)
           {
               button.image.color = Color.red; // set colors of all buttons to red
               button.interactable = false; // set all buttons to noninteractable state
           }
       } 
       else if (win)
        {
            objectToDisable.GetComponent<LightScript>().TurnOnAllLights();
        }
      yield return new WaitForSeconds(2f); // wait for 2 seconds so player can see the result
      RestartTheGame(); // restarts the game again
   }
   
}
