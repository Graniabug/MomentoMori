using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueSelectableFile : MonoBehaviour
{
    public SaveManager manager;
    public SaveFile thisSave;

    void Start()
    {
        manager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
    }

    public void SaveSelected()
    {
        manager.currentSave = thisSave;
    }
}
