using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{

    public float dmgPerSec = 5f;
    bool damageCd = false;
    public float spikeDamage = 20f;
    public float spikebleedDamage = 5f;
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

        if (col.gameObject.tag == "Player" )
        {
            Debug.Log("Spikes damaged Player");
            col.gameObject.GetComponent<PlayerCombat>().takeDamage(spikeDamage);
            col.gameObject.GetComponent<PlayerCombat>().takedamageoverTime(spikebleedDamage, dmgforSeconds, 1f);
            Destroy(gameObject);
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            enemies.Remove(col.gameObject);
        }
    }
    IEnumerator damageEnemies()
    {
        foreach (GameObject gameObject in enemies)
        {
            gameObject.GetComponent<PlayerCombat>().takeDamage(spikeDamage);
        }
        Debug.Log(enemies.Count);
        damageCd = true;
        Debug.Log("damaged");
        yield return new WaitForSeconds(dmgPerSec);
        damageCd = false;

    }
}
