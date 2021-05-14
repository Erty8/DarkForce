using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Attacking : MonoBehaviour
{
    
    public enum HeroAttackType { Melee, Ranged, Versatile};
    public HeroAttackType heroAttackType;

    [SerializeField] private Image attackRangeImage;
    public GameObject attackObject;
    [SerializeField] private Transform ProjectileTransform;
    public GameObject targetedEnemy = null;
    public GameObject oldtargetedEnemy = null;
    public GameObject itemToPick;
    public Animator anim;
    public float attackRange;
    public float attackDamage;
    public float attackSpeed = 1f;
    public float rotateSpeedForAttack;
    bool damageCd = false;
    bool enemyInRange = false;
    public bool attackOnSight;
    GameObject closestEnemy;
    

    private Movement moveScript;

    public bool basicAtkIdle = false;
    public bool isHeroAlive;
    public bool performMeleeAttack = true;
    private NavMeshAgent agent;

    [Header ("Inventory")]
    private Inventory inventory;
    public bool itemPickup;
    private Image inventoryImage;



    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<Movement>();
        inventory = GetComponent<Inventory>();
        agent = GetComponent<NavMeshAgent>();
    }
    void FixedUpdate()
    {
        if (attackOnSight)
        {
            FindClosestEnemy();
            if (Vector3.Distance(new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z), new Vector3
                (closestEnemy.transform.position.x, 0, closestEnemy.transform.position.z)) <= attackRange)
            {
                targetedEnemy = closestEnemy;
                oldtargetedEnemy = closestEnemy;
                Quaternion rotationToLookAt = Quaternion.LookRotation(new Vector3
              (closestEnemy.transform.position.x, 0, closestEnemy.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z));
                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
            rotationToLookAt.eulerAngles.y, ref moveScript.rotateVelocity, rotateSpeedForAttack * (Time.deltaTime * 5));
                transform.eulerAngles = new Vector3(0, rotationY, 0);
                moveScript.agent.SetDestination(transform.position);
                /*
                Quaternion rotationToLookAt = Quaternion.LookRotation(new Vector3
                (targetedEnemy.transform.position.x, 0, targetedEnemy.transform.position.z) - transform.position);
                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                    rotationToLookAt.eulerAngles.y, ref moveScript.rotateVelocity, moveScript.rotateSpeedMovement 
                    * (Time.deltaTime * 5));

                transform.eulerAngles = new Vector3(0, rotationY, 0);
                moveScript.agent.SetDestination(targetedEnemy.transform.position);
                moveScript.agent.stoppingDistance = attackRange;*/
                
            }
        }
        if (itemToPick != null)
        {
            moveScript.agent.SetDestination(itemToPick.transform.position);
            moveScript.agent.stoppingDistance = 0;
            Quaternion rotationToLookAt = Quaternion.LookRotation(itemToPick.transform.position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
            rotationToLookAt.eulerAngles.y, ref moveScript.rotateVelocity, rotateSpeedForAttack * (Time.deltaTime * 5));
            transform.eulerAngles = new Vector3(0, rotationY, 0);
        }
        if (targetedEnemy != null)
        {
            //distancetoEnemy = Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position);
            if (Vector3.Distance(new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z), new Vector3
                (targetedEnemy.transform.position.x, 0, targetedEnemy.transform.position.z)) > attackRange)
            {
                //targetedEnemy.transform.Find("Selected").gameObject.SetActive(true);
                enemyInRange = false;
                moveScript.agent.SetDestination(targetedEnemy.transform.position);
                moveScript.agent.stoppingDistance = attackRange;

                Quaternion rotationToLookAt = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);
                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                rotationToLookAt.eulerAngles.y, ref moveScript.rotateVelocity, rotateSpeedForAttack * (Time.deltaTime * 5));

                transform.eulerAngles = new Vector3(0, rotationY, 0);
            }
            if (Vector3.Distance(new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z), new Vector3
                (targetedEnemy.transform.position.x, 0, targetedEnemy.transform.position.z)) <= attackRange)
            {
                
                if (heroAttackType == HeroAttackType.Ranged)
                {
                    enemyInRange = true;
                    Quaternion rotationToLookAt = Quaternion.LookRotation(new Vector3
               (targetedEnemy.transform.position.x, 0, targetedEnemy.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z));
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                rotationToLookAt.eulerAngles.y, ref moveScript.rotateVelocity, rotateSpeedForAttack * (Time.deltaTime * 5));
                    transform.eulerAngles = new Vector3(0, rotationY, 0);
                    moveScript.agent.SetDestination(transform.position);                                          
                        StartCoroutine(damageEnemies());
                    
                }
            }
            else
            {
                if (heroAttackType == HeroAttackType.Melee)
                {
                    if (performMeleeAttack)
                    {
                        Debug.Log("A T T A C K");
                    }
                }
            }

        }
        rangeIndicator();

    }


    // Update is called once per frame
    void Update()
    {
        if (targetedEnemy != null)
        {
            targetedEnemy.transform.Find("Selected").gameObject.SetActive(true);

        }
        if (targetedEnemy == null)
        {
            if(oldtargetedEnemy!= null)
            {
                oldtargetedEnemy.transform.Find("Selected").gameObject.SetActive(false);
            }
           
        }
        if(targetedEnemy != null)
        {
            //distancetoEnemy = Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position);
            if (Vector3.Distance(new Vector3 (gameObject.transform.position.x,0, gameObject.transform.position.z), new Vector3
                (targetedEnemy.transform.position.x,0, targetedEnemy.transform.position.z)) > attackRange)
            {
                //targetedEnemy.transform.Find("Selected").gameObject.SetActive(true);
                
                moveScript.agent.SetDestination(targetedEnemy.transform.position);
                moveScript.agent.stoppingDistance = attackRange;

                Quaternion rotationToLookAt = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);
                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                rotationToLookAt.eulerAngles.y, ref moveScript.rotateVelocity, rotateSpeedForAttack * (Time.deltaTime * 5));

                transform.eulerAngles = new Vector3(0, rotationY, 0);
            }
            if (Vector3.Distance(new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z), new Vector3
                (targetedEnemy.transform.position.x, 0, targetedEnemy.transform.position.z)) <= attackRange)
            {
                
                if (heroAttackType == HeroAttackType.Ranged)
                {
                    Quaternion rotationToLookAt = Quaternion.LookRotation(new Vector3
               (targetedEnemy.transform.position.x, 0, targetedEnemy.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z));
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                rotationToLookAt.eulerAngles.y, ref moveScript.rotateVelocity, rotateSpeedForAttack * (Time.deltaTime * 5));
                    transform.eulerAngles = new Vector3(0, rotationY, 0);
                    moveScript.agent.SetDestination(transform.position);

                    if (damageCd == false)
                    {
                        
                        //moveScript.agent.stoppingDistance = 0;
                       
                        //transform.rotation = Quaternion.Slerp(transform.rotation, rotationToLookAt, rotateSpeedForAttack * Time.deltaTime);
                        //Debug.Log("Hero basic attack");
                        StartCoroutine(damageEnemies());
                    }
                }
            }
            else
            {
                if(heroAttackType == HeroAttackType.Melee)
                {
                    if (performMeleeAttack)
                    {
                        Debug.Log("A T T A C K");
                    }
                }
            }
            
        }
        rangeIndicator();
        
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Item"))
        {
            if (itemToPick == col.gameObject) 
            {
                for (int i = 0; i < inventory.itemSlots.Length; i++)
                {
                    if (!inventory.isfull[i])
                    {                      
                        inventory.itemSlots[i].GetComponent<Image>().sprite = col.gameObject.GetComponent<Image>().sprite;                      
                        inventory.isfull[i] = true;
                        if (col.gameObject.GetComponent<Item>().type == Item.itemType.movementSpeed)
                        {
                            agent.speed++;
                        }
                        Destroy(col.gameObject);
                        break;
                    }
                }
            }
        }
    }
    IEnumerator damageEnemies()
    {
        anim.SetBool("Attack", true);
        //targetedEnemy.GetComponent<EnemyCombatScript>().takeDamage(attackDamage);
        
        yield return new WaitUntil(()=>!enemyInRange);
        
        anim.SetBool("Attack", false);

    }
    /*IEnumerator damageEnemies()
    {
        anim.SetBool("Attack", true);
        //targetedEnemy.GetComponent<EnemyCombatScript>().takeDamage(attackDamage);
        
        damageCd = true;
        Debug.Log("damaged");
        yield return new WaitForSeconds(1/attackSpeed);
        damageCd = false;
        anim.SetBool("Attack", false);

    }*/
    public void basicAttack()
    {
        
        if (targetedEnemy != null) {
            Instantiate(attackObject, ProjectileTransform.transform.position, ProjectileTransform.transform.rotation);
            attackObject.GetComponent<BasicAttack>().targetTransform = targetedEnemy.transform;
        }
        else
        {
            //Destroy(attackObject);
        }
        
        attackObject.GetComponent<BasicAttack>().attackDamage = attackDamage;
        //targetedEnemy.GetComponent<EnemyCombatScript>().takeDamage(attackDamage);
    }
    void rangeIndicator()
    {
        attackRangeImage.rectTransform.sizeDelta = new Vector2(attackRange / 2, attackRange / 2);
    }
    void FindClosestEnemy()
    {
        List<GameObject> enemies = new List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
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
        
    }
}
