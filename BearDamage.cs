using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearDamage : MonoBehaviour
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

        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && enemy.GetComponent<Bear>().canDealDamage)
        {
            PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
            if (player != null)
            {
                if (!player.isParrying)
                {
                    player.takeDamage(weaponDamage);
                }
                else
                {
                    player.parryStrength -= 1;
                    player.animator.SetBool("blockHit", true);
                }
                enemy.GetComponent<Bear>().canDealDamage = false;
            }
        }
    }
}
