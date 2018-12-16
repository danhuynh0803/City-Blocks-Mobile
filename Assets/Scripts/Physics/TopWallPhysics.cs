using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopWallPhysics : MonoBehaviour {

    public float hitTIme = 1f;
    bool isHitted;
    public bool isBottom;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball" && !isHitted)
        {
            if(isBottom)
            {
                collision.gameObject.GetComponent<BallPhysics>().BounceUp();
                gameObject.SetActive(false);
            }
            else
            {
                collision.gameObject.GetComponent<BallPhysics>().BounceDown();
                isHitted = true;
                StartCoroutine(ResetHit(hitTIme));
            }
        }
    }
    IEnumerator ResetHit(float hitTime)
    {
        yield return new WaitForSeconds(hitTime);
        isHitted = false;
    }
}
