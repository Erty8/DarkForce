using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
    EnemyCombatScript enemyCombatScript;

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
    bool attackCooldown = false;
    public bool summoned;
    int attackRandomize;
    
    public float speed = 4f;
    public float attackDamage = 10f;
    public float attackCd = 5f;
    float attacktimePassed;
    public float attackRange = 25f;
    public int attackIndex = 0; 
    float step;
    public float rotSpeed = 4f;
    public float abilityCD = 10f;
    float abilityTimePassed;
    public float abilityCastTime = 2f;
    public float walkSpeed;
    public float speedVal;
    public bool walkbool = true;
    public bool rotatebool = true;

    float motionSmoothTime = .1f;
    float maxSpeed;
    Vector3 position;
    Vector3 targetPosition;
    Vector3 lookAtTarget;
    Quaternion EnemyRot;
    [SerializeField] GameObject closestEnemy = null;
    public float rotateSpeedMovement;
    public float rotateSpeedAttack; 
    public float rotateVelocity;

    public EnemyPath pathScript;
    // Start is called before the first frame update
    void Start()
    {
        enemyCombatScript = GetComponent<EnemyCombatScript>();
        
        attacktimePassed = -attackCd;
        
        position = transform.position;
        //skillshotCanvas = this.transform.Find("Skillshot Canvas").gameObject;
        skillshotCanvas.SetActive(false);
        //attackRangeImage = gameobject.transform.Find("Range Canvas").transform.Find("Attack Range");
        rangeIndicator();
        maxSpeed = agent.speed;
        StartCoroutine(randomizer());
        pathScript = GetComponent<EnemyPath>();
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
        if (summoned)
        {
            enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
            enemies.Remove(gameObject);
        }
        float distanceToClosestEnemy = Mathf.Infinity;

        foreach (GameObject currentEnemy in enemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
                pathScript.player = closestEnemy.transform;
            }
        }
        //transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, step);
        Attack();
    }
    public void Attack()
    {
        //Attacking after health = 0 bug fixed 
        if (Vector3.Distance(new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z), new Vector3
                (closestEnemy.transform.position.x, 0, closestEnemy.transform.position.z)) <= attackRange && enemyCombatScript.isAlive)
        {
            //walkbool = false;
                        
            //if (closestEnemy.tag == "Player")
            {
                agent.stoppingDistance = attackRange;
                if (rotatebool)
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
                    attackCooldown = false;
                    attacktimePassed = Time.time;
                    Debug.Log("Enemy attacked");
                    StartCoroutine(attackBool());
                    agent.SetDestination(transform.position);
                    //rotatebool = false;
                    walkbool = false;
                    attackCooldown = true;
                }
                
                else if ((Time.time - abilityTimePassed) > abilityCD)
                {
                    rotatebool = false;
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
        

        //
        yield return new WaitUntil(() => walkbool == true);
        if (Vector3.Distance(new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z), new Vector3
                (closestEnemy.transform.position.x, 0, closestEnemy.transform.position.z)) > attackRange)
        {
            anim.SetBool("continueAttack", false);
            anim.SetBool("attack", false);
        }
        else
        {
            anim.SetBool("continueAttack", true);
            walkbool = false;
        }
        //yield return new WaitUntil(() => attackCooldown == true);
        
        //yield return new WaitUntil(() => attackIndex == 2);
        //anim.SetBool("continueAttack", false);
        attackIndex = 0;
        //anim.SetBool("attack", false);

    }
    /*IEnumerator attackBool()
    {
        anim.SetBool("attack", true);
        

        //
        yield return new WaitUntil(() => walkbool == true);
        if (Vector3.Distance(new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z), new Vector3
                (closestEnemy.transform.position.x, 0, closestEnemy.transform.position.z)) > attackRange)
        {
            anim.SetBool("continueAttack", false);
            anim.SetBool("attack", false);
        }
        else
        {
            anim.SetBool("continueAttack", true);
        }
        //yield return new WaitUntil(() => attackCooldown == true);
        yield return new WaitForSeconds(3f);
        anim.SetBool("attack", false);
        yield return new WaitUntil(() => attackIndex == 2);
        anim.SetBool("continueAttack", false);
        attackIndex = 0;
        //anim.SetBool("attack", false);

    }*/
    IEnumerator randomizer()
    {
        yield return new WaitForSeconds(2.5f);
        attackRandomize = Random.Range(1, 3);
        anim.SetInteger("random", attackRandomize);
        //Debug.Log(attackRandomize);
        StartCoroutine(randomizer());
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
        if (closestEnemy.gameObject.GetComponent<PlayerCombat>() == true)
        {
            closestEnemy.gameObject.GetComponent<PlayerCombat>().takeDamage(attackDamage);
        }
        if (closestEnemy.gameObject.GetComponent<EnemyCombatScript>() == true)
        {
            closestEnemy.gameObject.GetComponent<EnemyCombatScript>().takeDamage(attackDamage);
        }
        //closestEnemy.gameObject.GetComponent<PlayerCombat>().takeDamage(attackDamage);
        Quaternion rotationToLookAt = Quaternion.LookRotation(new Vector3
                  (closestEnemy.transform.position.x, 0, closestEnemy.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z));
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
    rotationToLookAt.eulerAngles.y, ref rotateVelocity, rotateSpeedAttack * (Time.deltaTime * 5));
        transform.eulerAngles = new Vector3(0, rotationY, 0);
        agent.SetDestination(transform.position);
        if (Vector3.Distance(new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z), new Vector3
                (closestEnemy.transform.position.x, 0, closestEnemy.transform.position.z)) > attackRange)
        {
            anim.SetBool("continueAttack", false);
            anim.SetBool("attack", false);
        }
        else
        {
            anim.SetBool("continueAttack", true);
        }
        //anim.SetBool("attack", false);
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
            walkbool = false;
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
        Debug.Log("canmove");
    }
    public void cannotWalk()
    {
        walkbool = false;
        Debug.Log("cant move ");
    }
    public void canRotate()
    {
        rotatebool = true;
    }
    public void cannotRotate()
    {
        rotatebool = false;
    }
}
