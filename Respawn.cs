using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.SceneStreamer;

public class Respawn : MonoBehaviour
{
    public PlayerScript player;
    public Transform spawnPoint;
    public GameObject gameOverScreen;
    public GameObject loading;
    public string spawnScene = "Scene9";

    private bool canSetScreen = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isAlive && canSetScreen)
        {
            canSetScreen = false;
            StartCoroutine(setGameOverScreen());
        }
    }

    public void respawn()
    {
        StartCoroutine(doRespawn());
        
    }

    IEnumerator setGameOverScreen()
    {
        yield return new WaitForSeconds(2f);
        gameOverScreen.SetActive(true);
    }
    IEnumerator doRespawn()
    {
        loading.SetActive(true);
        SceneStreamer.SetCurrentScene(spawnScene);
        player.enabled = true;
        player.currentHealth = player.maxHealth;
        player.isAlive = true;
        player.animator.SetBool("isDead", false);
        player.GetComponent<Collider>().enabled = true;
        player.transform.position = spawnPoint.transform.position;
        yield return new WaitForSeconds(0.4f);
        loading.SetActive(false);
        gameOverScreen.SetActive(false);
        canSetScreen = true;
    }
}
