using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word
{
    public string word;
    public int lineNumFunction; // Hacky way of finding which word goes with which powerup function (Replace w/ fcn pointers)

    private int currentTypedIndex;  // Used to check if the next letter matches what we type 
    
    public WordDisplay display;
    public Color color;
    public int points = 0;

    public Word(string word, WordDisplay display, Color color)
    {
        this.word = word;
        currentTypedIndex = 0;

        // For displaying the word in game
        this.display = display;
        display.SetWordText(word);
        display.text.color = color;
    }

    public Word (string word, WordDisplay display)
    {
        this.word = word;
        currentTypedIndex = 0;

        // For displaying the word in game
        this.display = display;
        display.SetWordText(word);
    }

    public char GetNextLetter()
    {
        return word[currentTypedIndex];
    }

    public void IncrementTypeIndex()
    {
        currentTypedIndex++;
        // Remove the letter that is currently on screen
        display.RemoveLetter();
    }

    public bool WordTyped()
    {
        bool wordTyped = (currentTypedIndex >= word.Length - 1);
        if (wordTyped)
        {
            // Remove the word on screen
            display.RemoveWord();
        }

        return wordTyped;
    }
}
