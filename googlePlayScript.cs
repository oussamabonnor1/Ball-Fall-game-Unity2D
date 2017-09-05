using System;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

public class googlePlayScript : MonoBehaviour
{

    public GameObject text;

	// Use this for initialization
	void Start () {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
	    PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.InitializeInstance(config);
	    PlayGamesPlatform.Activate();
	    GooglePlayGames.OurUtils.Logger.DebugLogEnabled = true;
        signIn();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SignInCallback(bool success)
    {
        if (success)
        {
            // Change sign-in button text
            text.GetComponent<Text>().text = "";
        }
        else
        {
            text.SetActive(false);
        }
    }

    void signIn()
    {
        if (!Social.localUser.authenticated)
        {
            
            text.GetComponent<Text>().text = "";
            // Sign in with Play Game Services, showing the consent dialog
            // by setting the second parameter to isSilent=false.
            Social.localUser.Authenticate(SignInCallback);
        }
        else
        {
            text.GetComponent<Text>().text = "";
        }

    }

    public void unlockAchievement(string id)
    {
        Social.ReportProgress(id,100,succes =>{});
    }

    public void showAchievementUI()
    {
        Social.ShowAchievementsUI();
    }

    public void addScoreLeaderbord(string id, int score)
    {
        Social.ReportScore(score,id, succes =>{});
    }

    public void showLeaderbordUI()
    {
        Social.ShowLeaderboardUI();
    }
}
