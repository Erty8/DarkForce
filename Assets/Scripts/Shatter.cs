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
    bool hardened = false;
    bool noDamage = false;
    public float shatterDamage = 50f;
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> damagedEnemies = new List<GameObject>();
    public List<GameObject> objects = new List<GameObject>();
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(noDamageroutine());
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.time);
        if (enemies.Count != 0)
        {
            
            if (damageCd == false&&!hardened&&!noDamage)
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
       
        

    }
   

    void OnTriggerExit(Collider col)
    {
        unitCount--;
        if (col.gameObject.tag == "Enemy")
        {
            enemies.Remove(col.gameObject);
        }
       
       

    }
    IEnumerator damageEnemies()
    {
        foreach (GameObject gameObject in enemies)
        {
            if (!damagedEnemies.Contains(gameObject)) 
            {
                gameObject.GetComponent<EnemyCombatScript>().takeDamage(shatterDamage);
                gameObject.GetComponentInChildren<Animator>().SetBool("takeHit", true);
                damagedEnemies.Add(gameObject);
            }
            
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
            hardened = true;
        }
    }
    IEnumerator noDamageroutine()
    {
        yield return new WaitForSeconds(1);
        noDamage = true;
    }
}
