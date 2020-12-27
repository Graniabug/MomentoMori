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
    public bool[] foundRiddles = new bool[6];
    //string accountName = "DefaultName";

    public SaveFile(int level, bool singlePlayer, Vector3 location, string creationTime)
    {
        currentLevel = level;
        isSingleplayer = singlePlayer;
        currentLocation = location;
        time = creationTime;
        for (int i = 0; i < 6; i++)
        {
            foundRiddles[i] = false;
        }
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

        if (new FileInfo("file").Exists)
        {
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
                saveFile.Write(foundRiddles[0]);
                for (int i = 1; i < 6; i++)
                {
                    saveFile.Write("," + foundRiddles[i]);
                }
                saveFile.Write("\n");
            }
        }
        else
        {
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
                saveFile.Write("0");
                for (int i = 1; i < 6; i++)
                {
                    saveFile.Write(",0");
                }
                saveFile.Write("\n");
            }
        }
    }

    void Load()
    {
        using (StreamReader saveFile = new StreamReader(filePath))
        {
            currentLevel = int.Parse(saveFile.ReadLine());
            isSingleplayer = bool.Parse(saveFile.ReadLine());
            currentLocation.x = int.Parse(saveFile.ReadLine());
            currentLocation.y = int.Parse(saveFile.ReadLine());
            currentLocation.z = int.Parse(saveFile.ReadLine());
            time = saveFile.ReadLine();
            for (int i = 0; i < 6; i++)
            {
                if (saveFile.Read() == 0)
                {
                    foundRiddles[i] = false;
                }
                else
                {
                    foundRiddles[i] = true;
                }
                //get comma
                saveFile.Read();
            }
        }
    }

    void Print(Text saveText)
    {

    }
}
