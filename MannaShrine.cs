using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannaShrine : MonoBehaviour
{
    private bool triggered = false;
    public string currScene;
    public GameObject Tutorial;
    public GameObject Confirmation;
    public Transform newSpawn;
    public Respawn respawn;
    // Start is called before the first frame update
    void Start()
    {
        respawn = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Respawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && triggered)
        {
            Tutorial.SetActive(false);
            Confirmation.SetActive(true);
            respawn.spawnPoint.position = newSpawn.position;
            respawn.spawnScene = currScene;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            triggered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Confirmation.SetActive(false);
            Tutorial.SetActive(true);
            triggered = false;
        }
    }
}
