using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killborder : MonoBehaviour
{
    private LevelController levelController;

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Note")
        {
            //Debug.Log("Remove Note");
            Destroy(collision.gameObject);
            levelController.DecrementBlockSpeed();
        }
    }
}
