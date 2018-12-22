using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [Header("Note Positioning")]
    public Transform[] spawnPos = new Transform[2];
    public Transform[] finalPos = new Transform[2];

    [Header("Note Settings")]
    public GameObject noteObject;
    public GameObject powerupNoteObject;
    public int beatsPerMinute;
    public float noteSpeed = 5.0f; // for testing, later scale with beatsPerMinute
    
    private float spawnTime;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        spawnTime = 60.0f / (float)beatsPerMinute;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnTime)
        {
            //Debug.Log("Spawn a note");
            RandomNoteSpawn();
            timer = 0;
        }
    }

    // Spawns note randomly at one of the four spawn positions
    private void RandomNoteSpawn()
    {
        int index = (int)(Random.Range(0, (float)(spawnPos.Length-0.01f)));
        float powerupChance = 0.15f;
        GameObject newNote;
        if (Random.Range(0.0f, 1.0f) < powerupChance)
        {
            newNote = Instantiate(powerupNoteObject,
                                  spawnPos[index].position,
                                  Quaternion.identity)
                        as GameObject;

            // TODO: Refactor so that we can place % chance of spawn on the object directly
            // Hardcode for now
            float powerupSelection = Random.RandomRange(0, 1.0f);
            if (powerupSelection < 0.25f)
            {
                // Note useful when playing in time mode
                //newNote.GetComponent<PowerupNote>().powerup = Powerup.AddLife;
                newNote.GetComponent<PowerupNote>().powerup = Powerup.DecreaseBallSpeed;
            }
            else if (powerupSelection < 0.5f)
            {
                newNote.GetComponent<PowerupNote>().powerup = Powerup.Bumpers;
            }
            else if (powerupSelection < 0.75f)
            {
                newNote.GetComponent<PowerupNote>().powerup = Powerup.DecreaseBallSpeed;
            }
            else
            {
                newNote.GetComponent<PowerupNote>().powerup = Powerup.LengthenPaddle;
            }
        }
        else
        {
            newNote = Instantiate(noteObject,
                          spawnPos[index].position,
                          Quaternion.identity)
                as GameObject;
        }

        newNote.GetComponent<Note>()
            .SetSpeedAndInputTimer(noteSpeed, 
                                   spawnPos[index].position, 
                                   finalPos[index].position);
    }

}
