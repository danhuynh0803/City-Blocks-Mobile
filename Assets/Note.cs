using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float speed;
    public float inputTimer;
    public float timer; 

    public void SetSpeedAndInputTimer(float speed, 
                                      Vector3 startPos, 
                                      Vector3 endPos)
    {
        this.speed = speed;
        inputTimer = Vector3.Distance(startPos, endPos) / speed;
        timer = 0;
    }    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        timer += Time.deltaTime;
    }
}
