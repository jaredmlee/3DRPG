using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkedToTallisChecker : MonoBehaviour
{
    public EventTracker ev;
    public  GameObject blockerDialogue;
    public GameObject blocker;

    private bool triggered;
    // Start is called before the first frame update
    void Start()
    {
        ev = GameObject.FindGameObjectWithTag("EventTracker").GetComponent<EventTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ev.TalkedtoTallis && !triggered)
        {
            Destroy(blockerDialogue);
            Destroy(blocker);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            ev.TalkedtoTallis = true;
        }
    }
}
