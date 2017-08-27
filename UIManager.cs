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
    public GameObject creditMenu;
    public GameObject finishedMenu;
    public GameObject paramButton;
    public GameObject creditButton;
    public GameObject tutorielMenu;

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
        if (PlayerPrefs.GetInt("firstTime") == 0)
        {
            tutoriel();
            PlayerPrefs.SetInt("firstTime",1);
        }

        doubleTap = false;
        addition = false;

        musicPlayer = GameObject.Find("Music Manager");

        if (PlayerPrefs.GetInt("sound") == 0)
        {
            //sound on
            parameterMenu.transform.GetChild(2).GetComponent<Image>().sprite = IconSprites[0];
            if(!musicPlayer.GetComponent<AudioSource>().isPlaying) musicPlayer.GetComponent<AudioSource>().Play();
        }
        else
        {
            //sound off
            parameterMenu.transform.GetChild(2).GetComponent<Image>().sprite = IconSprites[1];
            //ONOFF.GetComponent<Text>().text = "OFF";
            if (musicPlayer.GetComponent<AudioSource>().isPlaying) musicPlayer.GetComponent<AudioSource>().Pause();
        }
        highScoreText.GetComponent<Text>().text = "  Best Score\n        : " + PlayerPrefs.GetInt("Score");
        play = false;
    
    }

    // Update is called once per frame
    void Update()
    {
        if (!play)
        {
            startMenu.transform.GetChild(0).GetComponent<RectTransform>().transform.Rotate(0, 0, 50 * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && play)
        {
            PauseGame();
        }

        if((int) controllerScript.timeLeft == 1) StopAllCoroutines();
        if (controllerScript.timeLeft <= 0)
        {
            finishedMenu.SetActive(true);
            finishedMenu.transform.GetChild(4).GetComponent<Text>().text = "x "+ PlayerPrefs.GetInt("Score");
            finishedMenu.transform.GetChild(5).GetComponent<Text>().text = "x " + Score.score;
        }
    }

    public void StartGame()
    {
        play = true;
        movementScript.can = true;
        controllerScript.startedPlaying = true;
        startDark.SetActive(false);
        StartCoroutine(controllerScript.Spawn());
    }

    public void PauseGame()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            GetComponent<AudioSource>().Play();
        }
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
        if (PlayerPrefs.GetInt("score") < Score.score) PlayerPrefs.SetInt("score", Score.score);
        Application.Quit();
    }

    //about us button clicked
    public void AboutUs()
    {
        StartCoroutine(creditAnim(creditMenu.activeSelf));
    }

    IEnumerator creditAnim(bool state)
    {
        creditButton.GetComponent<Button>().enabled = false;
        if (state)
        {
            for (int i = 9; i > 0; i--)
            {
                creditButton.transform.Rotate(0, 0, -10f);
                for (int j = 2; j > -1; j--)
                {
                    Vector3 a = new Vector3(creditMenu.transform.GetChild(j).transform.localPosition.x,
                        creditMenu.transform.GetChild(j).transform.localPosition.y,
                        creditMenu.transform.GetChild(j).transform.localPosition.z);

                    creditMenu.transform.GetChild(j).transform.localPosition =
                        new Vector3(a.x, a.y - (i * (2.75f + j * 2.75f)), a.z);
                    yield return new WaitForSeconds(0.01f);
                }
            }
            creditMenu.SetActive(!state);

        }
        else
        {
            creditMenu.SetActive(!state);
            for (int i = 1; i < 10; i++)
            {
                creditButton.transform.Rotate(0, 0, 10f);
                for (int j = 0; j < 3; j++)
                {
                    Vector3 a = new Vector3(creditMenu.transform.GetChild(j).transform.localPosition.x,
                        creditMenu.transform.GetChild(j).transform.localPosition.y,
                        creditMenu.transform.GetChild(j).transform.localPosition.z);

                    creditMenu.transform.GetChild(j).transform.localPosition =
                        new Vector3(a.x, a.y + (i * (2.75f + j * 2.75f)), a.z);
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }
        creditButton.GetComponent<Button>().enabled = true;
    }

    //if option button clicked
    public void optionClicked()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            GetComponent<AudioSource>().Play();
        }
        StartCoroutine(paramAnim(parameterMenu.activeSelf));
    }

    IEnumerator paramAnim(bool state)
    {
        paramButton.GetComponent<Button>().enabled = false;
        if (state)
        {
            for (int i = 9; i > 0; i--)
            {
                paramButton.transform.Rotate(0,0,10f);
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
                paramButton.transform.Rotate(0, 0, -10f);
                for (int j = 0; j < 3; j++)
                {
                    Vector3 a = new Vector3(parameterMenu.transform.GetChild(j).transform.localPosition.x,
                        parameterMenu.transform.GetChild(j).transform.localPosition.y,
                        parameterMenu.transform.GetChild(j).transform.localPosition.z);

                    parameterMenu.transform.GetChild(j).transform.localPosition =
                        new Vector3(a.x, a.y + (i * (2.75f + j * 2.75f)), a.z);
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }
        paramButton.GetComponent<Button>().enabled = true;
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
            if(!musicPlayer.GetComponent<AudioSource>().isPlaying) musicPlayer.GetComponent<AudioSource>().Play();
        }
    }

    public void RestartGame()
    {
        if(PlayerPrefs.GetInt("score") < Score.score) PlayerPrefs.SetInt("score",Score.score);
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

    public void pageVisit()
    {
        Application.OpenURL("https://www.facebook.com/JetLightstudio/");
    }
    public void siteVisit()
    {
        Application.OpenURL("https://www.jetlight-studio.tk/");
    }

    public void creditScene()
    {
        SceneManager.LoadScene("Credit");
    }

    public void tutoriel()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            GetComponent<AudioSource>().Play();
        }
        startDark.transform.GetChild(0).gameObject.SetActive(!startMenu.activeSelf);
        startDark.transform.GetChild(1).gameObject.SetActive(!startMenu.activeSelf);
    }

    public void tutorielRight()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            GetComponent<AudioSource>().Play();
        }
        float value = startDark.transform.GetChild(1).GetComponentInChildren<Scrollbar>().value;
        value += (float)1 / startDark.transform.GetChild(1).GetComponentInChildren<Scrollbar>().numberOfSteps;
        startDark.transform.GetChild(1).GetComponentInChildren<Scrollbar>().value = value;
    }
    public void tutorielLeft()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            GetComponent<AudioSource>().Play();
        }
        float value = startDark.transform.GetChild(1).GetComponentInChildren<Scrollbar>().value;
        value -= (float)1 / startDark.transform.GetChild(1).GetComponentInChildren<Scrollbar>().numberOfSteps;
        startDark.transform.GetChild(1).GetComponentInChildren<Scrollbar>().value = value;
    }

    public void rateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.Oussama.BallFall");
    }
}