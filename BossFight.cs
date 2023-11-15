using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFight : MonoBehaviour
{
    public GameObject bossBar;
    public GameObject boss;
    public string enemyName;

    public Text bossName;

    private Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        bossBar = GameObject.FindGameObjectWithTag("BossBar").transform.GetChild(0).gameObject;
        bossName = GameObject.FindGameObjectWithTag("BossName").GetComponent<Text>();
        enemy = boss.GetComponent<Enemy>();
        bossName.text = enemyName;
        enemy.healthBar = bossBar.GetComponent<HealthBar>();
        enemy.healthBar.setMaxHealth(enemy.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy.isAlive)
        {
            bossBar.SetActive(false);
            bossName.gameObject.GetComponent<Text>().enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            bossBar.SetActive(true);
            bossName.gameObject.GetComponent<Text>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            bossBar.SetActive(false);
            bossName.gameObject.GetComponent<Text>().enabled = false;
        }
    }
}
