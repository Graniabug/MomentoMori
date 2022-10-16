using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextArea : MonoBehaviour
{
    SaveManager sManager;
    public GameObject confirmationMenu;
    private bool galeHere = false;
    private bool kieranHere = false;
    // Start is called before the first frame update
    void Start()
    {
        confirmationMenu.SetActive(false);
        sManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<BlackPlayerController>())
            {
                kieranHere = true;
            }
            if(other.gameObject.GetComponent<WhitePlayerController>())
            {
                galeHere = true;
            }

            if (galeHere && kieranHere)  //only load to the next zone if both have gotten to the end
            {
                if (sManager.currentSave.isSingleplayer)
                {
                    SceneManager.LoadScene("Loading");
                }
                else
                {
                    confirmationMenu.SetActive(true);
                }
            }
        }
    }

    public void ConfirmGoBack()
    {
        confirmationMenu.SetActive(false);
    }

    public void ConfirmMoveOn()
    {
        SceneManager.LoadScene("Loading");
    }
}
