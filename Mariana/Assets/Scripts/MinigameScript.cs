using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class MinigameScript : MonoBehaviour
{
    public List<Button> buttons;
    public List<Button> shuffledButtons;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        RestartTheGame();
    }

   public void RestartTheGame()
   {
       counter = 0;
       shuffledButtons=buttons.OrderBy(a => Random.Range(0,100)).ToList();
       for (int i=1;i<11;i++)
       {
           shuffledButtons[i - 1].GetComponentInChildren<Text>().text = i.ToString();
           shuffledButtons[i - 1].interactable = true;
           shuffledButtons[i - 1].image.color = new Color32(177,220,233,255);    
       }
   }

   public void pressButton(Button button)
   {
     if(int.Parse(button.GetComponentInChildren<Text>().text)-1==counter)
     {
       counter++;
       button.interactable = false;
       button.image.color = Color.green;
       if(counter==10)
       {
           StartCoroutine(presentResult(true));
       }
     }
     else
     {
         StartCoroutine(presentResult(false));
     }
       
   }

   public IEnumerator presentResult(bool win)
   {
       if(!win)
       {
           foreach(var button in shuffledButtons)
           {
               button.image.color = Color.red;
               button.interactable = false;
           }
       } 
      yield return new WaitForSeconds(2f);
      RestartTheGame();
   }
   
}
