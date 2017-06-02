using UnityEngine;
using System.Collections;
public class ManageMusic : MonoBehaviour
{
    public static ManageMusic Instance;


    // Use this for initialization
    void Start()
    {
        if(PlayerPrefs.GetInt("sound") == 0)
        {
            GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void Awake()
    {

        if (Instance)
            DestroyImmediate(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }
}