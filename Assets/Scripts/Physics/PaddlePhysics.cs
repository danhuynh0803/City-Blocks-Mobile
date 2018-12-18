using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddlePhysics : MonoBehaviour {

    [Header("Adjust Paddle Speed here")]
    public float paddleSpeed = 30f;
    public float hitTime = 1f;

    [Header("Paddle Boundary")]
    public Transform leftBound, rightBound;
    private float leftBoundX, rightBoundX;

    bool isHit;

    private void Start()
    {
        rightBoundX = rightBound.position.x;
        leftBoundX = leftBound.position.x;
    }
    
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball" && !isHit)
        {
            float angle = GetOffset(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);
            collision.gameObject.GetComponent<BallPhysics>().Bounce(angle);
            isHit = true;
            //StartCoroutine(ResetHit(hitTIme));
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball" && isHit)
        {
            isHit = false;
        }
    }

    void Update()
    {
        //transform.Translate(Input.acceleration.y * paddleSpeed, 0, 0);
        PaddleInput();
    }

    private void LateUpdate()
    {
        //isHit = false;
    }

    IEnumerator ResetHit(float hitTime)
    {
        yield return new WaitForSeconds(hitTime);
        isHit = false;
    }

    void PaddleInput()
    {       
        if((Input.GetKey(KeyCode.RightArrow) || (Input.GetKey(KeyCode.RightBracket))) && (transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x/2) <= rightBoundX)
        {
            transform.position = new Vector2(transform.position.x + paddleSpeed * Time.deltaTime, transform.position.y);
        }
        else if ((Input.GetKey(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.LeftBracket))) && (transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x/2) >= leftBoundX)
        {
            transform.position = new Vector2(transform.position.x - paddleSpeed * Time.deltaTime, transform.position.y);
        }      
    }

    float GetOffset(float ballX, float ballY)
    {
        float radian = 0;
        float paddleX = transform.position.x;
        float paddleY = transform.position.y;
        radian = Mathf.Atan2(ballY - paddleY, ballX - paddleX);
        return radian;
    }
    
}
