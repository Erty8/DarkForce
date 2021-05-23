using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IceShield : MonoBehaviour
{
    public float dmgPerSec = 1f;
    public bool damageCd = false;
    public float shieldDamage = 20f;
    public float slowedSpeed = 2f;
    public float shieldDuration = 5f;
    public List<GameObject> enemies = new List<GameObject>();
    Dictionary<GameObject, float> speeds = new Dictionary<GameObject, float>();
    // Start is called before the first frame update
    void Start()
    {
        
        //enemies.Clear();
        //damageCd = false;
    }
    private void OnEnable()
    {
        StartCoroutine(destroyShield());
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count != 0)
        {
            //Debug.Log(damageCd);
            
            if (damageCd == false)
            {
                StartCoroutine(damageEnemies());
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy" &&enemies.Contains(col.gameObject)==false)
        {
            enemies.Add(col.gameObject);
            //Debug.Log(enemies.Count);
        }
        if (col.gameObject.GetComponent<NavMeshAgent>() != null)
        {
            if (speeds.ContainsKey(col.gameObject) == false)
            {
                speeds.Add(col.gameObject, col.gameObject.GetComponent<NavMeshAgent>().speed);
            }

            //speeds.Insert(speedIndex, col.gameObject.GetComponent<NavMeshAgent>().speed);
            col.gameObject.GetComponent<NavMeshAgent>().speed = col.gameObject.GetComponent<NavMeshAgent>().speed / slowedSpeed;
        }

    }
   
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            enemies.Remove(col.gameObject);
        }
        if (col.gameObject.GetComponent<NavMeshAgent>() != null)
        {
            if (speeds.ContainsKey(col.gameObject))
            {

                col.gameObject.GetComponent<NavMeshAgent>().speed = speeds[col.gameObject];
            }

        }
    }
    IEnumerator damageEnemies()
    {
       foreach(GameObject gameObject in enemies)
        {
            gameObject.GetComponent<EnemyCombatScript>().takeDamage(shieldDamage);
        }
        Debug.Log(enemies.Count);
        damageCd = true;
        Debug.Log("damaged");
        yield return new WaitForSeconds(dmgPerSec);
        damageCd = false;
        
    }
    IEnumerator destroyShield()
    {
        yield return new WaitForSeconds(shieldDuration);
        foreach (GameObject gameObject in enemies)
        {
            gameObject.GetComponent<NavMeshAgent>().speed = speeds[gameObject];            
        }
        enemies.Clear();
        gameObject.SetActive(false);
    }

}
