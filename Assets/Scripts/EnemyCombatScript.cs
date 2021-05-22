using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombatScript : MonoBehaviour
{

    public XPManager XP_ManagerScript;

    public Canvas enemyHealthBar;
    public Slider enemySlider;
    public float health = 100f;
    private bool canbeDamaged = true;
    float maxhealth;
    public Animator anim;

    //Bool that is used to fix "surfing" after death in EnemyPath script
    public bool isAlive = true;

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

    
    private void LateUpdate()
    {
        //Enemy Health Bar not faced to the camera fix    
        enemyHealthBar.transform.LookAt(enemyHealthBar.transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.up);
    }
    public void takeDamage(float x)
    {
        health -= x;
        //anim.SetBool("takeHit", true);
        //Debug.Log(health);
        
        if (health <= maxhealth/2 &&canbeDamaged)
        {
   
            anim.SetBool("takeHit", true);
            StartCoroutine(cannotbeDamaged());
            
                     
        }
        if (health <=0 )
        {
            isAlive = false;
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
            
            //Debug.Log("damage over time");
            yield return new WaitForSeconds(z);
            
        }
        yield return null;
    }
    IEnumerator cannotbeDamaged()
    {
        yield return new WaitForSeconds(1f);
        canbeDamaged = false;
    }
    void destroy()
    {
        XP_ManagerScript.ShouldGainXP();
        Destroy(gameObject);
        
    }
    
}
