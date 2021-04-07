using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    public float health = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeDamage(float x)
    {
        health -= x;
        Debug.Log(health);
        if (health <=0 )
        {
            Destroy(gameObject);
        }
    }
}
