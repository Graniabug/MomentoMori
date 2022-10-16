/**********************************************************************************************************
 * Script: OffScreenMonitor
 * Author: Kayleigh Shaw
 * Date created: 1/2/2021
 * Date edited: 1/2/2021
 * Attached to: NewSprite under "Annus" and "Unus"
 **********************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenMonitor : MonoBehaviour
{
    public GameObject mainCamera;  //reference to the main camera in the scene, used to get location
    public GameObject player;  //reference to the player, aka the parent object of this sprite object
    public float xPosition;  //temporary value to update the amount the camera should move by based on the x position of the player

    // Start is called before the first frame update
    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
    }

    // Called when the renderer leaves the screen
    public void OnBecameInvisible()
    {
        //If Kieran left the screen, let him know that he's left it and can't be seen
        if (player.GetComponent<BlackPlayerController>())
        {
            player.GetComponent<BlackPlayerController>().isInvisible = true;
        }
        //If Gale left the screen, let him know that he's left it and can't be seen
        if (player.GetComponent<WhitePlayerController>())
        {
            player.GetComponent<WhitePlayerController>().isInvisible = true;
        }

        //if the camera has been set, set the amount the screen shifts when both characters leave it
        if (mainCamera != null)
        {
            xPosition = player.transform.position.x;
            xPosition -= mainCamera.transform.position.x;
            xPosition *= 2;
            mainCamera.GetComponent<CameraMove>().shiftAmount = xPosition;
        }
    }

    // Called when the renderer comes back into view on the screen
    public void OnBecameVisible()
    {
        //If Kieran returned to being on-screen, let him know (used to move the camera)
        if (player.GetComponent<BlackPlayerController>())
        {
            player.GetComponent<BlackPlayerController>().isInvisible = false;
        }
        //If Gale returned to being on-screen, let him know (used to move the camera)
        if (player.GetComponent<WhitePlayerController>())
        {
            player.GetComponent<WhitePlayerController>().isInvisible = false;
        }
    }
}
