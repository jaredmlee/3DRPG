using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightCrawlerSpawner : MonoBehaviour
{
    public GameObject nightCrawler;
    public DayNight dayNight;

    private GameObject currCrawler;
    private bool spawned = false;
    // Start is called before the first frame update
    void Start()
    {
        dayNight = GameObject.FindGameObjectWithTag("DayNight").GetComponent<DayNight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dayNight.changedToNight && !spawned)
        {
            currCrawler =  Instantiate(nightCrawler, transform.position, transform.rotation);
            spawned = true;
        }
        if (dayNight.changedToDay)
        {
            spawned = false;
            Destroy(currCrawler);
        }
    }
}
