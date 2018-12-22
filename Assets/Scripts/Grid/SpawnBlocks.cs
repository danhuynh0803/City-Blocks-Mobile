using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour {
  
    [Header("Left bottom block spawn postition")]
    public Transform startBlockPos;
    private float offsetX = -2.45f; // number here was for testing purposes
    private float offsetY = 2.7f;   // number here was for testing purposes

    [Header("Spawn block settings")]
    public float gapX; // This number already takes into account the size of the block, which is 1.9f currently
    public float gapY;
    public GameObject blockPrefab;
    public GameObject metalBlockPrefab;
    public int rows;
    public int cols;
    public static int blocksCount;
    public static int blocksHit;
    public GameObject ball;
    public float spawnDelay;
    private float timeStamp;
    private float timer;

    [Header("Gradient shift of blocks")]
    public float gradientShift;
    private float currSpawnNumber;
    
    void Start()
    {
        timeStamp = 0.0f;
        currSpawnNumber = 0.57f;
        blocksHit = 0;
        blocksCount = 0;
        //float blockSizeX = blockPrefab.GetComponent<Collider2D>().bounds.size.x;
        float midpoint = (rows * gapX * 0.5f);
        offsetX = startBlockPos.position.x - midpoint + (gapX * 0.5f);
        //Debug.Log(offsetX);
        offsetY = startBlockPos.position.y;
        timer = 0;
        Spawn();
    }

    void Update()
    {
        timer += Time.deltaTime;
        /*
        if(blocksCount <= 0)
        {
            ball.GetComponent<BallPhysics>().RespawnBall();
            Spawn();
        }
        */

        // Spawn blocks after certain delay
        if (timer >= spawnDelay)
        {
            Spawn();            
            timer = 0;
        }
    }

    void Spawn()
    {
        //SoundController.Play((int)SFX.ClearBoard); 

        blocksCount = 0;
        for (int i = 0; i < rows; i++)
       {
            for(int j = 0; j<cols; j++)
            {
                float blockX = offsetX + i * gapX;
                float blockY = offsetY + j * gapY;
                GameObject block;
                // Set color based on rows, changes gradient based on the block num

                // 5% chance of a metal block
                if (Random.Range(0.0f, 1.0f) < 0.05f)
                {
                    block = Instantiate(metalBlockPrefab, new Vector2(blockX, blockY), Quaternion.identity);
                    block.GetComponent<BlockPhysics>().isMetal = true;
                    //block.GetComponent<SpriteRenderer>().sprite = block.GetComponent<BlockPhysics>().metalSprite;
                }
                else
                {
                    block = Instantiate(blockPrefab, new Vector2(blockX, blockY), Quaternion.identity);
                    block.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(
                        Mathf.Lerp(currSpawnNumber, currSpawnNumber + gradientShift, (float)j / cols), 1.0f, 1.0f);
                }
                blocksCount++;               
            }
       }
       // Shift the gradient of the blocks
       currSpawnNumber = ((currSpawnNumber + gradientShift) % 1.0f);
    }
    public static void BlockHit()
    {
        blocksCount--;
        blocksHit++;
    }
}
