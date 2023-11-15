using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public Material day;
    public Material night;
    public Material dusk;
    public Material dawn;
    public Light Sun;
    public float dayLength = 0.01f;
    public bool changedToDay = false;
    public bool changedToNight = false;

    private float nextChangeTime = 250f;
    private bool isDay = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextChangeTime)
        {
            nextChangeTime = Time.time + 1f / dayLength;
            if (isDay)
            {
                changeToNight();
            }
            else
            {
                changeToDay();
            }
        }
    }
    public void changeToDay()
    {
        changedToDay = true;
        StartCoroutine(changeLightDay());
        isDay = true;
    }
    public void changeToNight()
    {
        changedToNight = true;
        isDay = false;
        StartCoroutine(changeLightNight());
    }

    private IEnumerator changeLightNight()
    {
        Sun.intensity = 0.8f;
        yield return new WaitForSeconds(2);
        Sun.intensity = 0.7f;
        RenderSettings.skybox = dusk;
        yield return new WaitForSeconds(2);
        Sun.intensity = 0.6f;
        yield return new WaitForSeconds(2);
        Sun.intensity = 0.5f;
        yield return new WaitForSeconds(2);
        Sun.intensity = 0.4f;
        RenderSettings.skybox = night;
        yield return new WaitForSeconds(2);
        Sun.intensity = 0.3f;
        changedToNight = false;
    }
    private IEnumerator changeLightDay()
    {
        Sun.intensity = 0.4f;
        yield return new WaitForSeconds(2);
        Sun.intensity = 0.5f;
        RenderSettings.skybox = dawn;
        yield return new WaitForSeconds(2);
        Sun.intensity = 0.6f;
        yield return new WaitForSeconds(2);
        Sun.intensity = 0.7f;
        yield return new WaitForSeconds(2);
        Sun.intensity = 0.8f;
        RenderSettings.skybox = day;
        yield return new WaitForSeconds(2);
        Sun.intensity = 0.9f;
        changedToDay = false;
    }
}
