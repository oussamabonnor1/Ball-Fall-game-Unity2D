using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject highScoreText;
    public GameObject pauseMenu;
    public GameObject startDark;
    public GameObject startMenu;
    public GameObject parameterMenu;

    GameObject musicPlayer;

    public Movement movementScript;
    public Controller controllerScript;

    bool addition;
    private bool play;
    bool doubleTap;


    // Use this for initialization
    void Start () {

        doubleTap = false;
        addition = false;

        musicPlayer = GameObject.Find("Music Manager");

        if (PlayerPrefs.GetInt("score") == 0)
        {
           // ONOFF.GetComponent<Text>().text = "ON";
            if (!musicPlayer.GetComponent<AudioSource>().isPlaying) musicPlayer.GetComponent<AudioSource>().Play();

        }
        else
        {
            //ONOFF.GetComponent<Text>().text = "OFF";
            if (musicPlayer.GetComponent<AudioSource>().isPlaying) musicPlayer.GetComponent<AudioSource>().Pause();

        }
        highScoreText.GetComponent<Text>().text = "  Best Score\n        : " + PlayerPrefs.GetInt("Score");
        play = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        play = true;
        movementScript.can = true;
        startDark.SetActive(false);

        PauseGame();
        PauseGame();
    }

    public void PauseGame()
    {
        addition = true;
        play = !play;
        movementScript.toogle(play);
        pauseMenu.SetActive(!play);

        if (play == false)
        {
            Time.timeScale = 0;
            StopAllCoroutines();
        }
        else
        {
            Time.timeScale = 1;
            StartCoroutine(controllerScript.Spawn());
        }
    }

    //if we press Quit button
    public void QuitClicked()
    {
        pauseMenu.SetActive(false);
        parameterMenu.SetActive(false);
        startMenu.SetActive(false);
    }

    //if we press No (quit) button
    public void No()
    {
        startMenu.SetActive(true);
    }
    // if yes (quit) button 
    public void QuitGame()
    {
        Application.Quit();
    }

    //about us button clicked
    public void AboutUs()
    {
        
    }
    //if return (about us) button is pressed
    public void Returning()
    {
        
    }

    //if option button clicked
    public void optionClicked()
    {
        parameterMenu.SetActive(!parameterMenu.activeSelf);
    }

    

    //if On/OFF clicked
    /*public void OnOff()
    {
        if (ONOFF.GetComponent<Text>().text == "ON")
        {
            ONOFF.GetComponent<Text>().text = "OFF";
            musicPlayer.GetComponent<AudioSource>().Pause();
            PlayerPrefs.SetInt("sound", 1);
        }
        else
        {
            ONOFF.GetComponent<Text>().text = "ON";
            PlayerPrefs.SetInt("sound", 0);
            musicPlayer.GetComponent<AudioSource>().UnPause();
        }
    }*/

    public void RestartGame()
    {
        ShowAd();
    }

    void ShowAd()
    {
        SceneManager.LoadScene("Main");
    }

    public void siteVisit()
    {
        Application.OpenURL("www.ballfall.ga");
    }

    public void rateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.Oussama.BallFall");
    }
}
