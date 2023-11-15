using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator animator;
    public GameObject hitBox;
    public LayerMask enemyLayer;
    public HealthBar healthBar;
    public HealthBar parryBar;
    public HealthBar hungerBar;
    public DisableCollider dc;
    public GameObject starterSword;

    public bool spinAttacking = false;
    public bool canDealDamage = false;
    public float attackRange = 1.5f;
    public float speed = 6f;
    public float sprintSpeed = 11f;
    public float rollSpeed = 20f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public int attackDamage = 20;
    public float attackCoolDown = 1f;
    public float hungerTime = 1f;
    public int parryStrength = 5;
    public int maxHealth = 300;
    public int currentHealth = 300;
    public int maxHunger = 100;
    public int currentHunger = 100;
    public bool isAlive = true;
    public bool isParrying = false;
    public float ParryRestoreRate = 0.1f;
    public bool stunOpponent;
    public float stunDuration = 0.2f;

    public Transform groundCheck;
    public float groundDistance = 0.03f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private int attacknum = 0;
    private bool gettingDamaged;
    private bool rollDelaydone = false;
    private bool isRolling;
    private bool rollTriggered;
    private bool isSprinting;
    private float nextReduceHunger = 0f;
    private float nextAttackTime = 0f;
    private float nextRestoreTime = 0f;
    private int maxParryStrength = 5;


    //testing
    public GameObject parentOfWeapon;
    public GameObject newWeapon;

    private void Start()
    {
        parryStrength = maxParryStrength;
        currentHealth = maxHealth;
        currentHunger = maxHunger/2;

        healthBar.setMaxHealth(maxHealth);
        parryBar.setMaxHealth(maxParryStrength);
        hungerBar.setMaxHealth(maxHunger);

        Instantiate(starterSword, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        //testing 
        if (Input.GetKeyDown(KeyCode.M))
        {
            testing();
        }

        healthBar.SetHealth(currentHealth);
        parryBar.SetHealth(parryStrength);
        hungerBar.SetHealth(currentHunger);
        if (currentHealth <= 0)
        {
            die();
        }
        //MOVEMENT SCRIPT

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("Jump");
            if (velocity.y <= 0)
            {
                animator.SetTrigger("Fall");
            }
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);



            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            if (Input.GetKey(KeyCode.LeftShift) && currentHunger > 2)
            {
                animator.SetBool("Sprint", true);
                isSprinting = true;
                controller.Move(moveDir.normalized * sprintSpeed * Time.deltaTime);
            }
            else if (isRolling)
            {
                if (!rollTriggered)
                {
                    rollTriggered = true;
                    animator.SetTrigger("roll");
                }
                if (rollDelaydone)
                {
                    controller.Move(moveDir.normalized * rollSpeed * Time.deltaTime);
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isSprinting = false;
                animator.SetBool("Sprint", false);
            }
           
            else
            {
                animator.SetBool("Walk", true);
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }
        else if (isRolling)
        {
            if (!rollTriggered)
            {
                rollTriggered = true;
                animator.SetTrigger("rollBack");
            }
            if (rollDelaydone) {
                Vector3 moveDir = -transform.forward;
                controller.Move(moveDir.normalized * rollSpeed * Time.deltaTime);
            }
        }
        else
        {
            animator.SetBool("Sprint", false);
            animator.SetBool("Walk", false);
        }

        //END MOVEMENT SCRIPT

        //Attack
        if (Input.GetKeyDown(KeyCode.Q) && currentHunger >= 20)
        {
            if (Time.time >= nextAttackTime)
            {
                currentHunger -= 20;
                spinAttacking = true;
                StartCoroutine(stopSpinning());
                animator.SetTrigger("spinAttack");
                attack();
                nextAttackTime = Time.time + 1f / attackCoolDown;
            }
        }
        if (Input.GetMouseButtonDown(0) && !isParrying)
        {
            if (Time.time >= nextAttackTime)
            {
                if (attacknum == 0)
                {
                    animator.SetTrigger("attack");
                    attacknum++;
                }
                else if (attacknum == 1)
                {
                    animator.SetTrigger("attack1");
                    attacknum++;
                }
                else if (attacknum == 2)
                {
                    animator.SetTrigger("attack2");
                    attacknum = 0;
                }
                attack();
                nextAttackTime = Time.time + 1f / attackCoolDown;
            }
        }

        //PARRY 
        
        if (Input.GetMouseButton(1) && parryStrength > 0 && !gettingDamaged)
        {
            parry();
        }
        if (Input.GetMouseButtonUp(1))
        {
            stopParrying();
        }
        if (parryStrength < 0)
        {
            parryStrength = 0;
        }
        if (parryStrength < maxParryStrength)
        {
            if (Time.time >= nextRestoreTime)
            {
                parryStrength += 1;
                nextRestoreTime = Time.time + 1f / ParryRestoreRate;
            }
        }

        //ROLL

        if (Input.GetKeyDown(KeyCode.Z) && !isRolling){
            //animator.SetTrigger("roll");
            StartCoroutine(roll());
        }

        //hunger
        if (Time.time >= nextReduceHunger)
        {
            if (isSprinting)
            {
                currentHunger -= 3;
            }
            nextReduceHunger = Time.time + 1f / hungerTime;
        }

    }

    public void attack()
    {
        canDealDamage = true;
        hitBox.GetComponent<BoxCollider>().enabled = true;
        
    }

    public void takeDamage(int damage)
    {
        gettingDamaged = true;
        stopParrying();
        StartCoroutine(beingDealtDamage());
        if (isRolling)
        {
            return;
        }
        currentHealth -= damage;
        animator.SetTrigger("hurt");

        if (currentHealth <= 0)
        {
            die();
        }
    }

    public void die()
    {
        isAlive = false;
        animator.SetBool("isDead", true);
        GetComponent<Collider>().enabled = false;
        this.enabled = false;
    }

    public void parry()
    {
        isParrying = true;
        animator.SetBool("block", true);
        StartCoroutine(stunOpponentCoroutine());
    }

    private IEnumerator stunOpponentCoroutine()
    {
        stunOpponent = true;
        yield return new WaitForSeconds(stunDuration);
        stunOpponent = false;
    }
    public void stopParrying()
    {
        isParrying = false;
        animator.SetBool("block", false);
    }
    private IEnumerator roll()
    {
        isRolling = true;
        yield return new WaitForSeconds(0.3f);
        rollDelaydone = true;
        yield return new WaitForSeconds(0.3f);
        isRolling = false;
        rollTriggered = false;
        rollDelaydone = false;
    }
    private IEnumerator beingDealtDamage ()
    {
        yield return new WaitForSeconds(0.6f);
        gettingDamaged = false;
    }

    private IEnumerator stopSpinning()
    {
        yield return new WaitForSeconds(1f);
        spinAttacking = false;
    }

    private void testing()
    {
        Destroy(parentOfWeapon.transform.GetChild(0).gameObject);
        GameObject newChild = Instantiate(newWeapon, parentOfWeapon.transform);
        newChild.transform.localPosition = new Vector3(-0.08f, 0f, 0f);
        newChild.transform.localRotation = Quaternion.Euler(100f, 0f, 0f);
        newChild.transform.localScale = Vector3.one;
        hitBox = newChild.transform.Find("Hitbox").gameObject;
        dc.hitBox = hitBox;
    }
}


