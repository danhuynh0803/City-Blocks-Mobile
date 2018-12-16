using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour {

    public GameObject wordPrefab;
    public Transform wordCanvas;

    public float firstWordDelay = 7.0f;
    public float spawnDelay = 4.0f;
    private float timeStamp = 0.0f;
    public float spawnDecreaseFactor = 0.97f;
    public float horizontalOffset;  // Offset amount from center
    public float maxHeight; 

    private WordManager wordManager; 

    private void Start()
    {
        wordManager = FindObjectOfType<WordManager>();

        // Display the first word
        wordManager.AddWord("type us");
        timeStamp = Time.time + firstWordDelay;
    }

    public WordDisplay SpawnWord()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-horizontalOffset, horizontalOffset), maxHeight, 0.0f);

        GameObject wordGameObj = Instantiate(wordPrefab, 
            wordCanvas.transform.position + randomPosition, Quaternion.identity, wordCanvas);

        return wordGameObj.GetComponent<WordDisplay>();
    }

    private void Update()
    {
        if (Time.time >= timeStamp)
        {
            wordManager.AddWord();
            if(!GameModeController.isQuickMode)
                spawnDelay = Mathf.Clamp(spawnDelay * spawnDecreaseFactor, 3.0f, spawnDelay);
            timeStamp = Time.time + spawnDelay;
            
            //spawnDelay *= spawnDecreaseFactor;
        }
    }
}
