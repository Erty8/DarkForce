using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attacking : MonoBehaviour
{
    
    public enum HeroAttackType { Melee, Ranged, Versatile};
    public HeroAttackType heroAttackType;

    [SerializeField] private Image attackRangeImage;
    public GameObject targetedEnemy = null;
    public GameObject oldtargetedEnemy = null;
    public float attackRange;
    public float attackDamage;
    public float attackSpeed = 1f;
    public float rotateSpeedForAttack;
    bool damageCd = false;

    private Movement moveScript;

    public bool basicAtkIdle = false;
    public bool isHeroAlive;
    public bool performMeleeAttack = true;
     

    
    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
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
                targetedEnemy.transform.Find("Selected").gameObject.SetActive(true);
                
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
                    if (damageCd == false)
                    {
                        Debug.Log("Hero basic attack");
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
    
    IEnumerator damageEnemies()
    {
        
        targetedEnemy.GetComponent<EnemyCombatScript>().takeDamage(attackDamage);
        
        damageCd = true;
        Debug.Log("damaged");
        yield return new WaitForSeconds(1/attackSpeed);
        damageCd = false;

    }
    void rangeIndicator()
    {
        attackRangeImage.rectTransform.sizeDelta = new Vector2(attackRange / 2, attackRange / 2);
    }
}
