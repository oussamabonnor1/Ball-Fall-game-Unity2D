using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Anime : MonoBehaviour {
	[Range(.1f,2f)]
	public float fly = 0f;

    Vector2 move;
    public Text title;
    public Sprite[] images;
    public Text tutoriel;
    public Text start;
    int imageId= 0;
    bool direction;

    private void Start()
    {
        direction = true;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (direction) {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, 5, 0), Time.fixedDeltaTime * 0.5f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, -5, 0), Time.fixedDeltaTime);
        }
        start.GetComponent<Text>().color= new Color(transform.position.y , 255 - transform.position.y, transform.position.y + 100);
        title.GetComponent<Text>().color = tutoriel.GetComponent<Text>().color;
    }

    private void Update()
    {
        if (transform.position.y >= 3f)
        {
            direction = false;
            
        }
        if(transform.position.y <= -0.5f)
        {
            direction = true;
            gameObject.GetComponent<AudioSource>().Play();
            imageId++;
            if (imageId > 5) imageId = 0;
            GetComponent<SpriteRenderer>().sprite = images[imageId];
            tuto(imageId);
        }
    }

    void tuto(int num)
    {
        switch (num)
        {
            case 0:
                tutoriel.GetComponent<Text>().text = "SCORE +2";
                tutoriel.GetComponent<Text>().color = new Color32(255, 135, 0, 255);
                break;
            case 1://
                tutoriel.GetComponent<Text>().text = "TIME +5";
                 tutoriel.GetComponent<Text>().color = new Color(0,213,255,1);
                break;
            case 2:
                tutoriel.GetComponent<Text>().text = "TIME -5";
                tutoriel.GetComponent<Text>().color = new Color32(209,0,255,255);
                break;
            case 3://
                tutoriel.GetComponent<Text>().text = "SCORE +5";
                 tutoriel.GetComponent<Text>().color = new Color(0,213,0,1);
                break;
            case 4:
                tutoriel.GetComponent<Text>().text = "DARK SCREEN 2 SEC";
                tutoriel.GetComponent<Text>().color = Color.black;
                break;
            case 5:
                tutoriel.GetComponent<Text>().text = "SCORE -3";
                tutoriel.GetComponent<Text>().color = new Color32(42,40,53,255);
                break;
            case 6:

                break;
            

        }
    }

    public void loading()
    {
        SceneManager.LoadScene("Main");
    }
}
