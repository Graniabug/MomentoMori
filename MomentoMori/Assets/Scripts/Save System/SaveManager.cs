using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveManager : MonoBehaviour
{
    public SaveFile currentSave;
    private int inScene = 0;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //if (SceneManager.GetActiveScene().buildIndex != inScene)
        if (SceneManager.GetActiveScene().name == "Loading")
        {
            currentSave.Save();
            SceneManager.LoadScene(inScene + 1);
            inScene++;
        }
    }
}
