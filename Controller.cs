using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {
    public GameObject[] balls;
    public GameObject GameOver;
    public GameObject restartButton;
    public GameObject shareButton;
    public GameObject highScoreText;
    public GameObject startButton;
    public GameObject pauseButton;
    public GameObject ReturnToMainMenu;
    public GameObject optionButton;
    public GameObject aboutButton;
    public GameObject quitButton;
    public GameObject startDark;
    public GameObject quitMenu;
    public GameObject aboutUsText;
    public GameObject optionText;
    public GameObject ONOFF;
    public GameObject bestScoreText;
    public GameObject musicPlayer;
    public GameObject badView;
    public GameObject tutoriel;

    public Movement m;
    public Text timer;
    public Camera cam1;

    float maxWidth;
    public float timeLeft;

    private bool play;
    bool addition;
    bool doubleTap;

    // Use this for initialization
    void Start() {

        doubleTap = false;

        addition = false;
        ONOFF.GetComponent<Text>().text = "ON";
        highScoreText.GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("Score");
        play = false;
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam1.ScreenToWorldPoint(upperCorner);
        float ballWidth = (balls[0].GetComponent<Renderer>().bounds.extents.x) / 2f;
        maxWidth = targetWidth.x - ballWidth;
       
    }

    void FixedUpdate()
    {

        if (play)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timer.text = "Time's UP !!!";
                pauseButton.SetActive(false);
            }
            else timer.text = "Time Left: " + Mathf.RoundToInt(timeLeft);
        }

    }

    void UpDate()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (doubleTap == false)
            {
                doubleTap = true;
                StartCoroutine(quit());

            }
            else
            {
                Application.Quit();
            }
        }
    }

    IEnumerator quit()
    {
        yield return new WaitForSeconds(0.5f);
        doubleTap = false;
    }

    IEnumerator Spawn()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.0f);

        while (timeLeft > 0)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-maxWidth, maxWidth), transform.position.y, 0.0f);
            Quaternion spawnRotation = Quaternion.identity;
            GameObject ball = balls[Random.Range(0, balls.Length)];
            Instantiate(ball, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(Random.Range(0.5f, 0.8f));
        }

        restartButton.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.1f);
        GameOver.SetActive(true);
        restartButton.SetActive(true);
        //shareButton.SetActive(true);
        bestScoreText.SetActive(true);

    }

    public void StartGame()
    {
        play = true;
        m.toogle(play);
        startButton.SetActive(false);
        highScoreText.SetActive(false);
        pauseButton.SetActive(true);
        aboutButton.SetActive(true);
        quitButton.SetActive(false);
        startDark.SetActive(false);

        StartCoroutine(Spawn());
    }

    public void PauseGame()
    {
        addition = true;
        play = !play;
        quitButton.SetActive(!play);
        ReturnToMainMenu.SetActive(!play);
        m.toogle(play);
        if (play == false)
        {
            Time.timeScale = 0;
            StopAllCoroutines();
        }
        else
        {
            Time.timeScale = 1;
            StartCoroutine(Spawn());
        }
    }

    //if we press Quit button
    public void QuitClicked()
    {
        quitMenu.SetActive(true);
        startButton.SetActive(false);
        highScoreText.SetActive(false);
        aboutButton.SetActive(false);
        quitButton.SetActive(false);
        optionButton.SetActive(false);
        ReturnToMainMenu.SetActive(false);
    }

    //if we press No (quit) button
    public void No()
    {
        quitMenu.SetActive(false);
        startButton.SetActive(true);
        highScoreText.SetActive(true);
        aboutButton.SetActive(true);
        quitButton.SetActive(true);
        optionButton.SetActive(true);
        if (addition == true) ReturnToMainMenu.SetActive(true);
    }
    // if yes (quit) button 
    public void QuitGame()
    {
        Application.Quit();
    }

    //about us button clicked
    public void AboutUs()
    {
        aboutUsText.SetActive(true);
        startButton.SetActive(false);
        highScoreText.SetActive(false);
        quitButton.SetActive(false);
        aboutButton.SetActive(false);
        optionButton.SetActive(false);
    }
    //if return (about us) button is pressed
    public void Returning()
    {
        aboutUsText.SetActive(false);
        startButton.SetActive(true);
        highScoreText.SetActive(true);
        quitButton.SetActive(true);
        aboutButton.SetActive(true);
        optionButton.SetActive(true);
    }

    //if option button clicked
    public void optionClicked()
    {
        optionText.SetActive(true);
        startButton.SetActive(false);
        highScoreText.SetActive(false);
        quitButton.SetActive(false);
        aboutButton.SetActive(false);
        optionButton.SetActive(false);
    }

    //if return (option) clicked
    public void optionReturn()
    {
        optionText.SetActive(false);
        startButton.SetActive(true);
        highScoreText.SetActive(true);
        quitButton.SetActive(true);
        aboutButton.SetActive(true);
        optionButton.SetActive(true);
    }

    //if On/OFF clicked
    public void OnOff()
    {
        if (ONOFF.GetComponent<Text>().text == "ON")
        {
            ONOFF.GetComponent<Text>().text = "OFF";
            musicPlayer.GetComponent<AudioSource>().Pause();
        }
        else { ONOFF.GetComponent<Text>().text = "ON";

            musicPlayer.GetComponent<AudioSource>().UnPause();
        }
    }

    public void RestartGame()
    {
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
   
}
