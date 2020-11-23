using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameButtonAnchoring : MonoBehaviour
{
    public GameObject[] buttons = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {
        Vector3 goalPosition;
        for (int i = 0; i < buttons.Length; i++)
        {
            goalPosition = new Vector3(Screen.width / 8, buttons[i].transform.position.y, buttons[i].transform.position.z);
            buttons[i].transform.position = Vector3.Lerp(buttons[i].transform.position, goalPosition, 1f);
        }


    }
}
