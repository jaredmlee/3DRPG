using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnterCave : MonoBehaviour
{
    public Light directionalLight;
    public float changeLight = 0.2f;
    public float ogLight = 0.9f;
    public string newSong = "Cave";
    public string oldSong = "DuinshireTheme";
    public bool changeLightSource = true;

    public bool inCave = false;
    // Start is called before the first frame update
    void Start()
    {
        directionalLight = GameObject.FindGameObjectWithTag("direrectionalLight").GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && !inCave)
        {
            change();
        }
        else if (other.gameObject.name == "Player")
        {
            changeBack();
        }
    }

    public void change()
    {
        FindObjectOfType<AudioManager>().stopPlaying(oldSong);
        FindObjectOfType<AudioManager>().play(newSong);
        inCave = true;
        if (changeLightSource)
        {
            RenderSettings.ambientMode = AmbientMode.Flat;
            RenderSettings.ambientLight = Color.black;
        }
        directionalLight.intensity = changeLight;
    }
    public void changeBack()
    {
        FindObjectOfType<AudioManager>().stopPlaying(newSong);
        FindObjectOfType<AudioManager>().play(oldSong);
        inCave = false;
        if (changeLightSource)
        {
            RenderSettings.ambientMode = AmbientMode.Skybox;
        }
        directionalLight.intensity = ogLight;
    }
}
