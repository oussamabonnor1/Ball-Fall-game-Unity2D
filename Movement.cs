using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Movement : MonoBehaviour {
    public Camera cam;
    //public Slider slider;
    float maxWidth;
    private bool reverse;
    public  bool can;
    Vector3 bounds;

    // Use this for initialization
    void Start () {
        bounds = new Vector3(Screen.width, 0.0f, 0.0f);
       // SliderPos = cam.ScreenToWorldPoint(bounds);
        
        can = false;
        reverse = false;
	    if(cam == null)
        {
            cam = Camera.main;
        }
        Vector3 edge = new Vector3(Screen.width,Screen.height,0.0f);
        Vector3 target = cam.ScreenToWorldPoint(edge);
        float hoopWidth = (GetComponent<Renderer>().bounds.extents.x) / 2f;
        maxWidth = target.x - hoopWidth;
	}

    void FixedUpdate()
    {
        if (can)
        {
            Vector3 newPosition;
             Vector3 rawPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            // Vector3 rawPosition = new Vector3(slider.value * SliderPos.x, transform.position.y);
            if (reverse)
            {
                newPosition = new Vector3(-rawPosition.x, transform.position.y, 0.0f);
            }
            else
            {
                newPosition = new Vector3(rawPosition.x, transform.position.y, 0.0f);
            }

            float target = Mathf.Clamp(newPosition.x, -maxWidth, maxWidth);

            transform.position = Vector3.Lerp(transform.position,new Vector3(target, transform.position.y, 0.0f),Time.fixedDeltaTime * 5);
           
        }
    }
   
    public void toogle(bool other)
    {
        can = other;
    }

    public void Reversing(bool other)
    {
        reverse = other;
    }
}
