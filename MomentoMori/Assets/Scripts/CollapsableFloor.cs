using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsableFloor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}
