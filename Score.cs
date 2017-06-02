using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {
    int score;
    public int points;
    public Text Scorer;
    public GameObject noise;
     int bestScore;
    public Controller c;
    public Movement m;
    private bool reverse;

	// Use this for initialization
	void Start () {
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
            UpdateScore();
            MakeNoise();
        }else if(other.gameObject.tag == "bomb")
        {
            score -= points+1;
            UpdateScore();
        }
        else if (other.gameObject.tag == "blue ball")
        {
            if (c.timeLeft > 55) c.timeLeft = 60;
            else c.timeLeft += 5;
            MakeNoise();
        }
        else if (other.gameObject.tag == "black ball")
        {
            MakeNoise();
            c.badView.SetActive(true);
            StartCoroutine(badView());
        }
        else if (other.gameObject.tag == "reverse")
        {
            reverse = !reverse;
            m.Reversing(reverse);
            if (reverse) points *= 2;
            else points = 2;
        }
        else if (other.gameObject.tag == "purpul ball")
        {
            
            c.timeLeft -= 5;
            MakeNoise();
        }
        else if (other.gameObject.tag == "green ball")
        {

            score += (points * 2) +1 ;
            if (PlayerPrefs.GetInt("sound") == 0)
            {
                other.gameObject.GetComponent<AudioSource>().Play();
            }
            UpdateScore();
        }

    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if(other.gameObject.tag == "bomb")
        {
            score -= 2;
            UpdateScore();
        }
    }

    void UpdateScore()
    {
        if (score < 0) score = 0;
        Scorer.text = "Score: " + score;
    }

    void FixedUpdate()
    {
        if (c.timeLeft <= 0)
        {
            if (score > bestScore)
            {
                PlayerPrefs.SetInt("Score", score);
                bestScore = score;
            }
            c.bestScoreText.GetComponent<Text>().text = "SCORE: " + score+ "\n\nBEST SCORE: "+ bestScore;
        }
    }

    IEnumerator badView()
    {
        yield return new WaitForSeconds(2.0f);
        c.badView.SetActive(false);
    }

    void MakeNoise()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            noise.GetComponent<AudioSource>().Play();
        }
    }
}
