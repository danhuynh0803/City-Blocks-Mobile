using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour {
    [Header("Adjust Ball Speed here")]
    public float speedY = 10;
    public float speedX = 10;

    private float initalSpeedX;
    private float initalSpeedY;
    private float radian;

    [Header("Respawn Settings")]
    public float startDelay = 2.0f;
    public float respawnDelay = 1.5f;

    [Header("Extra Settings")]
    public float ballBounceVolume = 0.1f;
    public float glassVolume = 1f;

    Rigidbody2D rigidBody;

    public GameObject paddle;
    public GameObject levelController;

    public float InitalSpeedX
    {
        get
        {
            return initalSpeedX;
        }

        set
        {
            initalSpeedX = value;
        }
    }

    public float InitalSpeedY
    {
        get
        {
            return initalSpeedY;
        }

        set
        {
            initalSpeedY = value;
        }
    }

    public float Radian
    {
        get
        {
            return radian;
        }

        set
        {
            radian = value;
        }
    }

    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        Kick();
        InitalSpeedX = speedX;
        InitalSpeedY = speedY;
    }
    void Update()
    {
        CheckingOutOfBOund();
    }
    public void Kick()
    {
        StartCoroutine(WaitThenKick(startDelay));

    }
    IEnumerator WaitThenKick(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GetComponent<TrailRenderer>().enabled = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0.2f, -1 * speedY);
    }
    public void Bounce(bool isMetal,float blockX, float bound)
    {
        if(SpawnBlocks.blocksHit % 50 == 0)
            SoundController.Play((int)SFX.CarExplodeGlass, glassVolume);
        else
            SoundController.Play((int)SFX.BallBounce, ballBounceVolume);

        if (isMetal)
        {
            if(blockX - bound >= transform.position.x|| blockX + bound <= transform.position.x)
                rigidBody.velocity = new Vector2(rigidBody.velocity.x * -1, rigidBody.velocity.y );
            else
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * -1);
        }
    }

    public void Bounce(float radian)
    {
        SoundController.Play((int)SFX.BallBounce, ballBounceVolume);
        rigidBody.velocity = new Vector2(speedX * Mathf.Cos(radian), rigidBody.velocity.y * -1);

    }
    public void BounceDown()
    {
        SoundController.Play((int)SFX.BallBounce, ballBounceVolume);
        rigidBody.velocity = new Vector2(rigidBody.velocity.x , Mathf.Abs(rigidBody.velocity.y) * -1f);
    }
    public void BounceUp()
    {
        SoundController.Play((int)SFX.BallBounce, ballBounceVolume);
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Abs(rigidBody.velocity.y));
    }
    public void LeftSideBounce()
    {
        SoundController.Play((int)SFX.BallBounce, ballBounceVolume);
        rigidBody.velocity = new Vector2(Mathf.Abs(rigidBody.velocity.x), rigidBody.velocity.y);
    }
    public void RightSideBounce()
    {
        SoundController.Play((int)SFX.BallBounce, ballBounceVolume);
        rigidBody.velocity = new Vector2(Mathf.Abs(rigidBody.velocity.x) * -1f, rigidBody.velocity.y);
    }
    public void RespawnBall()
    {
        SoundController.Play((int)SFX.BallFall, 0.7f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        if (paddle == null)
            paddle = GameObject.Find("PlayerPaddle");
        transform.position = new Vector2(paddle.transform.position.x, paddle.transform.position.y + (0.37f + 2.61f));
        StartCoroutine(WaitThenKick(respawnDelay));
    }
    void CheckingOutOfBOund()
    {
        if(transform.position.y < -8f && !LevelController.isGameOver)
        {
            if(levelController.GetComponent<LevelController>().Life <= 0)
            {
                // Disable lives if in challenge mode
                if (GameController.level != (int)GameController.Level.challenge)
                {
                    levelController.GetComponent<LevelController>().GameOver();
                }
            }
           
          
            levelController.GetComponent<LevelController>().LoseLife(1);
            levelController.GetComponent<LevelController>().SetLifeText();
            // Restart score multiplier when ball is lost
            ScoreController.resetMultiplier();
            RespawnBall();
            GetComponent<TrailRenderer>().enabled = false;           
        }
        
    }
    public void SetSpeedX(float x)
    {
        speedX = x;
    }
    public void SetSpeedY(float y)
    {
        speedY = y;
    }

}
