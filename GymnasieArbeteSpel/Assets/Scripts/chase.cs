using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.Shapes;

public class chase : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public Vector3 EnemyRange = Vector3.back;
        
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent <NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position -= EnemyRange;
    } 
}
