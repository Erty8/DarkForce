using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombatScript : MonoBehaviour
{
    public Slider enemySlider;
    public float health = 100f;
    float maxhealth;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        enemySlider.maxValue = health;
        maxhealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        enemySlider.value = health;
        
    }
    public void takeDamage(float x)
    {
        health -= x;
        //anim.SetBool("takeHit", true);
        Debug.Log(health);
        if (health <= maxhealth/2)
        {
            for (int i = 0; i < 1; i++)
            {
                anim.SetBool("takeHit", true);
            }
            
        }
        if (health <=0 )
        {
            anim.SetBool("death", true);
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
