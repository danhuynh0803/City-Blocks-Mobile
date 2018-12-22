using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Powerup
{
    AddLife,
    LengthenPaddle,
    DecreaseBallSpeed,
    Bumpers
};

public class PowerupController : MonoBehaviour
{
    public GameObject ball;
    public GameObject powerUpJumpOutPrefab;
    private LevelController levelController;

    #region DecreaseBallSpeed
    [Header("DecreaseBallSpeed")]
    public float decreaseBallSpeedFactor;
    [Header("Read Only")]
    [SerializeField]
    private float decreaseBallSpeedDuration;
    private bool isSlowDown;
    public int DecreaseBallSpeedStack;

    public void SetDecreaseBallSpeed(int stack)
    {
        DecreaseBallSpeedStack = stack;
    }

    public void DecreaseBallSpeedEffect(float time)
    {
        isSlowDown = true;
        SetDecreaseBallSpeed(DecreaseBallSpeedStack + 1);
        Rigidbody2D ballRidigBody = ball.GetComponent<Rigidbody2D>();
        BallPhysics ballPhysics = ball.GetComponent<BallPhysics>();
        ballPhysics.SetSpeedX(ballPhysics.speedX * decreaseBallSpeedFactor);
        ballPhysics.SetSpeedY(ballPhysics.speedY * decreaseBallSpeedFactor);
        decreaseBallSpeedDuration = time;
        ballRidigBody.velocity = new Vector2(ballRidigBody.velocity.x * decreaseBallSpeedFactor, ballRidigBody.velocity.y * decreaseBallSpeedFactor);
    }

    public void ResetBallSpeedBuff()
    {
        isSlowDown = false;
        Rigidbody2D ballRidigBody = ball.GetComponent<Rigidbody2D>();
        BallPhysics ballPhysics = ball.GetComponent<BallPhysics>();
        ballPhysics.SetSpeedX(ballPhysics.InitalSpeedX);
        ballPhysics.SetSpeedY(ballPhysics.InitalSpeedY);
        ballRidigBody.velocity = new Vector2(ballRidigBody.velocity.x / Mathf.Pow(decreaseBallSpeedFactor, DecreaseBallSpeedStack), ballRidigBody.velocity.y / Mathf.Pow(decreaseBallSpeedFactor, DecreaseBallSpeedStack));
        DecreaseBallSpeedStack = 0;
        decreaseBallSpeedDuration = 0;
    }
    #endregion

    public void ActivatePowerup(PowerupNote pnote)
    {
        GameObject powerUpText = Instantiate(powerUpJumpOutPrefab);
        // TODO, replace this magicstring
        powerUpText.transform.parent = GameObject.Find("HUDPanel").transform;
        powerUpText.transform.position = new Vector2(850f, 550f);
        Text white = powerUpText.GetComponent<PowerUpJumpOutTextAnimation>().white;
        Text black = powerUpText.GetComponent<PowerUpJumpOutTextAnimation>().black;       
        //Debug.Log("In Powerup functions");
        switch (pnote.powerup)
        {
            case Powerup.AddLife:
                //Debug.Log("AddLife");
                white.text = "Life++!";
                black.text = "Life++!";
                white.color = new Color(1.0f, 0f, 0f);
                SoundController.Play((int)SFX.Pickup, 0.1f);
                AddLife(pnote);
                break;

            case Powerup.DecreaseBallSpeed:
                //Debug.Log("Ballspeed");
                white.text = "Speed--!";
                black.text = "Speed--!";
                white.color = new Color(1.0f, 1.0f, 0f);
                SoundController.Play((int)SFX.Brakes, 0.1f);
                DecreaseBallSpeedEffect(20f);
                break;

            case Powerup.LengthenPaddle:
                //Debug.Log("Paddle");
                white.text = "Size++!";
                black.text = "Size++!";
                white.color = new Color(0f, 1.0f, 0f);
                LengthenPaddle(20f);
                SoundController.Play((int)SFX.Speedup, 0.1f);
                break;

            case Powerup.Bumpers:
                //Debug.Log("Bumpers");
                white.text = "Bumper!";
                black.text = "Bumper!";
                white.color = new Color(0, 1f, 1f);
                SoundController.Play((int)SFX.Speedup, 0.1f);
                ToggleBumper(true, 10);
                break;

            default:
                break;

        };
    }

    #region AddLife
    [Header("Life")]
    private bool moveWord;
    private GameObject wordObject;
    private Vector3 wordPosition;
    public GameObject lifeText;
    public GameObject lifePrefab;
    // Add life but do not add if at max lives
    public void AddLife(Note note)
    {
        wordPosition = Camera.main.ScreenToWorldPoint(note.gameObject.transform.position);
        wordObject = Instantiate(lifePrefab);
        moveWord = true;
    }
    #endregion
    #region LengthenPaddle
    [Header("LengthenPaddle")]
    public GameObject playerPaddle;
    public float lengthenScale;
    [Header("Read Only")]
    [SerializeField]
    private float lengthenDuration;
    [SerializeField]
    private Vector3 originalScale;
    private bool isLengthen;
    private int lengthStack;
    public void LengthenPaddle(float time)
    {
        isLengthen = true;
        Transform paddleTransform = playerPaddle.transform;
        paddleTransform.localScale = new Vector3(paddleTransform.localScale.x + lengthenScale, paddleTransform.localScale.y, paddleTransform.localScale.z);
        if (playerPaddle.GetComponent<BoxCollider2D>().enabled)
            Destroy(playerPaddle.GetComponent<BoxCollider2D>());
        playerPaddle.AddComponent<BoxCollider2D>();
        playerPaddle.GetComponent<BoxCollider2D>().isTrigger = true;
        lengthenDuration = time;
    }
    public void ResetScale()
    {
        isLengthen = false;
        Transform paddleTransform = playerPaddle.transform;
        paddleTransform.localScale = originalScale;
        if (playerPaddle.GetComponent<BoxCollider2D>().enabled)
            Destroy(playerPaddle.GetComponent<BoxCollider2D>());
        playerPaddle.AddComponent<BoxCollider2D>();
        playerPaddle.GetComponent<BoxCollider2D>().isTrigger = true;

    }
    #endregion
    #region Bumpers
    [Header("Bumpers")]
    public GameObject bumper;
    [Header("Read Only")]
    [SerializeField]
    private float bumperDuration;
    private bool isBumperOn;
    public void ToggleBumper(bool toggle, float time)
    {
        //isBumperOn = toggle;
        bumper.SetActive(toggle);
        //if (toggle)
        //bumperDuration = time;
        //else
        //bumperDuration = time;
    }

    #endregion
    #region Multiplier
    public void AddMultiplier()
    {

    }
    #endregion
    void Start()
    {
        DecreaseBallSpeedStack = 0;
        lengthStack = 0;
        levelController = FindObjectOfType<LevelController>();
        originalScale = playerPaddle.transform.localScale;
    }

    void Update()
    {

        if (playerPaddle.GetComponent<BoxCollider2D>() != null)
        {
            playerPaddle.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        //for health animation
        if (moveWord)
        {
            if (wordObject.GetComponent<LifeAnimation>().time < 1)
            {
                wordObject.GetComponent<LifeAnimation>().time += Time.deltaTime;
                wordObject.transform.position = Vector3.Lerp(wordPosition, lifeText.transform.position, 2 * wordObject.GetComponent<LifeAnimation>().time);
            }
            else
            {
                Destroy(wordObject);
                levelController.AddLife();
                wordObject = null;
                moveWord = false;
            }
        }
        //bumper buff
        /**if(bumperDuration > 0)
        {
            bumperDuration -= Time.deltaTime;
        }
        else
            if(isBumperOn)
                ToggleBumper(false, 0);
        **/
        //size buff
        if (lengthenDuration > 0)
            lengthenDuration -= Time.deltaTime;
        else
        {
            if (isLengthen)
                ResetScale();
        }
        //speed buff
        if (decreaseBallSpeedDuration > 0)
        {
            decreaseBallSpeedDuration -= Time.deltaTime;
        }
        else
        {
            if (isSlowDown)
                ResetBallSpeedBuff();
        }
    }
}

