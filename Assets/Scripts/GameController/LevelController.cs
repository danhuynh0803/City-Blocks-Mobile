using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class LevelController : MonoBehaviour {

	public GameObject GameOverPanel;							// Display when lives reach 0
    public GameObject WinPanel;
	public GameObject playerSpawnPoint;							// Player1's spawnpoint
    public float respawnDelay;
    public int initialLife;
    private int life;
    public int maxLife;
	public Text timer; 											// Time remaining
	public Text timerShadow;		
	public Text p1Score;										// Player1's score
	public Text p1ScoreShadow;
    public Text lifePoint;                                      // Life
    public Text lifePointShadow;

    [Header("Challenge Mode Settings")]
    public float startingTime = 120; // 2mins
    public float deathTimeLost = 30; // Lose 30 seconds per death
    public float comboTimeGain = 15; // Gain 15 seconds per 3 words?
    private static float time;
    private static float highTime;
    

    private MainMenu mainMenu;

	public float gameTimer;
	private float currentTime;
	private bool hasUpdatedScore = false;
	public static bool isGameOver = false;
    
    public int Life
    {
        get { return life; }    
        set { life = value;}
    }

    void Start () {
        mainMenu = FindObjectOfType<MainMenu>();
        time = startingTime;
        Life = initialLife;
        SetLifeText();
        GameOverPanel.SetActive(false);
		currentTime = gameTimer;
		hasUpdatedScore = false;
		isGameOver = false;
		ScoreController.RestartScore ();
	}
	
	// Update is called once per frame
	void Update ()
    {
        time -= Time.deltaTime;

        SetLifeText();
        
        if (GameController.level == (int)GameController.Level.challenge)
        {
            if (time <= 0)
            {
                GameOver();
            }
        }

		if (isGameOver)
        {
			//DisplayFinalText (hasUpdatedScore);
		}
	}
	
	public void Respawn() 
	{
		StartCoroutine("RespawnCo");
	}
	
	public IEnumerator RespawnCo() 
	{
        /*
		player.GetComponent<Renderer>().enabled = false; 
		yield return new WaitForSeconds(respawnDelay);
		
		player.enabled = true; 
		player.GetComponent<Renderer>().enabled = true; 
		player.GetComponent<Rigidbody> ().velocity = Vector3.zero;				// Reset ball to zero velocity
		player.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;		// Reset ball to zero angular velocity


		player.transform.position = playerSpawnPoint.transform.position;
        */

        yield return new WaitForSeconds(0);
	}

	public void waitDelay() 
	{ 
		StartCoroutine(wait ());
	}
	
	IEnumerator wait() 
	{ 
		yield return new WaitForSeconds(respawnDelay);
	}

    // Win condition
    public void Win()
    {
        mainMenu.LoadWinScene();
        //WinPanel.SetActive(true);
    }
	
	public void GameOver() 
	{
        isGameOver = true;
        Time.timeScale = 0.0f;
        time = 0.0f;
        if(GameModeController.isQuickMode)
        {
            if (ScoreController.isHighTime())
                ScoreController.UpdateHighTime();
        }
        else if(ScoreController.isHighScore())
        {
            ScoreController.UpdateHighScore();
        }
		GameOverPanel.SetActive (true);
        GameOverPanel.transform.SetSiblingIndex(GameOverPanel.transform.parent.childCount-1);
	}
		
	private void DisplayFinalText(bool isHighScore) 
	{
		if (isHighScore) {
			timer.text = "New High Score: " + ScoreController.getScore();
			timerShadow.text = "New High Score: " + ScoreController.getScore();
			isHighScore = true;
		} else { 
			// Display player's score when timer reaches 0				
			timer.text = "Final Score: " + ScoreController.getScore();
			timerShadow.text = "Final Score: " + ScoreController.getScore();
		}
	}

    public void AddLife()
    {
        Life = Mathf.Clamp(Life + 1, 0, maxLife);
        incrementTime(); 
    }

    public void LoseLife(int lose)
    {
        Life = Mathf.Clamp(Life - lose, 0, maxLife);
        decrementTime();       
    }

    public void SetLifeText()
    {
        // If in challenge mode, replace lives with a timer instead
        if (GameController.level == (int)GameController.Level.challenge)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            TimeSpan highTimeSpan = TimeSpan.FromSeconds(highTime);

            string timeText = "";
            string highTimeText = "";
            if (time >= 3600f)
                timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            else
                timeText = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
            if (highTime >= 3600f)
                highTimeText = string.Format("{0:D2}:{1:D2}:{2:D2}", highTimeSpan.Hours, highTimeSpan.Minutes, highTimeSpan.Seconds);
            else
                highTimeText = string.Format("{0:D2}:{1:D2}", highTimeSpan.Minutes, highTimeSpan.Seconds);
            lifePoint.text = "Time: " + timeText;
            lifePointShadow.text = "Time: " + timeText;
        }
        else
        {
            lifePoint.text = "Life: " + Life;
            lifePointShadow.text = "Life: " + Life;
        }
    }

    public void decrementTime()
    {
        time -= deathTimeLost;
    }

    public void incrementTime()
    {
        SoundController.Play((int)SFX.Pickup, 0.1f);
        time += comboTimeGain;
    }
}
