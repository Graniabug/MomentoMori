using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public bool followPlayerX = false;
    public bool followPlayerXY = false;
    public bool followPlayerXYZ = false;
    public bool followOffScreen = false;

    public float zFollowDistance = 10;
    float yFollowDistance = 5;

    public GameObject Annus;
    public GameObject Unus;

    Vector3 newLocation;

    public float shiftAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        yFollowDistance = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayerXYZ)
        {
            if (Annus.GetComponent<WhitePlayerController>().isHost)
            {
                newLocation = transform.position;
                newLocation.x = Annus.transform.position.x;
                newLocation.y = Annus.transform.position.y;
                newLocation.z = Annus.transform.position.z + zFollowDistance;
                transform.position = newLocation;
            }
            if (Unus.GetComponent<BlackPlayerController>().isHost)
            {
                newLocation = transform.position;
                newLocation.x = Unus.transform.position.x;
                newLocation.y = Unus.transform.position.y;
                newLocation.z = Unus.transform.position.z + zFollowDistance;
                transform.position = newLocation;
            }
        }

        if (followPlayerXY)
        {
            if (Annus.GetComponent<WhitePlayerController>().isHost)
            {
                newLocation = transform.position; 
                newLocation.x = Annus.transform.position.x;
                newLocation.y = Annus.transform.position.y + yFollowDistance;
                transform.position = newLocation;
            }
            if (Unus.GetComponent<BlackPlayerController>().isHost)
            {
                newLocation = transform.position;
                newLocation.x = Unus.transform.position.x;
                newLocation.y = Unus.transform.position.y + yFollowDistance;
                transform.position = newLocation;
            }
        }

        if (followPlayerX)
        {
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

        if (followOffScreen)
        {
            if (Annus.GetComponent<WhitePlayerController>().isInvisible && Unus.GetComponent<BlackPlayerController>().isInvisible)
            {
                print("camera shift time");
                newLocation = transform.position;
                newLocation.x = transform.position.x + shiftAmount;
                transform.position = newLocation;
            }
        }
    }
}
