using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ServicesManager : MonoBehaviour
{
    public static ServicesManager instance;
    public Text authStatus;
    public Text signInButtonText;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        
    }

    public void SignInButton()
    {
        SignIn();
    }

    private void SignIn()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // Sign in with Play Game Services, showing the consent dialog
            // by setting the second parameter to isSilent=false.
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
        }
        else
        {
            // Sign out of play games
            PlayGamesPlatform.Instance.SignOut();

            // Reset UI
            signInButtonText.text = "Sign In";
            authStatus.text = "";
        }

        /*
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) => {
                if (success)
                    Debug.Log("local user login sucessful");
            });
        }
        */
    }

    public void SignInCallback(bool success)
    {
        if (success)
        {
            Debug.Log("Signed in!");

            // Change sign-in button text
            signInButtonText.text = "Sign out";

            // Show the user's name
            authStatus.text = "Signed in as: " + Social.localUser.userName;
        }
        else
        {
            Debug.Log("Sign-in failed...");

            // Show failure message
            signInButtonText.text = "Sign in";
            authStatus.text = "Sign-in failed";
        }
    }

    #region Achievements

    /*public void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100, (bool success) =>
        {

        });
    }

    public void IncrementAchievement(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.InstanceAchievement(id, stepsToIncrement, success) =>
        {

        });
    }*/
    #endregion

    #region Leaderboard

    public void AddScoreToLeaderboard(string leaderboardID, long score)
    {
        Social.ReportScore(score, leaderboardID, 
            (bool success) =>{
                if(success)
                    Debug.Log("post sucessful");
            }
       );
    }

    public int LoadScoreFromLeaderboard()
    {
        int score = 0;
        PlayGamesPlatform.Instance.LoadScores(
            GPGSIds.leaderboard_leaderboard,
            LeaderboardStart.PlayerCentered,
            100,
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.AllTime,
            (LeaderboardScoreData data) =>
            {
                if(data.Valid)
                {
                    Debug.Log(data.Id);
                    Debug.Log(data.PlayerScore);
                    Debug.Log(data.PlayerScore.userID);
                    Debug.Log(data.PlayerScore.formattedValue);
                    score = (int)data.PlayerScore.value;
                }
            });
        return score;
    }

    public void ShowLeaderboardsUI()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        else
        {
            Debug.Log("Cannot show leaderboard: not authenticated");
        }
        //((PlayGamesPlatform)Social.Active).ShowLeaderboardUI("CgkI0I7Ty48CEAIQAQ");
    }
#endregion
}
