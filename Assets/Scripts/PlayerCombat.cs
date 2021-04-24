using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public Slider healthBar;
    public float health = 100f;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health;
        
    }
    public void takeDamage(float x)
    {
        health -= x;
        Debug.Log(health);
        if (health <=0 )
        {
            //anim.SetBool("death", true);
            Invoke("destroy", 7f);
            //Destroy(gameObject);
        }
    }
    public void takedamageoverTime(float x, float y, float z)
    {
        StartCoroutine(takeDmgoverTime(x,y,z));
    }
    IEnumerator takeDmgoverTime(float x,float y,float z)
    {
        for (int i = 0; i < y; i++)
        {
            takeDamage(x);
            
            Debug.Log("damage over time");
            yield return new WaitForSeconds(z);
            
        }
        yield return null;
    }
    void destroy()
    {
        Destroy(gameObject);
    }
    
}
