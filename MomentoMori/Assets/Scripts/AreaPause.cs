using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaPause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject journalMenu;
    public GameObject youSureMenu;
    public GameObject optionsMenu;
    //public GameObject[] journalPages = new GameObject[6];
    public GameObject journalPages;
    public GameObject journalLast;
    public GameObject journalNext;
    bool pauseActive = false;
    bool journalActive = false;
    bool journalGoToNext = false;
    bool journalGoToLast = false;
    Vector3 journalPagesStarting;
    Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        optionsMenu.SetActive(false);
        youSureMenu.SetActive(false);
        pauseMenu.SetActive(false);
        journalLast.SetActive(false);
        journalNext.SetActive(true);
        journalMenu.SetActive(false);

        journalPagesStarting = journalPages.transform.position;
        journalPagesStarting.x -= 200;
    }

    // Update is called once per frame
    void Update()
    {
        //if the menu is open and escape is pressed again, it'll close
        if (Input.GetKeyDown(KeyCode.Escape) && pauseActive)
        {
            Continue();
        }

        //open the menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            pauseActive = true;
        }

        if (Input.GetKeyDown(KeyCode.J) && journalActive)
        {
            CloseJournal();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            OpenJournal();
        }

        if (journalGoToNext)
        {
            journalPages.transform.position = Vector3.Lerp(journalPages.transform.position, destination, 0.2f);
            if (Vector3.Distance(journalPages.transform.position, destination).ToString("0.00") == "0.00")
            {
                journalGoToNext = false;
            }
            //true if the player is looking at the last page
            /*if (journalPages.transform.position == journalPagesEnding)
            {
                journalNext.SetActive(false);
            }
            else
            {
                journalNext.SetActive(true);
            }*/
        }

        if (journalGoToLast)
        {
            journalPages.transform.position = Vector3.Lerp(journalPages.transform.position, destination, 0.2f);
            if (Vector3.Distance(journalPages.transform.position, destination).ToString("0.00") == "0.00")
            {
                journalGoToLast = false;
            }
            //true if the player is looking at the first page
            if (journalPages.transform.position.x >= journalPagesStarting.x)
            {
                journalLast.SetActive(false);
            }
            else
            {
                journalLast.SetActive(true);
            }
        }
    }

    public void Continue()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        pauseActive = false;
    }

    public void QuitToMenu()
    {
        youSureMenu.SetActive(true);
    }

    public void sureYes()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void sureNo()
    {
        youSureMenu.SetActive(false);
    }

    public void OpenJournal()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 0;
        journalActive = true;
        journalMenu.SetActive(true);
        journalLast.SetActive(false);
        journalNext.SetActive(true);
    }

    public void CloseJournal()
    {
        journalMenu.SetActive(false);
        Time.timeScale = 1;
        journalActive = false;
        pauseMenu.SetActive(true);
    }

    public void JournalNext()
    {
        journalGoToNext = true;
        journalLast.SetActive(true);
        destination = journalPages.transform.position;
        destination.x -= 1205;
    }

    public void JournalLast()
    {
        journalGoToLast = true;
        journalNext.SetActive(true);
        destination = journalPages.transform.position;
        destination.x += 1205;
    }

    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void OptionsBack()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
