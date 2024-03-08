using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public bool followPlayer = true;

    public GameObject Annus;
    public GameObject Unus;

    public bool followPlayerX;
    public bool followPlayerXY;

    Vector3 newLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            print(transform.gameObject);
            if (Annus.GetComponent<WhitePlayerController>().isHost)
            {
                newLocation = transform.position;
                newLocation.x = Annus.transform.position.x;
                transform.position = newLocation;
            }
            if (Unus.GetComponent<BlackPlayerController>().isHost)
            {
                newLocation = transform.position;
                newLocation.x = Unus.transform.position.x;
                transform.position = newLocation;
            }
        }
        else
        {
            //if a player character leaves the screen, shift it over by the width of the screen
        }
    }
}
