using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputNote : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Note"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                ProcessNoteScore(collision.gameObject.GetComponent<Note>());

                Destroy(collision.gameObject);
            }
        }
    }

    // Score is higher when the note is pressed with near its inputTimer
    private void ProcessNoteScore(Note note)
    {
        float diff = Mathf.Abs(note.inputTimer - note.timer);

        if (diff <= 0.05f)
        {
            //Debug.Log("Perfect");
        }
        else if (diff <= 0.15f)
        {
            //Debug.Log("Great!");
        }
        else if (diff <= 0.3f)
        {
            //Debug.Log("Good!");
        }
        else if (diff <= 0.5f)
        {
            //Debug.Log("OK!");
        }
        else
        {
            //Debug.Log("Bad!");
        }
    }

    private void OnMouseDown()
    {
        //Debug.Log(gameObject.ToString() + " pressed");
    }
}
