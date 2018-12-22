using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notifications : MonoBehaviour
{
    Animator anim;
    private float timer;
    public Text textText;
    public GameObject textMsg;
    private bool hasStarted;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        timer = 6.0f;
        textMsg.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            float rate = Random.Range(0.0f, 100.0f);
            if (rate > 79.0f)
            {
                anim.Play("Notifications");
                textMsg.SetActive(true);
                hasStarted = true;
                float rate2 = Random.Range(0.0f, 8.0f);
                if (rate > 7.0f)
                {
                    textText.text = "3/1/12/12/9/15/16/5 1/18/9/19/5"; //Calliope arise
                }
                else if (rate > 6.0f)
                {
                    textText.text = "Working late again?";
                }
                else if (rate > 5.0f)
                {
                    textText.text = "When are you coming home?";
                }
                 else if (rate > 4.0f)
                {
                    textText.text = "Yes, there is a reason.";
                }
                else if (rate > 3.0f)
                {
                    textText.text = "No, we won't tell you.";
                }
                else if (rate > 2.0f)
                {
                    textText.text = "Journey is more than destination."; //will probably get clipped intentionally
                }
                else
                {
                    textText.text = "Thank you for playing.";
                }
            }
            timer = 25.0f;
        }
        if(timer < 18 && hasStarted)
        {
            anim.Play("Off");
            textMsg.SetActive(false);
        }
    }
}