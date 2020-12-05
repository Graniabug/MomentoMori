using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveFile
{
    string filePath;

    public int currentLevel;
    public bool isSingleplayer = true;
    public Vector3 currentLocation;
    public string time = "00/00/0000 00:00";
    //string accountName = "DefaultName";

    public SaveFile(int level, bool singlePlayer, Vector3 location, string creationTime)
    {
        currentLevel = level;
        isSingleplayer = singlePlayer;
        currentLocation = location;
        time = creationTime;
    }

    public SaveFile(string path)
    {
        //string store;
        using (StreamReader file = new StreamReader(path))
        {
            //store = file.ReadLine().TrimEnd('\n');
            currentLevel = int.Parse(file.ReadLine().TrimEnd('\n'));
            //store = file.ReadLine().TrimEnd('\n');
            isSingleplayer = bool.Parse(file.ReadLine().TrimEnd('\n'));
            //store = file.ReadLine().TrimEnd('\n');
            currentLocation.x = int.Parse(file.ReadLine().TrimEnd('\n'));
           // store = file.ReadLine().TrimEnd('\n');
            currentLocation.y = int.Parse(file.ReadLine().TrimEnd('\n'));
          //  store = file.ReadLine().TrimEnd('\n');
            currentLocation.z = int.Parse(file.ReadLine().TrimEnd('\n'));
            time = file.ReadLine().TrimEnd('\n');
        }
    }

    // Start is called before the first frame update
    /*void Start()
    {
        filePath = Application.persistentDataPath + time + "/";
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }*/

    public void Save()
    {
        string fileName = time;
        fileName = fileName.Replace('/', '_');
        fileName = fileName.Replace(' ', '_');
        fileName = fileName.Replace(':', '_');
        Debug.Log(fileName);
        filePath = Application.persistentDataPath + "/Data/" + fileName + ".txt";

        File.WriteAllText(filePath, string.Empty);
        currentLevel = SceneManager.GetActiveScene().buildIndex;

        using (StreamWriter saveFile = new StreamWriter(filePath))
        {
            saveFile.WriteLine(currentLevel);
            //saveFile.Write("\n");
            saveFile.WriteLine(isSingleplayer);
            //saveFile.Write("\n");
            saveFile.WriteLine(currentLocation.x);
            //saveFile.Write("\n");
            saveFile.WriteLine(currentLocation.y);
            //saveFile.Write("\n");
            saveFile.WriteLine(currentLocation.z);
            //saveFile.Write("\n");
            saveFile.WriteLine(time);
            //saveFile.Write("\n");
        }
    }

    void Load()
    {
        using (StreamReader saveFile = new StreamReader(filePath))
        {
            currentLevel = int.Parse(saveFile.ReadLine());
            isSingleplayer = bool.Parse(saveFile.ReadLine());
            //currentLocation = Vector3.Parse
            time = saveFile.ReadLine();
        }
    }

    void Print(Text saveText)
    {

    }
}
