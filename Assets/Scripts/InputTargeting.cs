using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTargeting : MonoBehaviour
{

    public GameObject selectedHero;
    public bool heroPlayer;
    RaycastHit hit;

    //Movement movementScript;
    bool attackMovebool;
    public KeyCode attackMovekey;


    // Start is called before the first frame update
    void Start()
    {
        selectedHero = GameObject.FindGameObjectWithTag("Player");
        //movementScript = GetComponent<Movement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (hit.collider == null)
        {
            //Debug.Log("null");
        }
        if (Input.GetKey(attackMovekey))
        {
            attackMovebool = true;
            

        }

        //Debug.Log(Input.GetMouseButtonDown(0));
        if (Input.GetMouseButtonDown(1))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if(hit.collider.GetComponent<Targetable>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Cube)
                    {
                        selectedHero.GetComponent<Attacking>().targetedEnemy = hit.collider.gameObject;
                        selectedHero.GetComponent<Attacking>().oldtargetedEnemy = hit.collider.gameObject;
                    }
                }
                if (hit.collider.GetComponent<Item>() != null)
                {
                    selectedHero.GetComponent<Attacking>().itemToPick = hit.collider.gameObject;
                    selectedHero.GetComponent<Attacking>().agent.SetDestination 
                        (new Vector3 (hit.point.x,selectedHero.transform.position.y,hit.point.z));


                }

            }

            else if(hit.collider.gameObject.GetComponent<Targetable>() == null)
            {
                
                
                selectedHero.GetComponent<Attacking>().targetedEnemy = null;                                                         
            }
            if (hit.collider.gameObject.GetComponent<Item>() == null)
            {


                selectedHero.GetComponent<Attacking>().itemToPick = null;
            }
        }
        if (attackMovebool && Input.GetMouseButtonDown(0))
        {
            
                Debug.Log("attack move target");
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                {
                    if (hit.collider.GetComponent<Targetable>() != null)
                    {
                        if (hit.collider.gameObject.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Cube)
                        {
                            selectedHero.GetComponent<Attacking>().targetedEnemy = hit.collider.gameObject;
                            selectedHero.GetComponent<Attacking>().oldtargetedEnemy = hit.collider.gameObject;
                        }
                    }

                }
            attackMovebool = false;
            
        }
    }
}
