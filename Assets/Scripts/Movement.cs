using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Movement : MonoBehaviour
{
    public NavMeshAgent agent;

    public float rotateSpeedMovement;
    public float rotateVelocity;

    private Attacking attackingtScript;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        attackingtScript = GetComponent<Attacking>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(attackingtScript.targetedEnemy != null)
        {
            if(attackingtScript.targetedEnemy.GetComponent<Attacking>() != null)
            {
                if (attackingtScript.targetedEnemy.GetComponent<Attacking>().isHeroAlive)
                {
                    attackingtScript.targetedEnemy = null;
                }
            }

           
        }


        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if(hit.collider.tag == "Floor")
                {
                    agent.SetDestination(hit.point);
                    attackingtScript.targetedEnemy = null;
                    agent.stoppingDistance = 0;

                    Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                        rotationToLookAt.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));

                    transform.eulerAngles = new Vector3(0, rotationY, 0);
                }

            }
        }
    }
}
