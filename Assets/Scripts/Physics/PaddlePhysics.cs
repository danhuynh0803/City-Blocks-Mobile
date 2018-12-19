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

    private Rigidbody2D rb2d;
    private float dirX;
    // TODO check performance of collider vs spriterenderer, which is more future-proof for us
    //private Vector3 minCollider, maxCollider;
    //private float midPointX; // Used to offset the collider bounds so we dont exit the break-out boundaries;
    bool isHit;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rightBoundX = rightBound.position.x;
        leftBoundX = leftBound.position.x;
        
        //minCollider = GetComponent<Collider2D>().bounds.min;
        //maxCollider = GetComponent<Collider2D>().bounds.max;        
        //midPointX = Vector3.Distance(maxCollider, minCollider) / 2.0f;
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

    private void Update()
    {
        dirX = Input.acceleration.x * paddleSpeed;
        transform.position = 
            new Vector2(Mathf.Clamp(transform.position.x, 
                                    (leftBound.position.x + GetComponent<SpriteRenderer>().bounds.size.x * 0.5f), 
                                    (rightBound.position.x - GetComponent<SpriteRenderer>().bounds.size.x * 0.5f)
                                    ),
                        transform.position.y);
    }

    void FixedUpdate()
    {
        //PaddleMobileInput();
        //PaddleInput(); // Keyboard inputs        
        rb2d.velocity = new Vector2(Input.acceleration.x * paddleSpeed, 0.0f);
    }

    void PaddleMobileInput()
    {
        //transform.Translate(Input.acceleration.x * paddleSpeed * Time.deltaTime, 0, 0);        
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
