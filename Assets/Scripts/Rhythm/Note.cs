using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float speed;
    public float inputTimer;
    public float timer;
    private bool isMoving;

    public void SetSpeedAndInputTimer(float speed, 
                                      Vector3 startPos, 
                                      Vector3 endPos)
    {
        this.speed = speed;
        inputTimer = (startPos.y - endPos.y) / speed;
        timer = 0;
        isMoving = true;
    }    

    // Update is called once per frame
    protected void Update()
    {
        if (isMoving)
        {
            timer += Time.deltaTime;
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }

    public virtual void ProcessInput()
    {
        // Basic notes just add to the score
    }
}
