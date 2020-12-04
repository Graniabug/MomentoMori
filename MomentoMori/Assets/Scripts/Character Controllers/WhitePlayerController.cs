/**********************************************************************************************************
 * Script: WhitePlayerController
 * Author: Kayleigh Shaw
 * Date created: 11/14/2020 (goodbye Unus Annus - Momento Mori)
 * Date edited: 11/17/2020
 * Credits:
 *     2D movement code referenced from code by Hope Moore
 *     Concept for light detection inspired by message from user nickavv on Unity Forum
 * Attached to: "Annus" gameobject in SampleScene scene
 **********************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhitePlayerController : MonoBehaviour
{
    public GameObject character;

    /*
    public Sprite stand;
    public Sprite walk1;
    public Sprite walk2;
    public Sprite walk3;
    public Sprite jump;
    */
    public string characterName;
    public float speed = 3;

    public float delay;
    public float delayReset = 1;

    public int walkingImage = 1;

    public bool isWalking = false;

    public int direction = 0;       // 0 = right 1 = left

    public bool isJumping = false;
    public float jumpHeight = 500;

    public bool isInLightCollider = false;  //true of the player is currently within the collider for a light: does not mean they are currently in the light
    public bool isInGreyLight = false;
    public bool inTheLight = false;  //true if the player is currently in the light, false if not
    Transform currentLight;  //reference to the light that is currently a threat to the player
    public Text dialogue;  //Reference to the dialogue box above the player's head
    Vector3 lastInDark;  //Saves the last safe location before the player was in the light

    public bool isHost;
    public GameObject black;

    bool isAlive;
    GameObject couldBeKilled;

    // Start is called before the first frame update
    void Start()
    {
        delay = delayReset;

        //initialize the spawn position as the last place the character was in the dark
        lastInDark = transform.position;

        //initialize White dialogue to empty
        dialogue.text = "";

        isAlive = GetComponent<Life>().alive;

        couldBeKilled = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //get if White is alive
        isAlive = GetComponent<Life>().alive;

        //get input and move the player character
        if (isAlive)
        {
            //if(isSinglePlayer)
            //{
            if (isHost)
            {
                WhiteMoveSingleplayer();
            }
            else if (isInGreyLight)
            {
                WhiteFollowBlack();
            }
            //}
            //else
            //{
            //WhiteMoveMultiplayer();
            //}
        }

        //if the player character is in the trigger for a light, check if they are in direct line-of-sight with the light
        if (isInLightCollider == true)
        {
            CheckForLight();
        }

        //If the player is in the light, move them back to outside of the collider
        if (inTheLight)
        {
            //TODO: Check if lastInDark or other PC are closer, go to the closer one
            //If you go to the other PC, match their position + an amount in tranform the opposite direction from the light
            transform.position = Vector3.Lerp(transform.position, lastInDark, (speed * 2));
            isWalking = true;
        }

        /*if(Input.GetKeyDown(KeyCode.E))
        {
            couldBeKilled.GetComponent<Life>().alive = false;
            print(couldBeKilled + " is dead");
        }*/
    }

    public void Walk()
    {
        if (walkingImage == 1)
        {
            // Sets the sprite for character to walk1
            //character.GetComponent<SpriteRenderer>().sprite = walk1;
            walkingImage = 2;
            delay = delayReset;
        }

        else if (walkingImage == 2)
        {
            // Sets the sprite for character to walk2
            //character.GetComponent<SpriteRenderer>().sprite = walk2;
            walkingImage = 3;
            delay = delayReset;
        }

        else
        {
            // Sets the sprite for character to walk3
            //character.GetComponent<SpriteRenderer>().sprite = walk3;
            walkingImage = 1;
            delay = delayReset;
        }
    }

    public void FaceDirection()
    {
        // if the x scale is negative, it's facing left
        if (character.transform.localScale.x < 0)
        {
            if (direction == 0)     // if character should be facing right, update it
            {
                // set a new scale using the current values and multiplying x by -1 to turn it positive
                character.transform.localScale = new Vector3(character.transform.localScale.x * -1, character.transform.localScale.y, character.transform.localScale.z);
            }
        }

        // if the x scale is positive, it's facing right
        if (character.transform.localScale.x > 0)
        {
            if (direction == 1)     // if character should be facing left, update it
            {
                // set a new scale using the current values and multiplying x by -1 to turn it negative
                character.transform.localScale = new Vector3(character.transform.localScale.x * -1, character.transform.localScale.y, character.transform.localScale.z);
            }
        }
    }

    void WhiteMoveSingleplayer()
    {
        delay -= 1 * Time.deltaTime;

        if (delay <= 0)
        {
            if (isWalking == true && isJumping != true)
            {
                Walk();
            }

            delay = delayReset;
        }

        if (isJumping == true)
        {
            //character.GetComponent<SpriteRenderer>().sprite = jump;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            isWalking = true;
            direction = 0;
            FaceDirection();
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            isWalking = true;
            direction = 1;
            FaceDirection();
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            isWalking = true;
            direction = 0;
            FaceDirection();
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
            isWalking = true;
            direction = 0;
            FaceDirection();
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A)
            && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)
            && isJumping != true)
        {
            // Sets the sprite for character to stand
            //character.GetComponent<SpriteRenderer>().sprite = stand;
            isWalking = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight);
                isJumping = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            couldBeKilled.GetComponent<Life>().alive = false;
            print(couldBeKilled + " is dead");
        }
    }

    void WhiteMoveMultiplayer()
    {
        delay -= 1 * Time.deltaTime;

        if (delay <= 0)
        {
            if (isWalking == true && isJumping != true)
            {
                Walk();
            }

            delay = delayReset;
        }

        if (isJumping == true)
        {
            //character.GetComponent<SpriteRenderer>().sprite = jump;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            isWalking = true;
            direction = 0;
            FaceDirection();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            isWalking = true;
            direction = 1;
            FaceDirection();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            isWalking = true;
            direction = 0;
            FaceDirection();
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
            isWalking = true;
            direction = 0;
            FaceDirection();
        }

        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)
            && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)
            && isJumping != true)
        {
            // Sets the sprite for character to stand
            //character.GetComponent<SpriteRenderer>().sprite = stand;
            isWalking = false;
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            if (!isJumping)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight);
                isJumping = true;
            }
        }
    }

    void WhiteFollowBlack()
    {
        float distanceWanted = 3.0f;

        if (!inTheLight)
        {
            Vector3 diff = transform.position - black.transform.position;
            transform.position = black.transform.position + diff.normalized * distanceWanted;
        }
    }

    void CheckForLight()
    {
        //get layer mask for layer 9: Environment
        //int layerMask = 1 << 9;
        
        //cast a line between the player and the light and checks if any parts of the environment are between the player and the light
        if (Physics.Linecast(transform.position, currentLight.transform.position/*, layerMask*/))
        {
            //there's something between the player and the light
            Debug.DrawLine(transform.position, currentLight.transform.position, Color.yellow, 100f);
            inTheLight = false;
            print("not in light");
        }
        else
        {
            Debug.DrawLine(transform.position, currentLight.transform.position, Color.red, 100f);
            inTheLight = true;
            StartCoroutine(MessageActivation("I'd prefer to stay in the dark, thanks."));
            print("In the light");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJumping = false;
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        //if inside a light trigger
        if (collision.gameObject.layer == 8)
        {
            isInLightCollider = true;
            lastInDark = transform.position;
            currentLight = collision.gameObject.transform.parent;
            print("In the light collider");
        }

        //if inside an enemy of player trigger that isn't your own
        if(collision.gameObject.GetComponent<Life>() && collision.gameObject.GetComponent<Life>().alive)
        {
             couldBeKilled = collision.gameObject;
            //killUI.SetActive(true);
        }

        if (collision.gameObject.tag == "GreyLight")
        {
            isInGreyLight = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            isInLightCollider = false;
        }

        if (other.gameObject.GetComponent<Life>() && other.gameObject.GetComponent<Life>().alive)
        {
            couldBeKilled = this.gameObject;
            //killUI.SetActive(false);
        }

        if (other.gameObject.tag == "GreyLight")
        {
            isInGreyLight = false;
        }
    }

    public IEnumerator MessageActivation(string characterText)
    {

        dialogue.text = characterText;
        print("Object active");
        yield return new WaitForSeconds(10);
        print("waited 10 seconds");
        dialogue.text = "";
    }
}
