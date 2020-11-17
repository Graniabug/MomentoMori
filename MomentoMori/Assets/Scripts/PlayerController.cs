using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    public bool isInLight = false;
    Transform currentLight;

    // Start is called before the first frame update
    void Start()
    {
        delay = delayReset;
    }

    // Update is called once per frame
    void Update()
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
            character.transform.Translate(Vector3.right * speed * Time.deltaTime);
            isWalking = true;
            direction = 0;
            FaceDirection();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            character.transform.Translate(Vector3.left * speed * Time.deltaTime);
            isWalking = true;
            direction = 1;
            FaceDirection();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            character.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            isWalking = true;
            direction = 0;
            FaceDirection();
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            character.transform.Translate(Vector3.back * speed * Time.deltaTime);
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isJumping != true)
            {
                character.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight);
                isJumping = true;
            }
        }

        if (isInLight == true)
        {
            CheckForLight();
        }
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

    public void OnTriggerEnter(Collider collision)
    {
        /*RaycastHit hit;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;*/

        if (collision.gameObject.name == "Floor")
        {
            isJumping = false;
        }

        if (collision.gameObject.layer == 8)
        {
            isInLight = true;
            currentLight = collision.gameObject.transform.parent;
            print("In the light collider");
            /*if (Physics.Raycast(transform.position, collision.gameObject.transform.position, out hit, layerMask))
            {
                print("not in light");
                isInLight = false;
            }
            else
            {
                //print(hit.collider.gameObject.name);
                print("In the light");
                isInLight = true;
            }*/
        }
    }

    /*public void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.layer == 8)
        {
            RaycastHit hit;
            int layerMask = 1 << 8;
            layerMask = ~layerMask;

            print("In the light collider, no raycast yet");
            if (Physics.Raycast(transform.position, collision.gameObject.transform.position, out hit, layerMask))
            {
                print("not in light");
                isInLight = false;
            }
            else
            {
                //print(hit.collider.gameObject.name);
                print("In the light");
                isInLight = true;
            }
        }
    }*/

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            isInLight = false;
            //print("not in light");
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
            print("not in light");
        }
        else
        {
            Debug.DrawLine(transform.position, currentLight.transform.position, Color.red, 100f);
            print("In the light");
        }
    }
}
