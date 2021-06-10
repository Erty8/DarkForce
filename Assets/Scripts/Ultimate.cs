using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class Ultimate : MonoBehaviour
{
    float dmgPerSec = 5f;
    public bool damageCd = false;
    public float damage = 50f;
    public static List<GameObject> enemies = new List<GameObject>();
    [SerializeField] VisualEffect visualEffect;
    [SerializeField] GameObject effectObject;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(effectObject,transform.position,transform.rotation);
        visualEffect = effectObject.GetComponent<VisualEffect>();
        //visualEffect = GetComponentInChildren<VisualEffect>();
        visualEffect.Stop();
        visualEffect.Play();
        StartCoroutine(stop());
        
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
            col.gameObject.GetComponent<EnemyCombatScript>().takeDamage(damage);
            
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
            gameObject.GetComponent<EnemyCombatScript>().takeDamage(damage);
        }
        Debug.Log(enemies.Count);
        damageCd = true;
        Debug.Log("damaged");
        yield return new WaitForSeconds(dmgPerSec);
        damageCd = false;

    }
    IEnumerator stop()
    {
        yield return new WaitForSeconds(2);
        visualEffect.Stop();
    }
}
