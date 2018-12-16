using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordDisplay : MonoBehaviour {

    public Text text;
    public Text shadowText;
    public float fallSpeed = 20.0f;
    public float fallSpeedQuickMode = 100f;
    //public float destroyTimer = 30.0f;      // Amount of time before the word disappeared
    public Color textColor = Color.white;
    public float time;

    public bool isScary;

    public Font normalFont;
    public Font scaryFont;

    public string completedLetters;
    public string incompleteLetters;

    private void Start()
    {
        if (GameModeController.isQuickMode)
            fallSpeed = fallSpeedQuickMode;
        if (isScary)
        {
            text.font = scaryFont;        
            text.fontSize -= 5;
            shadowText.font = scaryFont;
            shadowText.fontSize -= 5;
        }
        //Destroy(this.gameObject, destroyTimer);
    }

    // Set the text to display a word
    public void SetWordText(string word)
    {
        completedLetters = "";
        incompleteLetters = word;
        text.text = word;
        shadowText.text = word;
    }

    public void IncreaseFontSize(int increaseAmount)
    {
        text.fontSize += increaseAmount;
        shadowText.fontSize += increaseAmount;
    }

    public void RemoveLetter()
    {
        //text.text = text.text.Remove(0, 1);
        //shadowText.text = shadowText.text.Remove(0, 1);
        //text.color = textColor;

        // Don't remove a letter, just add the letter to the completeLetters string
        completedLetters += incompleteLetters[0];
        incompleteLetters = incompleteLetters.Substring(1);
        //Debug.Log("completed string" + completedLetters);
        //Debug.Log("incomplete string" + incompleteLetters);
    }

    // When player types wrong letter
    public void SetIncorrectColor()
    {
        text.color = Color.red;
    }

    public void RemoveWord()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        // Display green for typed in words
        text.text = string.Format("<color=#00FF00>{0}</color>", completedLetters) + incompleteLetters;
        transform.Translate(0f, -fallSpeed * Time.deltaTime, 0f);
    }
}
