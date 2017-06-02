using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Starter : MonoBehaviour {
    public GameObject Dark;
    Image d;
	void Start () {
        d = Dark.GetComponent<Image>();
        d.color = new Color(d.color.r, d.color.g, d.color.b,255);
        d.CrossFadeColor(new Color(d.color.r, d.color.g, d.color.b, 0), 5f, true, true);
        
    }
  
}
