using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class ScoreController : MonoBehaviour
{
    private static int score;                   // Player 1's score
    private static int highscore;               // Current high score, resets if program is turned off
    private static int scoreMultiplier;
    private static float time;
    private static float highTime;
    public float startingTime = 120; // 2mins

    public Text scoreText;
    public Text scoreShadowText;
    public Text highscoreText;
    public Text highscoreShadowText;
    public Text scoreMultiplierText;
    public Text scoreMultiplierShadowText;
    private static GameObject newHighScoreT;
    private static GameObject newHighTimeT;
    public GameObject newHighScoreText;
    public GameObject newHighTimeText;

    public static bool hasChain;

    private int level;
  
    void Awake()
    {
        score = 0;
        scoreMultiplier = 1;
        //highscore = 100;
        
        level = GameController.level;    
        if (level == (int)GameController.Level.story)
        {
            highscore = 5151992;
        }       
        highTime = 10f;        
        //Load();
        // For testing win condition
    }
    void Start()
    {
        time = startingTime;
        newHighScoreT = newHighScoreText;
        newHighTimeT = newHighTimeText;
    }
    void Update()
    {   
        if (isHighScore())
        {
            UpdateHighScore();
        }     

        scoreText.text = "Score: " + score;
        scoreShadowText.text = "Score: " + score;
        
        highscoreText.text = "High Score: " + highscore;
        highscoreShadowText.text = "High Score: " + highscore;

        if (!hasChain)
        {
            scoreMultiplierText.color = Color.white;
        } 
        else
        {
            scoreMultiplierText.color = Color.green;
        }

        scoreMultiplierText.text = "" + scoreMultiplier;       
        scoreMultiplierShadowText.text = "" + scoreMultiplier;
    }

    public static void incrementMultiplier()
    {
        if (GameController.level == (int)GameController.Level.story)
        {
            scoreMultiplier = Mathf.Clamp(scoreMultiplier *= 2, 1, 1024);
        }
        else
        {
            scoreMultiplier++;
        }

        if (scoreMultiplier % 3 == 0)
        {
            FindObjectOfType<LevelController>().incrementTime();            
        }
    }

    public static void resetMultiplier()
    {
        hasChain = false;
        scoreMultiplier = 1;
    }

    public static void RestartScore()
    {
        score = 0;
    }

    public static void incrementScore(int pointValue)
    {
        score += scoreMultiplier * pointValue;
    }

    public static void decrementScore(int pointValue)
    {
        score -= scoreMultiplier * pointValue;
    }

    public static int getScore()
    {
        return score;
    }

    public static bool isHighScore()
    {
        if (score > highscore)
        {
            return true;
        }
        return false;
    }
    public static bool isHighTime()
    {
        if (time > highTime)
        {
            return true;
        }
        return false;
    }

    public static void UpdateHighScore()
    {
        //Save(score,time);
        highscore = score;
        if (newHighScoreT.gameObject != null)
            newHighScoreT.gameObject.SetActive(true);
    }

    public static void UpdateHighTime()
    {
        //Save(score,time);
        highTime = time;
        if (newHighTimeT != null)
            newHighTimeT.gameObject.SetActive(true);
    }

    public static void Save(int highScore, float highTime)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.Create(Application.persistentDataPath + "/LD41PlayerInfo.dat");
        PlayerData data = new PlayerData(highScore, highTime);
        bf.Serialize(saveFile, data);
        saveFile.Close();
    }

    public static void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        try
        {
            FileStream saveFile = File.Open(Application.persistentDataPath + "/LD41PlayerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(saveFile);
            saveFile.Close();
            if (data.highScore > highscore)
            {
                highscore = data.highScore;
            }
            if(data.highTime > highTime)
            {
                highTime = data.highTime;
            }
        }
        catch(FileNotFoundException e)
        {
            Debug.Log("not found");
            Save(0,0f);
        }
    }
}
[Serializable]
public class PlayerData
{
    public int highScore;
    public float highTime;
    public PlayerData(int highScore, float highTime)
    {
        this.highScore = highScore;
        this.highTime = highTime;
    }
}

