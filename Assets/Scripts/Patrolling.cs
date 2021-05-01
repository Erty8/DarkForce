using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrolling : MonoBehaviour
{
    
    private Transform player;
    public NavMeshAgent agent;
    private float dist;
    public float detectRange;
        
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(player.position, transform.position);

        if(dist <= detectRange)
        {
            Vector3 targetVector = player.position;
            agent.SetDestination(targetVector);
            
        }
        
    }
}
