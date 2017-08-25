using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI components")]
    public GameObject highScoreText;
    public GameObject pauseMenu;
    public GameObject startDark;
    public GameObject startMenu;
    public GameObject parameterMenu;

    GameObject musicPlayer;

    [Header("Essentiel components")]
    public Sprite[] IconSprites;

    [Header("Scripts")]
    public Movement movementScript;
    public Controller controllerScript;

    bool addition;
    private bool play;
    bool doubleTap;


    // Use this for initialization
    void Start()
    {
        doubleTap = false;
        addition = false;

        musicPlayer = GameObject.Find("Music Manager");

        if (PlayerPrefs.GetInt("score") == 0)
        {
            //sound on
            parameterMenu.transform.GetChild(2).GetComponent<Image>().sprite = IconSprites[0];
        }
        else
        {
            //sound off
            parameterMenu.transform.GetChild(2).GetComponent<Image>().sprite = IconSprites[1];
            //ONOFF.GetComponent<Text>().text = "OFF";
            if (musicPlayer.GetComponent<AudioSource>().isPlaying) musicPlayer.GetComponent<AudioSource>().Pause();
        }
        highScoreText.GetComponent<Text>().text = "  Best Score\n      : " + PlayerPrefs.GetInt("Score");
        play = false;
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
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
        StartCoroutine(paramAnim(parameterMenu.activeSelf));
    }

    IEnumerator paramAnim(bool state)
    {
        if (state)
        {
            for (int i = 9; i > 0; i--)
            {
                for (int j = 2; j > -1; j--)
                {
                    Vector3 a = new Vector3(parameterMenu.transform.GetChild(j).transform.localPosition.x,
                        parameterMenu.transform.GetChild(j).transform.localPosition.y,
                        parameterMenu.transform.GetChild(j).transform.localPosition.z);

                    parameterMenu.transform.GetChild(j).transform.localPosition =
                        new Vector3(a.x, a.y - (i * (2.75f + j * 2.75f)), a.z);
                    yield return new WaitForSeconds(0.01f);
                }
            }
            parameterMenu.SetActive(!state);

        }
        else
        {
            parameterMenu.SetActive(!state);
            for (int i = 1; i < 10; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Vector3 a = new Vector3(parameterMenu.transform.GetChild(j).transform.localPosition.x,
                        parameterMenu.transform.GetChild(j).transform.localPosition.y,
                        parameterMenu.transform.GetChild(j).transform.localPosition.z);

                    parameterMenu.transform.GetChild(j).transform.localPosition =
                        new Vector3(a.x, a.y + (i * (2.75f + j * 2.75f)), a.z);
                    yield return new WaitForSeconds(0.01f);
                }
            }}
    }
    
    //if On/OFF clicked
    public void OnOff()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            //sound BECOMING OFF
            parameterMenu.transform.GetChild(2).GetComponent<Image>().sprite = IconSprites[1];
            musicPlayer.GetComponent<AudioSource>().Pause();
            PlayerPrefs.SetInt("sound", 1);
        }
        else
        {
            //sound BECOMING ON
            parameterMenu.transform.GetChild(2).GetComponent<Image>().sprite = IconSprites[0];
            PlayerPrefs.SetInt("sound", 0);
            musicPlayer.GetComponent<AudioSource>().UnPause();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        ShowAd();
    }

    void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
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