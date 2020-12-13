using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    Image fadeImage;
    public GameObject black, white;

    // Start is called before the first frame update
    void Start()
    {
        fadeImage = this.GetComponent<Image>();
        //keep players from moving if the fade isn't over
        black.GetComponent<Life>().alive = false;
        white.GetComponent<Life>().alive = false;
    }

    // Update is called once per frame
    void Update()
    {
            Color tempColor = fadeImage.color;
            tempColor.a = Mathf.Lerp(tempColor.a, 0.0f, 0.01f);
            fadeImage.color = tempColor;
            //let players move again when the animation is close enough to done that they can see
            if (fadeImage.color.a.ToString("0.00") == "0.30")
            {
                black.GetComponent<Life>().alive = true;
                white.GetComponent<Life>().alive = true;
            }
    }
}
