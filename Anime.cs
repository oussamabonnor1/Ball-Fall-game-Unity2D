using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Anime : MonoBehaviour {

    public void facebook()
    {
        Application.OpenURL("https://www.facebook.com/JetLightstudio/");
    }
    public void site()
    {
        Application.OpenURL("https://www.jetlight-studio.tk/");
    }
    public void googlePlus()
    {
        Application.OpenURL("https://plus.google.com/106078600308560786022");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
