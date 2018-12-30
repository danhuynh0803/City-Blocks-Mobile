using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

// This script just holds level settings 
// Used by the level manager 
public class GameController : MonoBehaviour {

    // 0 = story
    // 1 = challenge
    // 2 = endless

    //static bool isGameControllerOn;

    public enum Level
    {
        story = 0,
        challenge, 
        endless
    };

    public static int level = (int)Level.challenge;

    void Awake()
    {
        //isGameControllerOn = true;
        DontDestroyOnLoad(this);
    }

    public void ShowLeaderboard()
    {
        ServicesManager.instance.ShowLeaderboardsUI();
    }
}
