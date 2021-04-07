using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Movement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;
    PlayerAnimator animatorScript;
    

    public float rotateSpeedMovement;
    public float rotateVelocity;

    private Attacking attackingtScript;
    public KeyCode attackMove;
    Attacking attackScript;
    //public Canvas attackRangeCanvas;
    public Image attackRangeImage;
    bool attackMovebool = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        attackingtScript = GetComponent<Attacking>();
        animatorScript = GetComponent<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animatorScript.speedVal ==0)
        {
            //Debug.Log(anim.GetFloat("Speed"));
            anim.SetBool("Moving", false);
            //Debug.Log("stop");
        }
        else
        {
            anim.SetBool("Moving", true);
        }
        /*if (Vector3.Distance(attackingtScript.targetedEnemy.transform.position , this.transform.position)
            < attackingtScript.attackRange)
        {
            Debug.Log("Enemy in range");
        }*/
        if (attackingtScript.targetedEnemy != null)
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
            move();
        }

        if (Input.GetKey(attackMove))
        {
            attackMovebool = true;
            attackRangeImage.GetComponent<Image>().enabled = true;
            
        }
        if (attackMovebool&&Input.GetMouseButton(0))
        {
            move();
            attackRangeImage.GetComponent<Image>().enabled = false;
            Debug.Log("attack move");
            attackMovebool = false;
        }
    }
    public void move()
    {
        RaycastHit hit;
        //anim.SetBool("Moving", true);
        //Debug.Log(anim.GetFloat("Speed"));

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Floor")
            {
                agent.SetDestination(hit.point);
                attackingtScript.targetedEnemy = null;
                agent.stoppingDistance = 0;

                Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                    rotationToLookAt.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));

                transform.eulerAngles = new Vector3(0, rotationY, 0);
                //Invoke("movingFalse", 0.5f);
                
            }

        }
       
    }

}
