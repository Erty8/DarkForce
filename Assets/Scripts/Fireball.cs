using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float dmgPerSec = 5f;
    public bool damageCd = false;
    public float fireballDamage = 20f;
    public float fireballtimeDamage = 5f;
    public float dmgforSeconds = 5f;
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
            //Debug.Log(damageCd);

            if (damageCd == false)
            {
                //StartCoroutine(damageEnemies());
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.tag == "Enemy" && enemies.Contains(col.gameObject) == false)
        {
            col.gameObject.GetComponent<EnemyCombatScript>().takeDamage(fireballDamage);
            col.gameObject.GetComponent<EnemyCombatScript>().takedamageoverTime(fireballtimeDamage,dmgforSeconds,1f);
            Destroy(gameObject);
        }

    }
  
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            enemies.Remove(col.gameObject);
        }
    }
    IEnumerator damageEnemies()
    {
        foreach (GameObject gameObject in enemies)
        {
            gameObject.GetComponent<EnemyCombatScript>().takeDamage(fireballDamage);
        }
        Debug.Log(enemies.Count);
        damageCd = true;
        Debug.Log("damaged");
        yield return new WaitForSeconds(dmgPerSec);
        damageCd = false;

    }
}
