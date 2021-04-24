using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_AI : MonoBehaviour
{
    [SerializeField] GameObject spike;
    [SerializeField] Transform spike1Transform;
    [SerializeField] Transform spike2Transform;
    [SerializeField] Transform spike3Transform;
    [SerializeField] Animator anim;
    [SerializeField] Image attackRangeImage;
    public float spikeWaveCount = 3f;
    public float timeBetweenSpikeWaves = 1f;
    public float radius = 10f;
    bool canAbility = true;
    
    public float speed = 4f;
    public float attackDamage = 10f;
    public float attackCd = 5f;
    float attacktimePassed;
    public float attackRange = 25f;
    float step;
    public float rotSpeed = 4f;
    public float abilityCD = 10f;
    float abilityTimePassed;
    public float abilityCastTime = 2f;
    Vector3 position;
    Vector3 targetPosition;
    Vector3 lookAtTarget;
    Quaternion EnemyRot;
    [SerializeField] GameObject closestEnemy = null;
    // Start is called before the first frame update
    void Start()
    {
        attacktimePassed = -attackCd;
        
        position = transform.position;
        //attackRangeImage = gameobject.transform.Find("Range Canvas").transform.Find("Attack Range");
        rangeIndicator();
    }

    protected void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        step = speed * Time.deltaTime;
        FindClosestEnemy();
        targetPosition = closestEnemy.transform.position;
    }
    void FixedUpdate()
    {
        
        
        lookAtTarget = new Vector3(targetPosition.x - transform.position.x, transform.position.y, targetPosition.z - transform.position.z);
        EnemyRot = Quaternion.LookRotation(lookAtTarget);
        //transform.rotation = Quaternion.Slerp(transform.rotation, EnemyRot, rotSpeed * Time.deltaTime);
    }

   
    void FindClosestEnemy()
    {
        List<GameObject> enemies = new List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        float distanceToClosestEnemy = Mathf.Infinity;

        foreach (GameObject currentEnemy in enemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        //transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, step);
        Attack();
    }
    public void Attack()
    {
        
        if (Vector3.Distance(new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z), new Vector3
                (closestEnemy.transform.position.x, 0, closestEnemy.transform.position.z)) <= attackRange)
        {
         
            if (closestEnemy.tag == "Player")
            {
                //if (canAbility) { StartCoroutine(castAbility()); }
                if ((Time.time - attacktimePassed) > attackCd)
                {
                    attacktimePassed = Time.time;
                    Debug.Log("Enemy attacked");
                    StartCoroutine(attackBool());

                }
                
                else if ((Time.time - abilityTimePassed) > abilityCD)
                {
                    abilityTimePassed = Time.time;
                    StartCoroutine(castAbility());
                    
                    

                }
                


            }
        }
        else { anim.SetBool("attack", false); }
    }
    IEnumerator castAbility()
    {
        anim.SetBool("ability", true);
        canAbility = false;
        Debug.Log("enemy ability casted");
        yield return new WaitForSeconds(1f);
        anim.SetBool("ability", false);
        yield return new WaitForSeconds(abilityCD-1f);
        canAbility = true;
        //yield return new WaitForSeconds(abilityCD);

    }
    IEnumerator attackBool()
    {
        anim.SetBool("attack", true);
        
        Debug.Log("enemy ability casted");
        yield return new WaitForSeconds(2f);
        anim.SetBool("attack", false);
        
        
        //yield return new WaitForSeconds(abilityCD);

    }
    /*void spawnSpikes()
    {
        float angleStep = 360f / spikeCount;
        //float angle = 0f;
        for (int i = 0; i <= spikeCount - 1; i++)
        {
            //float projectileDirXposition = spikeTransform.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
        }
    }*/
    public void rangeIndicator()
    {
        attackRangeImage.rectTransform.sizeDelta = new Vector2(attackRange / 2, attackRange / 2);
    }
    public void dealDamage()
    {
        closestEnemy.gameObject.GetComponent<PlayerCombat>().takeDamage(attackDamage);
        Debug.Log("Demon dealed " + attackDamage + " damage");
    }
    public void test()
    {
        Debug.Log("test");
    }
    public void castSpikes(float y, float z)
    {
        StartCoroutine(castspike(y, z));
    }
    IEnumerator castspike(float y, float z)
    {
        for (int i = 0; i < y; i++)
        {
            Instantiate(spike, spike1Transform.transform.position, spike1Transform.transform.rotation);
            Instantiate(spike, spike2Transform.transform.position, spike2Transform.transform.rotation);
            Instantiate(spike, spike3Transform.transform.position, spike3Transform.transform.rotation);

            Debug.Log("enemy spikes launched");
            yield return new WaitForSeconds(z);

        }
        yield return null;
    }
}
