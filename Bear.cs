using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    public HealthBar healthBar;
    public Animator animator;
    public GameObject hitBox;
    public GameObject player;
    public GameObject drop;

    public float speed = 2f;
    public int maxHealth = 100;
    public int currentHealth = 100;
    public bool isAlive = true;
    public float attackCoolDown = 0.3f;
    public int attackDamage = 15;
    public float attackRange = 2f;
    public bool canDealDamage = false;
    public float rotationSpeed = 5f;
    public int inverseRotation = 1;
    public bool beenHit = false;

    private Rigidbody rb;
    private float nextAttackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        //set bars
        healthBar.SetHealth(currentHealth);


        //attack
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (Time.time >= nextAttackTime && distance < 2.5)
        {
            canDealDamage = true;
            animator.SetTrigger("Attack1");
            attack();
            nextAttackTime = Time.time + 1f / attackCoolDown;
        }

        //look at player
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction * inverseRotation);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        //run away when hit
        if (beenHit)
        {
            Vector3 backwordMovement = transform.forward * speed * Time.deltaTime;
            rb.MovePosition(rb.position + backwordMovement);
           
        }


    }

    public void takeDamage(int damage)
    {
        StartCoroutine(runAway());
        currentHealth -= damage;
        animator.SetTrigger("Get Hit Front");

        if (currentHealth <= 0)
        {
            die();
        }
        healthBar.SetHealth(currentHealth);

    }

    public void die()
    {
        Instantiate(drop, player.transform.position, player.transform.rotation);
        isAlive = false;
        animator.SetBool("Death", true);
        GetComponent<DestroyWhenDead>().DestroyObj();
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<FollowPlayer>().enabled = false;
        this.enabled = false;
    }

    public void attack()
    {
        StartCoroutine(delayAttack());
    }

    public void disableCollider()
    {
        hitBox.GetComponent<BoxCollider>().enabled = false;
    }

    //This is temp fix, please find a better way later;
    private IEnumerator delayAttack()
    {
        yield return new WaitForSeconds(0.5f);
        hitBox.GetComponent<BoxCollider>().enabled = true;
    }

    private IEnumerator runAway()
    {
        yield return new WaitForSeconds(0.5f);
        beenHit = true;
        inverseRotation = -1;
        animator.SetBool("Run Forward", true);
        GetComponent<FollowPlayer>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        inverseRotation = 1;
        beenHit = false;
        animator.SetBool("Run Forward", false);
        yield return new WaitForSeconds(1f);
        GetComponent<FollowPlayer>().enabled = true;
    }
}
