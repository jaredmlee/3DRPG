using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemy;
    public SpawnPoint[] spawnPoints;
    public  int numEnemies = 0;
    public int maxEnemies = 3;
    public GameObject spawnParent;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = spawnParent.GetComponentsInChildren<SpawnPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (numEnemies < maxEnemies)
        {
            spawnEnemy();
        }
    }

    public void spawnEnemy()
    {
        
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!spawnPoints[i].hasSpawned)
            {
                int rand = Random.Range(0, 3);
                if (rand == 1)
                {
                    //Instantiate(enemy, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
                    spawnPoints[i].hasSpawned = true;
                    numEnemies++;
                    break;
                }
            }
        }
    }
}
