using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimator : MonoBehaviour
{

    NavMeshAgent agent;
    public Animator anim;
    public float speed;
    public float speedVal;

    float motionSmoothTime = .1f;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);
        speedVal = speed * motionSmoothTime * Time.deltaTime;
        //Debug.Log(speedVal);


    }
}
