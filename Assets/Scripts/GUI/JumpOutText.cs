using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpOutText : MonoBehaviour {

    public float startJumpOutTime;
    public float startTransistOnTime;
    public float startFadeOutTime;
    public int startFontSize;

    int fontSize;
	float jumpOutTime;
    float transistonTime;
    float fadeOutTime;
    public Text white;
    public Text black;
    public bool isNewHighScore;

    void Start()
    {
        //fontSize = startFontSize;
        if(!isNewHighScore)
            DestroyObject(gameObject, 5f);
    }

    void Update () {
        if(transistonTime < 1)
        {
            jumpOutTime = Mathf.Clamp(jumpOutTime + Time.deltaTime, 0, 1);
            transistonTime = Mathf.Clamp(transistonTime + 3 * Time.deltaTime, 0, 1);
            fontSize = (int)Mathf.Round(Mathf.Clamp(300 * 3 * jumpOutTime, 0, startFontSize));
            white.fontSize = fontSize;
            black.fontSize = fontSize;
        }
        else
        {
            fadeOutTime = Mathf.Clamp(fadeOutTime + Time.deltaTime, 0, 1);
            float alpha = Mathf.Clamp(1.5f * fadeOutTime, 0, 1);
            white.color = new Color(white.color.r, white.color.g, white.color.b, 1-alpha);
            black.color = new Color(black.color.r, black.color.g, black.color.b, 1-alpha);
        }

        
    }
}
