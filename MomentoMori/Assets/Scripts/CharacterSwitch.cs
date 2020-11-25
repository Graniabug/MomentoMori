/**********************************************************************************************************
 * Script: CharacterSwitch
 * Author: Kayleigh Shaw
 * Date created: 11/17/2020
 * Date edited: 11/17/2020
 * Attached to: "EventSystem" gameobject in SampleScene scene
 **********************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    public GameObject[] characters = new GameObject[2];

    private float lastSwitched;
    private bool justSwitched = false;

    // Start is called before the first frame update
    void Start()
    {
        lastSwitched = Time.time;

        characters[0].GetComponent<WhitePlayerController>().isHost = true;
        characters[1].GetComponent<BlackPlayerController>().isHost = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && Time.time > lastSwitched)
        {
            print("switching");
            if (characters[0].GetComponent<WhitePlayerController>().isHost)
            {
                print("black is about to host");
                characters[1].GetComponent<BlackPlayerController>().isHost = true;
                characters[0].GetComponent<WhitePlayerController>().isHost = false;
                justSwitched = true;
                print("black should be host");
            }

            if(characters[1].GetComponent<BlackPlayerController>().isHost && !justSwitched)
            {
                print("white should be host");
                characters[1].GetComponent<BlackPlayerController>().isHost = false;
                characters[0].GetComponent<WhitePlayerController>().isHost = true;
            }

            lastSwitched = Time.time;
            lastSwitched += 1;
            justSwitched = false;
        }

        //if white dies, make black the host
        if (characters[0].GetComponent<WhitePlayerController>().isHost && !characters[0].GetComponent<Life>().alive)
        {
            characters[1].GetComponent<BlackPlayerController>().isHost = true;
            characters[0].GetComponent<WhitePlayerController>().isHost = false;
        }

        //if black dies, make white the host
        if (characters[1].GetComponent<BlackPlayerController>().isHost && !characters[1].GetComponent<Life>().alive)
        {
            characters[0].GetComponent<WhitePlayerController>().isHost = true;
            characters[1].GetComponent<BlackPlayerController>().isHost = false;
        }
    }
}
