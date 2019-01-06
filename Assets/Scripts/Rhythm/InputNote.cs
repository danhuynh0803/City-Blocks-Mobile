using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNote : MonoBehaviour
{
    [Header("Note combo settings")]
    public GameObject comboTextPrefab;
    public Transform comboTransform;  

    private LevelController levelController;    // For accessing block speed, which will be tied to note performance

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Note note = collision.gameObject.GetComponent<Note>();
        //if (collision.gameObject.tag.Equals("Note"))
        if (note != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                note.ProcessInput();
                ProcessNoteScore(collision.gameObject.GetComponent<Note>());
                Destroy(collision.gameObject);
            }
        }
    }

    private void DisplayComboText(string text, Color color)
    {
        GameObject jumpOutTextObject = Instantiate(comboTextPrefab);
        // TODO, replace this magicstring
        jumpOutTextObject.transform.parent = comboTransform;
        jumpOutTextObject.transform.localPosition = new Vector2(0f, 0f);        
        Text white = jumpOutTextObject.GetComponent<JumpOutText>().white;
        Text black = jumpOutTextObject.GetComponent<JumpOutText>().black;
        white.color = color;
        white.text = text;
        black.text = text;
        
    }

    // Score is higher when the note is pressed with near its inputTimer
    private void ProcessNoteScore(Note note)
    {
        // Increase the block speed since we tapped on the note
        levelController.IncrementBlockSpeed();

        float diff = Mathf.Abs(note.inputTimer - note.timer);
        //float diff = Vector3.Distance(note.transform.position, transform.position);

        if (diff <= 0.05f)
        {
            DisplayComboText("Perfect!", Color.green);
     
        }
        else if (diff <= 0.15f)
        {
            DisplayComboText("Great!", Color.cyan);
            //Debug.Log("Great!");
        }
        else if (diff <= 0.3f)
        {
            DisplayComboText("Good", Color.yellow);
            //Debug.Log("Good!");
        }
        else if (diff <= 0.5f)
        {
            DisplayComboText("Ok", Color.red);            
            //Debug.Log("OK!");
        }
        else
        {
            DisplayComboText("Bad.", Color.black);            
            //Debug.Log("Bad!");
        }


    }

    private void OnMouseDown()
    {
        //Debug.Log(gameObject.ToString() + " pressed");
    }
}
