using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject replacee;
    public DayNight dayNight;

    private bool spawned = false;
    // Start is called before the first frame update
    void Start()
    {
        dayNight = GameObject.FindGameObjectWithTag("DayNight").GetComponent<DayNight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dayNight.changedToDay && !spawned && replacee == null)
        {
            replacee = Instantiate(prefab, transform.position, transform.rotation);
            spawned = true;
        }
        if (dayNight.changedToNight)
        {
            spawned = false;
        }
    }
}
