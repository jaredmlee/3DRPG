using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public float speed = 2f;
    public float followRange = 8f;
    public float minFollowRange = 2f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > minFollowRange && distance < followRange)
        {
            if(gameObject.layer == LayerMask.NameToLayer("Bear"))
            {
                animator.SetBool("Run Forward", true);
            }
            else
            {
                animator.SetBool("walk", true);
            }
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            if (gameObject.layer == LayerMask.NameToLayer("Bear"))
            {
                animator.SetBool("Run Forward", false);
            }
            else if (!GetComponent<Enemy>().canWalk)
            {
                animator.SetBool("walk", false);
            }
        }
    }
}
