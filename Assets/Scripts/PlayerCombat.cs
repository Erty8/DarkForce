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
    public float maxhealth;
    public Animator anim;
    public bool hasShield = false ;
    public float shieldReduce = 2;
    public float regenRate = 1;
    public float potionRegenRate = 10f;
    public float healthPercentage;
    public bool potion = false;
    Canvas pauseCanvas;
    Canvas feedbackCanvas;
    [SerializeField] GameObject resumeButton;
    [SerializeField] GameObject iceShield;
    // Start is called before the first frame update
    void Start()
    {
        maxhealth = health;
        healthBarSlider.maxValue = maxhealth;
        healthBarSlider2D.maxValue = maxhealth;
        pauseCanvas = GameObject.Find("Pause").gameObject.GetComponent<Canvas>();
        resumeButton = GameObject.Find("Resume");
        feedbackCanvas = GameObject.Find("FeedbackCanvas").gameObject.GetComponent<Canvas>();


    }

    // Update is called once per frame
    void Update()
    {
        if (health < maxhealth) {
            if (potion)
            {
                health += potionRegenRate * Time.deltaTime;
            }
            else
            {
                health += regenRate * Time.deltaTime;
            }
            
        }
        
        healthBarSlider.maxValue = maxhealth;
        healthBarSlider2D.maxValue = maxhealth;
        healthBarSlider.value = health;
        healthBarSlider2D.value = health;

        if (health <= 0)
        {
            die();           
            Movement.canMove = false;
            
            //Destroy(gameObject);
        }
        healthPercentage = health / maxhealth;
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
        StartCoroutine(gameoverScreen());
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
    IEnumerator gameoverScreen()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("gameover");
        pauseCanvas.enabled = true;
        resumeButton.SetActive(false);
        feedbackCanvas.enabled = true;
    }
}
