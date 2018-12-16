using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseBallSpeed : MonoBehaviour {

    public GameObject ball;
    public float speedFactor;
    public float duration;
    public float maxDuration;
    public int stack;


    public void Start()
    {
        stack = 0;
    }

    public void SetDuration(float time)
    {
        duration = Mathf.Clamp(time, 0, maxDuration);
    }
    public void SetStack(int stack)
    {
        this.stack = stack;
    }

    public void Effect()
    {
        SetStack(stack + 1);
        Rigidbody2D ballRidigBody = ball.GetComponent<Rigidbody2D>();
        BallPhysics ballPhysics = ball.GetComponent<BallPhysics>();
        ballPhysics.SetSpeedX(ballPhysics.speedX * speedFactor);
        ballPhysics.SetSpeedY(ballPhysics.speedY * speedFactor);
        SetDuration(30);
        ballRidigBody.velocity = new Vector2(ballRidigBody.velocity.x * speedFactor, ballRidigBody.velocity.y * speedFactor);
    }

    public void ClearBuff()
    {
        Rigidbody2D ballRidigBody = ball.GetComponent<Rigidbody2D>();
        BallPhysics ballPhysics = ball.GetComponent<BallPhysics>();
        ballPhysics.SetSpeedX(ballPhysics.InitalSpeedX);
        ballPhysics.SetSpeedY(ballPhysics.InitalSpeedY);
        ballRidigBody.velocity = new Vector2(ballRidigBody.velocity.x / Mathf.Pow(speedFactor, stack), ballRidigBody.velocity.y / Mathf.Pow(speedFactor, stack));
        stack = 0;
        duration = 0;
    }
}
