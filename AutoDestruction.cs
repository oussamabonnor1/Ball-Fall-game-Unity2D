using UnityEngine;
using System.Collections;

public class AutoDestruction : MonoBehaviour {
    public float lifeTime;
    public GameObject bombSFX;
    public AudioClip boom;

    // Use this for initialization
    void Start () {

	    bombSFX = GameObject.Find("SFX Bomb");
	    bombSFX.GetComponent<AudioSource>().clip = boom;
	    bombSFX.GetComponent<AudioSource>().Play();
        Destroy(gameObject, lifeTime);
	}
	
	
}
