using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public GameObject continueButton;

    public GameObject defaultMenuArt;
    public GameObject specialMenuArt;
    public GameObject backGround;
    public GameObject mainMenu;
    public GameObject newGameMenu;
    public GameObject continueMenu;
    public GameObject singlePlayerMenu;
    public GameObject multiplayerMenu;
    public GameObject optionsMenu;
    public GameObject quitMenu;
    public Image fadeImage;

    public SaveManager saveManager;

    Vector3 leftShift;
    Vector3 rightShift;
    Vector3 BGOrigin;

    float lerpTime = 0.2f;

    bool optionsPressed = false;
    bool continuePressed = false;
    bool newGamePressed = false;
    bool resetBGPosition = false;
    bool fadeOut = false;

    // Start is called before the first frame update
    void Start()
    {
        BGOrigin = backGround.transform.position;

        backGround.SetActive(true);
        mainMenu.SetActive(true);
        newGameMenu.SetActive(false);
        continueMenu.SetActive(false);
        singlePlayerMenu.SetActive(false);
        multiplayerMenu.SetActive(false);
        optionsMenu.SetActive(false);
        quitMenu.SetActive(false);
        fadeImage.gameObject.SetActive(false);

        string filepath = Application.persistentDataPath + "/Data/";
        DirectoryInfo d = new DirectoryInfo(filepath);
        if (d.GetFiles("*.txt").Length < 1)
        {
            continueButton.GetComponent<Button>().interactable = false;
        }

        //show the easter egg menu art 1/50th of the time
        //otherwise, show the default art
        int choose = UnityEngine.Random.Range(1, 50);
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


        leftShift = backGround.transform.position;
        leftShift.x = Screen.width / 4;
        rightShift = backGround.transform.position;
        rightShift.x = leftShift.x*3;
    }

    private void Update()
    {
        //shift BG to the right if continue is pressed
        if (continuePressed)
        {
            backGround.transform.position = Vector3.Lerp(backGround.transform.position, rightShift, lerpTime);
            if (Vector3.Distance(backGround.transform.position, rightShift).ToString("0.00") == "0.00")
            {
                continuePressed = false;
            }
        }

        //shift BG to the left if new game is pressed
        if (newGamePressed)
        {
            backGround.transform.position = Vector3.Lerp(backGround.transform.position, leftShift, lerpTime);
            if (Vector3.Distance(backGround.transform.position, leftShift).ToString("0.00") == "0.00")
            {
                newGamePressed = false;
            }
        }

        //pull a grey box up from below if options is pressed

        if (resetBGPosition)
        {
            backGround.transform.position = Vector3.Lerp(backGround.transform.position, BGOrigin, lerpTime);
            if (Vector3.Distance(backGround.transform.position, BGOrigin).ToString("0.00") == "0.00")
            {
                resetBGPosition = false;
            }
        }

        if (fadeOut)
        {
            Color tempColor = fadeImage.color;
            tempColor.a = Mathf.Lerp(tempColor.a, 1.0f, 0.01f);
            fadeImage.color = tempColor;
            if (fadeImage.color.a.ToString("0.00") == "1.00")
            {
                SceneManager.LoadScene("Area1");
            }
        }
    }

    public void Options()
    {
        optionsPressed = true;
    }

    public void Continue()
    {
        continuePressed = true;
        mainMenu.SetActive(false);
        continueMenu.SetActive(true);
    }

    public void NewGame()
    {
        newGamePressed = true;
        mainMenu.SetActive(false);
        newGameMenu.SetActive(true);
    }
    public void OpenQuitMenu()
    {
        mainMenu.SetActive(false);
        quitMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackFromNewGame()
    {
        resetBGPosition = true;
        singlePlayerMenu.SetActive(false);
        multiplayerMenu.SetActive(false);
        newGameMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void BackFromContinue()
    {
        resetBGPosition = true;
        continueMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void BackFromOptions()
    {

    }

    public void BackFromQuit()
    {
        quitMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void SelectSinglePlayer()
    {
        multiplayerMenu.SetActive(false);
        singlePlayerMenu.SetActive(true);
    }

    public void SelectMultiplayer()
    {
        singlePlayerMenu.SetActive(false);
        multiplayerMenu.SetActive(true);
    }

    public void SinglePlay()
    {
        fadeImage.gameObject.SetActive(true);
        fadeOut = true;

        DateTime time = DateTime.Now;
        saveManager.currentSave = new SaveFile(0, true, new Vector3(-19, 2, 0), time.ToString());
        print("created new save");
    }

    public void MultiPlay()
    {
        fadeImage.gameObject.SetActive(true);
        fadeOut = true;

        DateTime time = DateTime.Now;
        saveManager.currentSave = new SaveFile(0, false, new Vector3(-19, 2, 0), time.ToString());
        print("created new save");
    }

    public void ContinuePlay()
    {
        SceneManager.LoadScene(saveManager.currentSave.currentLevel);
    }
}
