using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public Transform spawn1, spawn2, spawn3, spawn4;

    [Header("Note Settings")]
    public GameObject noteObject;
    public int beatsPerMinute;

    private Vector3 s1Pos, s2Pos, s3Pos, s4Pos;
    private Vector3 f1Pos, f2Pos, f3Pos, f4Pos;

    // Start is called before the first frame update
    void Start()
    {
        s1Pos = spawn1.position;
        s2Pos = spawn2.position;
        s3Pos = spawn3.position;
        s4Pos = spawn4.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Random Note spawner
    private void RandomSpawn()
    {

    }
}
