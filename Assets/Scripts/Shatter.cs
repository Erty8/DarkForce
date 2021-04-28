using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shatter : MonoBehaviour
{
    public float dmgPerSec = 3f;
    public float slowedSpeed = 2f;
    public float hardenTime = 1.5f;
    private int unitCount = 0;
    private int speedIndex = 0;
    public bool damageCd = false;
    public float shatterDamage = 50f;
    public static List<GameObject> enemies = new List<GameObject>();
    public static List<GameObject> objects = new List<GameObject>();
    //public static List<float> speeds = new List<float>();
    Dictionary<GameObject,float> speeds = new Dictionary<GameObject,float>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count != 0)
        {

            if (damageCd == false)
            {
                StartCoroutine(damageEnemies());
            }
        }
        if (unitCount == 0)
        {
            StartCoroutine(harden());
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        unitCount++;
        objects.Add(col.gameObject);
        if (col.gameObject.tag == "Enemy" && enemies.Contains(col.gameObject) == false)
        {
            enemies.Add(col.gameObject);
            //Debug.Log(enemies.Count);
        }
        if (col.gameObject.GetComponent<NavMeshAgent>() != null )
        {
            if (speeds.ContainsKey(col.gameObject) == false)
            {
                speeds.Add(col.gameObject, col.gameObject.GetComponent<NavMeshAgent>().speed);
            }
            
            //speeds.Insert(speedIndex, col.gameObject.GetComponent<NavMeshAgent>().speed);
            col.gameObject.GetComponent<NavMeshAgent>().speed = col.gameObject.GetComponent<NavMeshAgent>().speed/slowedSpeed;
        }
        

    }
   

    void OnTriggerExit(Collider col)
    {
        unitCount--;
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
        foreach (GameObject gameObject in enemies)
        {
            gameObject.GetComponent<EnemyCombatScript>().takeDamage(shatterDamage);
        }

        damageCd = true;
        Debug.Log("damaged");
        yield return new WaitForSeconds(dmgPerSec);
        damageCd = false;

    }
    IEnumerator harden()
    {
        yield return new WaitForSeconds(hardenTime);
        if (unitCount == 0)
        {
            gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
        }
    }
}
