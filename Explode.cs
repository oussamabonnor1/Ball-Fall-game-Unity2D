using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {
    public GameObject explosion;
    public ParticleSystem[] effects;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "hoop")
        {
            exploding();
        }
    }

    void exploding()
    {
        Debug.Log(PlayerPrefs.GetInt("sound"));
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            Debug.Log("ththhthhth");

            GetComponent<AudioSource>().Play();

        }

        Instantiate(explosion, transform.position, transform.rotation);

            foreach (var effect in effects)
            {
                effect.transform.SetParent(null);
                effect.Stop();
                Destroy(effect.gameObject, 1.0f);
            }

            Destroy(gameObject);

        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        exploding();
    }

    }
