using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponDamage : MonoBehaviour
{
    public int weaponDamage = 20;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && enemy.GetComponent<Enemy>().canDealDamage)
        {
            PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
            if (player != null)
            {
                if (!player.isParrying || enemy.GetComponent<Enemy>().doingSpecial)
                {
                    player.takeDamage(weaponDamage);
                }
                else
                {
                    if (player.stunOpponent)
                    {
                        enemy.GetComponent<Enemy>().stunEnemy();
                    }
                    player.parryStrength -= 1;
                    player.animator.SetTrigger("blockHit");
                }
                enemy.GetComponent<Enemy>().canDealDamage = false;
            }
        }
    }
}
