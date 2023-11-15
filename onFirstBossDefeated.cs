using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onFirstBossDefeated : MonoBehaviour
{
    public EventTracker eventTracker;
    public Enemy eliteSkeleton;
    public GameObject wall;
    public GameObject wall2;

    private bool deactivedWalls;
    // Start is called before the first frame update
    void Start()
    {
        eventTracker = GameObject.FindGameObjectWithTag("EventTracker").GetComponent<EventTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (eventTracker.EliteSkeltonDefeated && !deactivedWalls)
        {
            wall.SetActive(false);
            wall2.SetActive(false);
            deactivedWalls = true;
        }
        if (!eliteSkeleton.isAlive)
        {
            eventTracker.EliteSkeltonDefeated = true;
        }
    }
}
