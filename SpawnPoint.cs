using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject player;
    public GameObject parent;
    private GameObject spawn;
    public bool hasSpawned = false;

    private bool hasInstantiated = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawn = parent.GetComponent<SpawnEnemies>().enemy;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= 200 && !hasInstantiated)
        {
            hasInstantiated = true;
            Instantiate(spawn, transform.position, transform.rotation);
        }
    }
}
