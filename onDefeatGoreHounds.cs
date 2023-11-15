using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onDefeatGoreHounds : MonoBehaviour
{
    public EventTracker ev;
    public Enemy goreHound;
    public GameObject dialogue;
    public GameObject gem;
    public Transform player;

    private bool gotGem;
    // Start is called before the first frame update
    void Start()
    {
        ev = GameObject.FindGameObjectWithTag("EventTracker").GetComponent<EventTracker>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!goreHound.isAlive)
        {
            ev.LakeGoreHoundsDefeated = true;
        }
        if (ev.LakeGoreHoundsDefeated)
        {
            dialogue.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player" && !gotGem && ev.LakeGoreHoundsDefeated)
        {
            Debug.Log("HelloWorld");
            gotGem = true;
            Instantiate(gem, player.position, player.rotation);
        }
    }
}
