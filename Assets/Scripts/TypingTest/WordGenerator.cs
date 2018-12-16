using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour {

    public TextAsset wordFile;          // A text file that will be read in and stored in the words list

    public List<string> stringList;

    private WordSpawner wordSpawner;

    private static int scaryWordIndex = 20;
    public static int scaryWordScoreShift = 1300000;
    private int scaryWordScore = scaryWordScoreShift;
    
    private void Awake()
    {
        ParseWordFile();
        wordSpawner = FindObjectOfType<WordSpawner>();
    }

    public Word GetRandomWord()
    {
        int index = Random.Range(0, stringList.Count);
        Word newWord;

        // Control color based on what index the word was chosen at
        // All words from lines 1-19 are green (powerup related awards)

        // Check if the Score has reached a certain threshold, then spawn a scary word
        if (GameController.level == (int)GameController.Level.story)
        {
            if (ScoreController.isHighScore())
            {
                // Use only the final word
                ScoreController.UpdateHighScore();
                newWord = new Word("Remember?", wordSpawner.SpawnWord());
                newWord.display.IncreaseFontSize((int)(Random.Range(-0.2f, 1.0f) * 30));
                newWord.display.isScary = true;
                newWord.display.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(-45f, 45f)));
                return newWord;
            }
            else if (ScoreController.getScore() >= scaryWordScore)
            {
                newWord = new Word(stringList[scaryWordIndex++], wordSpawner.SpawnWord());
                newWord.display.isScary = true;
                scaryWordScore += scaryWordScoreShift;
                return newWord;
            }
        }

        newWord = new Word(stringList[index], wordSpawner.SpawnWord());

        if (GameController.level == (int)GameController.Level.story)
        {
            if (index >= 20 && index < 28)
            {
                newWord.display.isScary = true;
            }
            newWord.lineNumFunction = index;
        }

        // Set the line number of the word within the dictionary file
        // for use in determining what words have powerups (to replace with function pointers) 
        newWord.lineNumFunction = index;

        return newWord;
    }

    // Parse and add the words from the word text file into the word list
    private void ParseWordFile()
    {
        string[] wordLines = wordFile.text.Split('\n');
        foreach (string newWord in wordLines)
        {
            // Add only if the word has a length
            if (newWord.Length > 0)
            {
                stringList.Add(newWord);
            }
        }
    }
}
