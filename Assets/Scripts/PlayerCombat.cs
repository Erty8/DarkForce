using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public Canvas healthBar3D;
    public Slider healthBarSlider2D;
    public Slider healthBarSlider;
    public float health = 100f;
    public Animator anim;
    public bool hasShield = false ;
    public float shieldReduce = 2;
    [SerializeField] GameObject iceShield;
    // Start is called before the first frame update
    void Start()
    {
        healthBarSlider.maxValue = health;
        healthBarSlider2D.maxValue = health;

    }

    // Update is called once per frame
    void Update()
    {
        
        healthBarSlider.value = health;
        healthBarSlider2D.value = health;

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
        healthBar3D.transform.LookAt(healthBar3D.transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.up);
    }
    public void takeDamage(float x)
    {
        if (hasShield)
        {
            health -= (x / shieldReduce);
        }
        else
        {
            health -= x;           
        }
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
    void shieldBool()
    {
        if (iceShield.activeInHierarchy == true)
        {
            hasShield = true;
        }
        else
        {
            hasShield = false;
        }
    }
}
