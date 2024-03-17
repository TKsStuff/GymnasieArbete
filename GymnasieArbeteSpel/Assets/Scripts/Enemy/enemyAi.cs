using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAi : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround , whatIsPlayer;


    //Variables for controlling attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projetile;

    //Enemy States
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttackRange;

    // stats
    public int health;




    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        //kallar på funktionerna chasePlayer och attacking när spelaren är inom en viss radie runt fienden
        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);



        if (playerInSight && !playerInAttackRange) chasePlayer();
        if (playerInAttackRange && playerInSight) attacking();


    }


    private void attacking()
    {

  
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        /*Attack code goes here
        Rigidbody rb = Instantiate(projetile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        rb.AddForce(transform.up * 8f, ForceMode.Impulse);

        */



    }

    private void attackReset()
    {

        if (!alreadyAttacked)
        {
           
            alreadyAttacked = true;
            Invoke(nameof(attackReset), timeBetweenAttacks);

        }

    }



    private void chasePlayer()
    {
        //Gör så att fiendens position som den ska gå till är där spelaren är

        agent.SetDestination(player.position);
        

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health < 0)
        {

            Invoke(nameof(DestroyEnemy), 0.01f);
        }

    }
    private void DestroyEnemy()
    {

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}
