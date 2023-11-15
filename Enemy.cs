using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthBar healthBar;
    public HealthBar parryBar;
    public Animator animator;
    public GameObject hitBox;
    public GameObject player;
    public GameObject drop;
    public GameObject warning;
    public Rigidbody rb;

    public bool wander = true;
    public float hitBoxDelay = 0f;
    public float walkSpeed = 2f;
    public int maxHealth = 100;
    public int currentHealth = 100;
    public bool isAlive = true;
    public int parryStrength = 5;
    public float ParryRestoreRate = 0.2f;
    public float attackCoolDown = 0.3f;
    public float moveCoolDown = 0.2f;
    public int attackDamage = 15;
    public float attackRange = 2f;
    public bool canDealDamage = false;
    public bool stunned = false;
    public float rotationSpeed = 5f;
    public int inverseRotation = 1;
    public int attacksTilSpecial = 3;
    public bool doingSpecial = false;

    private float nextMoveTime = 0f;
    public bool canWalk = false;
    private bool isRotating = false;
    private int rand;
    private int specialCount = 0;
    private float nextRestoreTime = 0f;
    private float nextAttackTime = 0f;
    private Quaternion targetRotation;

    public int maxParryStrength = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        parryStrength = maxParryStrength;
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        parryBar.setMaxHealth(maxParryStrength);
        
    }

    // Update is called once per frame
    void Update()
    { 

        //set bars
        healthBar.SetHealth(currentHealth);
        parryBar.SetHealth(parryStrength);

        //parry strength update
        if (parryStrength < 0)
        {
            parryStrength = 0;
        }
        if (parryStrength < maxParryStrength && !stunned)
        {
            if (Time.time >= nextRestoreTime)
            {
                parryStrength += 1;
                nextRestoreTime = Time.time + 1f / ParryRestoreRate;
            }
        }

        //attack
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (Time.time >= nextAttackTime && distance < 2.5)
        {
            canDealDamage = true;
            if (specialCount < attacksTilSpecial)
            {
                animator.SetTrigger("attack");
                specialCount++;
            }
            else
            {
                warning.SetActive(true);
                doingSpecial = true;
                StartCoroutine(disableWarning());
                animator.SetTrigger("specialAttack");
                specialCount = 0;
            }
            attack();
            nextAttackTime = Time.time + 1f / attackCoolDown;
        }

        //look at player
        if (distance < GetComponent<FollowPlayer>().followRange)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0f;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction * inverseRotation);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        //wander
        else if (wander)
        {
            if (canWalk)
            {
                if (!isRotating && rand == 1)
                {
                    targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 45f, 0f));
                    isRotating = true;

                }
                if (isRotating)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                    if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
                    {
                        transform.rotation = targetRotation;
                        isRotating = false;
                    }
                }
                Vector3 move = transform.forward * walkSpeed * Time.deltaTime;
                rb.MovePosition(rb.position + move);
            }
            if (Time.time >= nextMoveTime)
            {
                rand = Random.Range(0, 2);
                canWalk = true;
                animator.SetBool("walk", true);
                StartCoroutine(walk());
                nextMoveTime = Time.time + 1f / moveCoolDown;
            }
        }


            
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("hurt");

        if (currentHealth <= 0)
        {
            die();
        }
        healthBar.SetHealth(currentHealth);
    }

    public void die()
    {
        Instantiate(drop, transform.position, transform.rotation);
        isAlive = false;
        animator.SetBool("isDead", true);
        GetComponent<DestroyWhenDead>().DestroyObj();
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<FollowPlayer>().enabled = false;
        this.enabled = false;
    }

    public void parry()
    {
        animator.SetTrigger("parry");
        parryStrength -= 1;
    }

    public void attack()
    {
        if (hitBoxDelay == 0f)
        {
            hitBox.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            StartCoroutine(delayedAttack());
        }
    }

    public void disableCollider()
    {
        hitBox.GetComponent<BoxCollider>().enabled = false;
        if (doingSpecial)
        {
            doingSpecial = false;
        }
    }

    public void stunEnemy()
    {
        parryStrength = 0;
        stunned = true;
        animator.SetTrigger("stunned");
        StartCoroutine(stun());
    }

    private IEnumerator stun()
    {
        yield return new WaitForSeconds(2f);
        stunned = false;
    }

    private IEnumerator disableWarning()
    {
        yield return new WaitForSeconds(1.5f);
        doingSpecial = false;
        warning.SetActive(false);
    }
    private IEnumerator walk()
    {
        yield return new WaitForSeconds(1.5f);
        canWalk = false;
        animator.SetBool("walk", false);
    }
    private IEnumerator delayedAttack()
    {
        yield return new WaitForSeconds(hitBoxDelay);
        hitBox.GetComponent<BoxCollider>().enabled = true;
    }
}
