using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    public float speed = 4f;
    public float attackCd;
    float step;
    public float rotSpeed = 4f;
    Vector3 position;
    Vector3 targetPosition;
    Vector3 lookAtTarget;
    Quaternion EnemyRot;
    GameObject closestEnemy = null;
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
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
        transform.rotation = Quaternion.Slerp(transform.rotation, EnemyRot, rotSpeed * Time.deltaTime);
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
        transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, step);
        Attack();
    }
    public void Attack()
    {
        if (Vector3.Distance(transform.position, closestEnemy.transform.position) > 2f)
        {
            speed = 2f;
            transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, step);
        }
        else
        {
            speed = 0;
            if (closestEnemy.tag == "Player")
            {
                if ((Time.time - attackCd) > 2f)
                {
                    attackCd = Time.time;
                    //attack

                    //Debug.Log(killed);

                }
            }
        }
    }
}
