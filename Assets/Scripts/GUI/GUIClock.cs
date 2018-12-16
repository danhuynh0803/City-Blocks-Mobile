using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GUIClock : MonoBehaviour
{

    public GameObject bar1;
    public GameObject bar2;
    public GameObject bar3;
    private bool isStrongSignal;
    private bool isBar2Enable;
    public bool isEndScene;
    private float time;
    private void Start()
    {
        isStrongSignal = true;
        isBar2Enable = true;
    }

    void Update()
    {
        
        time += Time.deltaTime;
        if(time > 0.5f)
        {
            if (isEndScene)
            {
                if (UnityEngine.Random.Range(0f, 1f) < 0.2f)
                {
                    GetComponent<Text>().text = "ERROR";
                    GetComponent<Text>().color = new Color(125f, 0f, 0f);
                }
                else
                {
                    GetComponent<Text>().color = new Color(255f, 255f, 255f);
                    char[] brokeText = DateTime.Now.ToString().ToCharArray();
                    int length = brokeText.Length;
                    string final = "";
                    for (int i = 0; i < length; i++)
                    {
                        if (UnityEngine.Random.Range(0f, 1f) < 0.3f)
                        {
                            final += " ";
                        }
                        else
                        {
                            final += brokeText[i];
                        }

                    }
                    GetComponent<Text>().text = final;
                }
            }
            else
                GetComponent<Text>().text = DateTime.Now.ToString();
            if (isStrongSignal)
            {
                if (UnityEngine.Random.Range(0f, 1f) < 0.3f)
                {
                    bar3.GetComponent<Image>().enabled = false;
                    isStrongSignal = false;
                }
            }
            else
            {
                if (UnityEngine.Random.Range(0f, 1f) < 0.4f)
                {
                    bar2.GetComponent<Image>().enabled = false;
                    isBar2Enable = false;
                }
                else
                {
                    if (UnityEngine.Random.Range(0f, 1f) < 0.5f)
                    {
                        bar2.GetComponent<Image>().enabled = true;
                        isBar2Enable = true;
                    }
                }
                if(isBar2Enable)
                {
                    if (UnityEngine.Random.Range(0f, 1f) < 0.3f)
                    {
                        bar3.GetComponent<Image>().enabled = true;
                        isStrongSignal = true;
                    }
                }
            }
            time = 0;
        }
    }
}
