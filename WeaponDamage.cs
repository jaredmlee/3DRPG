using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public GameObject player;
    public string weaponType; //must be sharp, blunt
    public int weaponDamage = 20;

    private bool multpily2 = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (weaponType == "sharp" && other.gameObject.CompareTag("flesh"))
        {
            weaponDamage *= 2;
            multpily2 = true;
        }
        else if (weaponType == "blunt" && other.gameObject.CompareTag("armored"))
        {
            weaponDamage *= 2;
            multpily2 = true;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") && player.GetComponent<PlayerScript>().canDealDamage)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if(player.GetComponent<PlayerScript>().spinAttacking)
            {
                enemy.takeDamage(weaponDamage + 15);
            }
            if (enemy.parryStrength <= 0 ) 
            {
                enemy.takeDamage(weaponDamage);
                enemy.parryStrength = enemy.maxParryStrength;
            }
            else
            {
                enemy.parry();
            }
            player.GetComponent<PlayerScript>().canDealDamage = false;
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Bear") && player.GetComponent<PlayerScript>().canDealDamage)
        {
            Bear bear = other.gameObject.GetComponent<Bear>();
            bear.takeDamage(weaponDamage);
            player.GetComponent<PlayerScript>().canDealDamage = false;
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Animal") && player.GetComponent<PlayerScript>().canDealDamage)
        {
            animal animal = other.gameObject.GetComponent<animal>();
            animal.takeDamage(weaponDamage);
            player.GetComponent<PlayerScript>().canDealDamage = false;
        }
        if (multpily2)
        {
            weaponDamage /= 2;
            multpily2 = false;
        }
    }

}
