using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [Header("Note Positioning")]
    public Transform[] spawnPos = new Transform[4];
    public Transform[] finalPos = new Transform[4];

    [Header("Note Settings")]
    public GameObject noteObject;
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
        int index = (int)(Random.Range(0, 3.99f));
        GameObject newNote = 
            Instantiate(noteObject, 
                        spawnPos[index].position, 
                        Quaternion.identity) 
                as GameObject;

        newNote.GetComponent<Note>()
            .SetSpeedAndInputTimer(noteSpeed, 
                                   spawnPos[index].position, 
                                   finalPos[index].position);
    }
}
