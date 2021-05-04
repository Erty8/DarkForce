using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public Canvas healthBar;
    public Slider healthBarSlider;
    public float health = 100f;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        healthBarSlider.maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBarSlider.value = health;

        if (health <= 0)
        {
            die();
            Movement.canMove = false;

            //Destroy(gameObject);
        }

    }
       
    private void LateUpdate()
    {
        //Health Bar not faced to the camera fix
        healthBar.transform.LookAt(healthBar.transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.up);
    }
    public void takeDamage(float x)
    {
        health -= x;
        //Debug.Log(health);
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
            
            //Debug.Log("damage over time");
            yield return new WaitForSeconds(z);
            
        }
        yield return null;
    }
    void die()
    {
        anim.SetBool("Death", true);
        //Invoke("destroy", 7f);
    }
    void destroy()
    {
        Destroy(gameObject);
    }
    
}
