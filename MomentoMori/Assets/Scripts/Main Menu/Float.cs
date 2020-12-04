using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Float : MonoBehaviour
{
    int floatAmount;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //MakeFloat();

        //ScaleOnSelected();
    }

    void MakeFloat()
    {
        Vector3 floatUp = this.transform.position;
        floatUp.x = floatUp.x + floatAmount;
        floatUp.y = floatUp.y + floatAmount;

        Vector3 floatDown = this.transform.position;
        floatDown.x = floatDown.x - floatAmount;
        floatDown.y = floatUp.y - floatAmount;

        if (this.transform.position.x < floatUp.x && this.transform.position.y < floatUp.y)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, floatUp, 0.1f);
        }
        else if (this.transform.position.x > floatDown.x && this.transform.position.y > floatDown.y)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, floatDown, 0.1f);
        }
    }

    void ScaleOnSelected()
    {
        //Vector3 scaleModifier = new Vector3(1.25f, 1.25f, 1.25f);
        print(EventSystem.current);
        if (EventSystem.current.currentSelectedGameObject == this)
        {
            print("is selected");
            Vector3 selectedScale = this.transform.localScale;
            selectedScale.x *= 1.25f;
            selectedScale.y *= 1.25f;
            selectedScale.z *= 1.25f;
            this.transform.localScale = selectedScale;
        }
    }
}
