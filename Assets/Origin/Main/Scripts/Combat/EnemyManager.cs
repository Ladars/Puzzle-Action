using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyManager : MonoBehaviour
{
    private Animator animator;

    public float rotationSpeed = 10f;
    GameObject player;
    NavMeshAgent agent;
    //attack
    private float attackRange = 2;
    private float attackCD = 2;
    private float timePassed;
    private EnemyDamageDealer EnemyDamageDealer;
    private EnemyHeathSystem heathSystem;

    private bool isHurt;
    private float hurtTime;
    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        heathSystem = GetComponent<EnemyHeathSystem>();
        heathSystem.onHurt += IsHurt;
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        EnemyDamageDealer = GetComponentInChildren<EnemyDamageDealer>();
    }
    public void IsHurt()
    {
        isHurt = true;
        hurtTime = 1.5f;
    }

    private void Update()
    {
        animator.SetFloat("Speed",agent.velocity.magnitude/agent.speed);
        //transform.LookAt(player.transform, Vector3.up);
        LookAtPlayer();
        EnemyAttack();
        agent.SetDestination(player.transform.position);
        if (hurtTime>=0)
        {
            hurtTime -= Time.deltaTime;
        }
        else
        {
            isHurt = false;
        }
    }
    void LookAtPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f; // set y component to zero
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    void EnemyAttack()
    {
        if (timePassed >= attackCD)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackRange&&isHurt==false)
            {
                animator.SetTrigger("Attack");
                timePassed = 0;
               // animator.CrossFade("attack", 0.1f);
            }
        }
        timePassed += Time.deltaTime;
    }
    public void StartDealDamage()
    {
        EnemyDamageDealer.StartDealDamage();
        
    }
    public void EndDealDamage()
    {
        EnemyDamageDealer.EndDealDamage();
       
    }
}
