using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShield : MonoBehaviour
{
    bool damageCd = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (damageCd == false)
            {
                StartCoroutine(damageEnemy());
            }
            //InvokeRepeating("damageEnemy", 0.2f, 2f);
            // do whatever the lava does to the player such as reduce player health or shield
        }
    }
    IEnumerator damageEnemy()
    {
        damageCd = true;
        Debug.Log("damaged");
        yield return new WaitForSeconds(2f);
        damageCd = false;
    }

}
