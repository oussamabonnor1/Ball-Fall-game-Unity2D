using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {
    public static int score;
    public int points;
    public GameObject noise;
    public GameObject bombNoise;
    public GameObject Scorer;
    public AudioClip[] goodBadVibe;
     int bestScore;
    public Controller controllerScript;
    public Movement m;
    public googlePlayScript uiManager;
    private bool reverse;

    private int taps;

	// Use this for initialization
	void Start ()
	{
	    taps = 0;
        reverse = false;
        score = 0;
        bestScore = PlayerPrefs.GetInt("Score");
        UpdateScore();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ball")
        {
            score += points;
            
            MakeNoise();
        }
        else if(other.gameObject.tag == "bomb")
        {
            score -= points+1;
        }
        else if (other.gameObject.tag == "blue ball")
        {
            if (controllerScript.timeLeft > 55) controllerScript.timeLeft = 60;
            else controllerScript.timeLeft += 5;
            MakeNoise();
        }
        else if (other.gameObject.tag == "black ball")
        {
            MakeNoise();
            controllerScript.badView.SetActive(true);
            StartCoroutine(badView());
        }
        else if (other.gameObject.tag == "reverse")
        {

            reverse = !reverse;
            m.Reversing(reverse);
            if (reverse)
            {
                points *= 2;
                bombNoise.GetComponent<AudioSource>().clip = goodBadVibe[0];
            }
            else
            {
                bombNoise.GetComponent<AudioSource>().clip = goodBadVibe[1];
                points = 2;
            }
            bombNoise.GetComponent<AudioSource>().Play();
        }
        else if (other.gameObject.tag == "purpul ball")
        {
            
            controllerScript.timeLeft -= 5;
            bombNoise.GetComponent<AudioSource>().clip = goodBadVibe[0];
            bombNoise.GetComponent<AudioSource>().Play();
        }
        else if (other.gameObject.tag == "green ball")
        {
            score += (points * 2) +1 ;
            MakeNoise();
            
        }
        UpdateScore();

    }

    void Update()
    {
        if (controllerScript.badView.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                taps++;
            }
        }
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.tag == "bomb")
        {
            score -= 2;
            UpdateScore();
            if (PlayerPrefs.GetInt("sound") == 0)
            {
                bombNoise.GetComponent<AudioSource>().clip = goodBadVibe[2];
                bombNoise.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("sound") == 0 && !GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }

    void UpdateScore()
    {
        if (score < 0) score = 0;
        Scorer.GetComponent<Text>().text = "x " + score;
    }

    void FixedUpdate()
    {
        if (controllerScript.timeLeft <= 0)
        {
            if (score > bestScore)
            {
                PlayerPrefs.SetInt("Score", score);
                bestScore = score;
                if (score > 100 && PlayerPrefs.GetInt("firstAchievement") == 0)
                {
                    uiManager.unlockAchievement(GPGSIds.achievement_score_100_for_the_first_time);
                    PlayerPrefs.SetInt("firstAchievement", 1);
                }
                if (score > 200 && PlayerPrefs.GetInt("secondAchievement") == 0)
                {
                    uiManager.unlockAchievement(GPGSIds.achievement_ball_fall_amateur);
                    PlayerPrefs.SetInt("secondAchievement", 1);
                }
                if (score > 300 && PlayerPrefs.GetInt("thirdAchievement") == 0)
                {
                    uiManager.unlockAchievement(GPGSIds.achievement_ball_fall_player_anyone);
                    PlayerPrefs.SetInt("thirdAchievement", 1);
                }
                if (score > 400 && PlayerPrefs.GetInt("fourthAchievement") == 0)
                {
                    uiManager.unlockAchievement(GPGSIds.achievement_wow_very_ball_much_fall);
                    PlayerPrefs.SetInt("fourthAchievement", 1);
                }
                if (score > 500 && PlayerPrefs.GetInt("fifthAchievement") == 0)
                {
                    uiManager.unlockAchievement(GPGSIds.achievement_balls_fall_at_your_feet_master);
                    PlayerPrefs.SetInt("fifthAchievement", 1);
                }
                if (score > 750 && PlayerPrefs.GetInt("sixthAchievement") == 0)
                {
                    uiManager.unlockAchievement(GPGSIds.achievement_we_did_not_think_it_was_possible);
                    PlayerPrefs.SetInt("sixthAchievement", 1);
                }
                uiManager.addScoreLeaderbord(GPGSIds.leaderboard_best_scores,bestScore);
            }
        }
    }

    IEnumerator badView()
    {
        m.can = false;
        do
        {
            yield return new WaitForSeconds(0.01f);
            controllerScript.badView.transform.GetChild(0).GetComponent<Text>().text = "Taps Lefts: " + (9 - taps);
        } while (taps < 9);
        controllerScript.badView.SetActive(false);
        taps = 0;
        m.can = true;
    }

    void MakeNoise()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            noise.GetComponent<AudioSource>().Play();
        }
    }
}
