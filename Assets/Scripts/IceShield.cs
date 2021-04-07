using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShield : MonoBehaviour
{
    public float dmgPerSec = 1f;
    public bool damageCd = false;
    public float shieldDamage = 20f;
    public static List<GameObject> enemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //enemies.Clear();
        //damageCd = false;
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
       
    }
    /*void OnTriggerStay(Collider col)
    {
        foreach (GameObject currentEnemy in enemies)
        {
            //Debug.Log(enemies.Count);
            if (enemies.Count!=0)
            {
                if (damageCd == false)
                {
                    StartCoroutine(damageEnemies());
                }
            }
        }
               
    }*/
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            enemies.Remove(col.gameObject);
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

}
