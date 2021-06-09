using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float trapDamage = 35;
    public float detectRange = 20;
    public float timeBeforeInvisible = 2f;
    bool candamage;
    bool ready = false;
    public List<GameObject> enemies = new List<GameObject>();
    GameObject closestEnemy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(invisible());
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();  
        if (ready)
        {         
            
                if (Vector3.Distance(new Vector3 (closestEnemy.transform.position.x,transform.position.y,closestEnemy.transform.position.z)
                    , transform.position) <= detectRange)
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = true;
                    candamage = true;

                }
                else
                {
                    
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    candamage = false;
                
                }
                
                       
        }
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (candamage)
        {
            if (col.tag == "Player")
            {
                col.GetComponent<PlayerCombat>().takeDamage(trapDamage);
                Destroy(gameObject);
            }
            if (col.tag == "Enemy")
            {
                col.GetComponent<EnemyCombatScript>().takeDamage(trapDamage);
                col.GetComponentInChildren<Animator>().SetBool("takeHit", true);
                Destroy(gameObject);    
            }
        }
        
    }
    void FindClosestEnemy()
    {
        List<GameObject> enemies = new List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        float distanceToClosestEnemy = Mathf.Infinity;

        foreach (GameObject currentEnemy in enemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        //transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, step);
        
    }
    IEnumerator invisible()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(timeBeforeInvisible);
        ready = true;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
