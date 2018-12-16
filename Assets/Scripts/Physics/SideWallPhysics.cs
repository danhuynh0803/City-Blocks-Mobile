using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWallPhysics : MonoBehaviour {

    public float hitTIme = 1f;
    public bool isLeftSide;
    bool isHitted;


    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball" && !isHitted)
        {
            if (isLeftSide)
                collision.gameObject.GetComponent<BallPhysics>().LeftSideBounce();
            else
                collision.gameObject.GetComponent<BallPhysics>().RightSideBounce();
            isHitted = true;
            StartCoroutine(ResetHit(hitTIme));
        }
    }
    IEnumerator ResetHit(float hitTime)
    {
        yield return new WaitForSeconds(hitTime);
        isHitted = false;
    }
}
