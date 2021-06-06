using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombatScript : MonoBehaviour
{
    [SerializeField] GameObject loot;
    public XPManager XP_ManagerScript;
    
    public Canvas enemyHealthBar;
    public Slider enemySlider;
    public float health = 100f;
    private bool canbeDamaged = true;
    float maxhealth;
    public Animator anim;
    public Enemy_AI aiScript;
    public float summonDuration = 15f;
    bool lootSpawned = false;
    public bool hasLoot = true;

    //Bool that is used to fix "surfing" after death in EnemyPath script
    public bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        enemySlider.maxValue = health;
        maxhealth = health;
        aiScript = GetComponent<Enemy_AI>();
        if (aiScript.summoned)
        {
            Debug.Log("summoned");
            StartCoroutine(summonedDie());
        }
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
        
        /*if (health <= maxhealth/2 &&canbeDamaged)
        {
   
            anim.SetBool("takeHit", true);
            anim.SetBool("attack", false);
            StartCoroutine(cannotbeDamaged());
            aiScript.walkbool = false;                                
        }*/

        if (health <=0 )
        {
            isAlive = false;
            anim.SetBool("death", true);
            Invoke("destroy", 5f);
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
        if (!aiScript.summoned)
        {
            XP_ManagerScript.ShouldGainXP();
            if (!lootSpawned&&hasLoot)
            {
                Instantiate(loot, transform.position, transform.rotation);
                lootSpawned = true;
                Debug.Log("loot spawned");
            }            
            
        }       
        Destroy(gameObject);
    }
    IEnumerator summonedDie()
    {
        yield return new WaitForSeconds(summonDuration);
        isAlive = false;
        anim.SetBool("death", true);
        Invoke("destroy", 3f);
    }
    
}
