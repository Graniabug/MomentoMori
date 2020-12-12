using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ContinueScreen : MonoBehaviour
{
    public GameObject[] buttons = new GameObject[2];
    public GameObject[] files = new GameObject[100];
    public GameObject saveFileTemplate;
    public GameObject scrollMenu;
    public SaveFile temp;
    private Transform location;
    private Vector3 vecLocation;
    // Start is called before the first frame update
    void Start()
    {
        location = scrollMenu.transform;
        location.position = vecLocation;
        int showIndex = 0;

        //Anchor buttons on right
        Vector3 goalPosition;
        for (int i = 0; i < buttons.Length; i++)
        {
            goalPosition = new Vector3((Screen.width / 8) * 7, buttons[i].transform.position.y, buttons[i].transform.position.z);
            buttons[i].transform.position = Vector3.Lerp(buttons[i].transform.position, goalPosition, 1f);
        }

        //populate existing save files
        string filepath = Application.persistentDataPath + "/Data/";
        DirectoryInfo d = new DirectoryInfo(filepath);
        for (int i = 0; i < d.GetFiles("*.txt").Length; i++)
        {
            files[i] = Instantiate(saveFileTemplate, location);
            temp = new SaveFile(filepath + d.GetFiles("*.txt")[i].Name);
            files[i].GetComponent<ContinueSelectableFile>().thisSave = temp;
            showIndex = i + 1;
            files[i].transform.GetChild(0).GetComponent<Text>().text = showIndex.ToString();
            if (temp.isSingleplayer) {
                files[i].transform.GetChild(1).GetComponent<Text>().text = "Singleplayer";
            }
            else
            {
                files[i].transform.GetChild(1).GetComponent<Text>().text = "Multiplayer";
            }
            files[i].transform.GetChild(2).GetComponent<Text>().text = temp.time;
            if (temp.currentLevel == 1)
            {
                files[i].transform.GetChild(3).GetComponent<Text>().text = "Area 1: Toward The Light";
            }
        }
    }
}
