using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
    [SerializeField] GameObject spike;
    [SerializeField] Transform spike1Transform;
    [SerializeField] Transform spike2Transform;
    [SerializeField] Transform spike3Transform;
    [SerializeField] Animator anim;
    [SerializeField] Image attackRangeImage;
    [SerializeField] public GameObject skillshotCanvas; 
    [SerializeField] NavMeshAgent agent; 
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
    public float walkSpeed;
    public float speedVal;
    public static bool walkbool = true;

    float motionSmoothTime = .1f;
    float maxSpeed;
    Vector3 position;
    Vector3 targetPosition;
    Vector3 lookAtTarget;
    Quaternion EnemyRot;
    [SerializeField] GameObject closestEnemy = null;
    public float rotateSpeedMovement;
    public float rotateVelocity;
    // Start is called before the first frame update
    void Start()
    {
        attacktimePassed = -attackCd;
        
        position = transform.position;
        //skillshotCanvas = this.transform.Find("Skillshot Canvas").gameObject;
        skillshotCanvas.SetActive(false);
        //attackRangeImage = gameobject.transform.Find("Range Canvas").transform.Find("Attack Range");
        rangeIndicator();
        maxSpeed = agent.speed; 
    }

    protected void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (walkbool)
        {

        }
        step = speed * Time.deltaTime;
        FindClosestEnemy();
        targetPosition = closestEnemy.transform.position;
        if (walkbool && (Vector3.Distance(new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z), new Vector3
                (closestEnemy.transform.position.x, 0, closestEnemy.transform.position.z)) > attackRange) )
        {
            anim.SetBool("attack", false);
        }
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
            //walkbool = false;
            
            

            if (closestEnemy.tag == "Player")
            {
                agent.stoppingDistance = attackRange;
                if (walkbool)
                {
                    Quaternion rotationToLookAt = Quaternion.LookRotation(new Vector3
                   (closestEnemy.transform.position.x, 0, closestEnemy.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z));
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                rotationToLookAt.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));
                    transform.eulerAngles = new Vector3(0, rotationY, 0);
                    agent.SetDestination(transform.position);
                }
                //if (canAbility) { StartCoroutine(castAbility()); }
                if ((Time.time - attacktimePassed) > attackCd)
                {
                    attacktimePassed = Time.time;
                    Debug.Log("Enemy attacked");
                    StartCoroutine(attackBool());
                    agent.SetDestination(transform.position);
                    walkbool = false;

                }
                
                else if ((Time.time - abilityTimePassed) > abilityCD)
                {
                    abilityTimePassed = Time.time;
                    StartCoroutine(castAbility());
                    walkbool = false;



                }
            }
        }
        else
        {
            if (walkbool)
            {
                //agent.SetDestination(closestEnemy.transform.position);
            }
            
        }
        if (walkbool)
        {
            //agent.SetDestination(transform.position);
        }
    }
    IEnumerator castAbility()
    {
        anim.SetBool("ability", true);
        canAbility = false;
        Debug.Log("enemy ability casted");
        yield return new WaitForSeconds(1f);
        agent.SetDestination(transform.position);
        anim.SetBool("ability", false);
        yield return new WaitForSeconds(abilityCD-1f);
        canAbility = true;
        //yield return new WaitForSeconds(abilityCD);

    }
    IEnumerator attackBool()
    {
        anim.SetBool("attack", true);
        
        
        yield return new WaitForSeconds(1f);
        if (Vector3.Distance(new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z), new Vector3
                (closestEnemy.transform.position.x, 0, closestEnemy.transform.position.z)) > attackRange)
        {
            anim.SetBool("attack", false);
        }

    }
    IEnumerator walkFalse()
    {
        anim.SetBool("attack", true);


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
   
    public void castSpikes(float y, float z)
    {
        StartCoroutine(castspike(y, z));
    }
    IEnumerator castspike(float y, float z)
    {
        //skillshotCanvas.gameObject.SetActive(true);
        for (int i = 0; i < y; i++)
        {
            Instantiate(spike, spike1Transform.transform.position, spike1Transform.transform.rotation);
            Instantiate(spike, spike2Transform.transform.position, spike2Transform.transform.rotation);
            Instantiate(spike, spike3Transform.transform.position, spike3Transform.transform.rotation);

            Debug.Log("enemy spikes launched");
            yield return new WaitForSeconds(z);

        }
        skillshotCanvas.gameObject.SetActive(false);
        yield return null;
    }
    public void canWalk()
    {
        walkbool = true;        
    }
}
