using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShield : MonoBehaviour
{
    public float dmgPerSec = 1f;
    bool damageCd = false;
    public static List<GameObject> enemies = new List<GameObject>();
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
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            enemies.Add(col.gameObject);
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
        damageCd = true;
        Debug.Log("damaged");
        yield return new WaitForSeconds(dmgPerSec);
        damageCd = false;
    }

}
