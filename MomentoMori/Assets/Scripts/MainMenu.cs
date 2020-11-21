using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public GameObject defaultMenuArt;
    public GameObject specialMenuArt;

    bool optionsPressed = false;
    bool continuePressed = false;
    bool newGamePressed = false;

    //public GameObject[] buttons = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        //show the easter egg menu art 1/50th of the time
        //otherwise, show the default art
        int choose = Random.Range(1, 50);
        if (choose == 50)
        {
            specialMenuArt.SetActive(true);
            defaultMenuArt.SetActive(false);
        }
        else
        {
            defaultMenuArt.SetActive(true);
            specialMenuArt.SetActive(false);
        }

        //deactiveate the "Continue" button if there are no ongoing games

    }

    private void Update()
    {
        //shift BG to the right if continue is pressed


        //shift BG to the left if new game is pressed


        //pull a grey box up from below if options is pressed


    }

    public void Options()
    {
        optionsPressed = true;
    }

    public void Continue()
    {
        continuePressed = true;
    }

    public void NewGame()
    {
        newGamePressed = true;
    }

    public void Quit()
    {
        //end the game
        Application.Quit();
    }
}
