using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animal : MonoBehaviour
{
    public Animator animator;
    public GameObject drop;
    public Rigidbody rb;
    public GameObject player;

    public float rotationSpeed = 5f;
    public bool isAlive = true;
    public int maxHealth = 60;
    public int currentHealth = 60;
    public float runSpeed = 2f;
    public float walkSpeed = 2f;
    public float moveCoolDown = 1f;

    private float nextMoveTime = 0f;
    private bool beenHit = false;
    private bool canWalk = false;
    private bool isRotating = false;
    private int rand;
    private Quaternion targetRotation;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
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
            rand = Random.Range(0, 3);
            if (rand == 2)
            {
                animator.SetTrigger("eat");
                nextMoveTime = Time.time + 1f / moveCoolDown;
            }
            else
            {
                canWalk = true;
                animator.SetBool("walk", true);
                StartCoroutine(walk());
                nextMoveTime = Time.time + 1f / moveCoolDown;
            }
        }
        if (beenHit)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0f;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction * -1);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            Vector3 backwordMovement = transform.forward * runSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + backwordMovement);
        }
    }
    public void takeDamage(int damage)
    {
        beenHit = true;
        currentHealth -= damage;
        animator.SetTrigger("hurt");

        if (currentHealth <= 0)
        {
            die();
            return;
        }
        StartCoroutine(runAway());
    }
    public void die()
    {
        GetComponent<DestroyWhenDead>().DestroyObj();
        Instantiate(drop, transform.position, transform.rotation);
        isAlive = false;
        animator.SetBool("isDead", true);
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        this.enabled = false;
    }

    private IEnumerator runAway()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("run", true);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("run", false);
        beenHit = false;
    }

    private IEnumerator walk()
    {
        yield return  new WaitForSeconds(1.5f);
        canWalk = false;
        animator.SetBool("walk", false);
    }
}
