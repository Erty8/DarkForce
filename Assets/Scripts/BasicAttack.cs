using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public float dmgPerSec = 0f;
    public bool damageCd = false;
    public float attackDamage = 20f;
    public float fireballtimeDamage = 0f;
    public float dmgforSeconds = 5f;
    public float speed = 1.5f;
    float step;
    public Transform targetTransform;
    public static List<GameObject> enemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroy());
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        if (targetTransform != null) {
            transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, step);
        }
        
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
            col.gameObject.GetComponent<EnemyCombatScript>().takeDamage(attackDamage);
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
            gameObject.GetComponent<EnemyCombatScript>().takeDamage(attackDamage);
        }
        Debug.Log(enemies.Count);
        damageCd = true;
        Debug.Log("damaged");
        yield return new WaitForSeconds(dmgPerSec);
        damageCd = false;

    }
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(0.2f);
        if (targetTransform == null)
        {
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(0.2f);
        if (targetTransform == null)
        {
            Destroy(gameObject);
        }

    }
}
