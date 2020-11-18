﻿/**********************************************************************************************************
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
    public GameObject[] characters = new GameObject[1];

    private float lastSwitched;

    // Start is called before the first frame update
    void Start()
    {
        lastSwitched = Time.time;

        characters[0].GetComponent<WhitePlayerController>().isHost = true;
        //characters[2].GetComponent<BlackPlayerController>().isHost = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && Time.time > lastSwitched)
        {
            if (characters[0].GetComponent<WhitePlayerController>().isHost)
            {
                characters[0].GetComponent<WhitePlayerController>().isHost = false;
                //characters[1].GetComponent<BlackPlayerController>().isHost = true;
            }
            else
            {
                //characters[1].GetComponent<BlackPlayerController>().isHost = false;
                characters[0].GetComponent<WhitePlayerController>().isHost = true;
            }

            lastSwitched = Time.time;
            lastSwitched += 1;
        }
    }
}