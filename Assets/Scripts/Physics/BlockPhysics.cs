using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPhysics : MonoBehaviour {

    public Block block;
    bool isHitted;
    public int score;
    //remeber to change this under prefab not here
    //metal block too
    public float fallSpeed = 0.2f;
    public float fallSpeedQuickMode = 2f;
    private GameObject killLayer;
    public bool isMetal; // will deflect the ball and indestructable
    public Sprite metalSprite;
    public float resetIsHit = 0.1f;
    private float timeStamp = 0;
    private LevelController levelController;

    void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        if (GameModeController.isQuickMode)
            fallSpeed = fallSpeedQuickMode;
        killLayer = GameObject.FindGameObjectWithTag("KillBlockLayer");
        block = new Block(transform.position.x, transform.position.y);
    }

    private void Update()
    {
        fallSpeed = levelController.blockSpeed;
        transform.Translate(0f, -fallSpeed * Time.deltaTime, 0f);
        // Kill the block if it falls below the kill layer
        if (transform.position.y < killLayer.transform.position.y)
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 1.0f);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball" && !isHitted)
        {
            SpawnBlocks.BlockHit();
            collision.gameObject.GetComponent<BallPhysics>().Bounce(isMetal, transform.position.x, GetComponent<SpriteRenderer>().bounds.size.x/2);
            ScoreController.incrementScore(score);
            isHitted = true;
            // Kill block only if its not metal
            if (!isMetal)
            {
                Kill();
            }
            else
            {
                StartCoroutine(ResetHit(0.2f));
            }
        }
    }

    IEnumerator ResetHit(float hitTime)
    {
        yield return new WaitForSeconds(hitTime);
        isHitted = false;
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
