using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddlePhysics : MonoBehaviour {
    [Header("Adjust Paddle Speed here")]
    public float paddleSpeed = 30f;
    public float hitTIme = 1f;
    public float rightBoundX = 13.7f;
    public float leftBoundX = -4.3f;
    bool isHitted;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball" && !isHitted)
        {
            float angle = GetOffset(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);
            collision.gameObject.GetComponent<BallPhysics>().Bounce(angle);
            isHitted = true;
            //StartCoroutine(ResetHit(hitTIme));
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball" && isHitted)
        {
            isHitted = false;
        }
    }
    void Update()
    {
        PaddleInput();
    }

    IEnumerator ResetHit(float hitTime)
    {
        yield return new WaitForSeconds(hitTime);
        isHitted = false;
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
