using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTargeting : MonoBehaviour
{

    public GameObject selectedHero;
    public bool heroPlayer;
    RaycastHit hit;
    Movement movementScript;
    bool attackMovebool;
    public KeyCode attackMovekey;


    // Start is called before the first frame update
    void Start()
    {
        selectedHero = GameObject.FindGameObjectWithTag("Player");
        movementScript = GetComponent<Movement>();
        
    }

    // Update is called once per frame
    void Update()
    {
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
                    
            }

            else if(hit.collider.gameObject.GetComponent<Targetable>() == null)
            {
                
                
                selectedHero.GetComponent<Attacking>().targetedEnemy = null;
                
                
                
            }
        }
        if (attackMovebool &&Input.GetMouseButtonDown(0))
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
