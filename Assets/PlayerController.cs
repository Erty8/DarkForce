using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 targetPosition;
    Vector3 lookAtTarget;
    Quaternion playerRot;
    public float rotSpeed = 1f;
    public float moveSpeed = 10f;
    bool moving = false;

    // stop rotating
    protected void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(1))
        {
            SetTargetPosition();
        }
       
        
    }
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        if (moving)
        {
            Move();
        }
    }
    void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit, 100))
        {
            targetPosition = hit.point;
            //this.transform.LookAt(targetPosition);
            lookAtTarget = new Vector3(targetPosition.x - transform.position.x, transform.position.y, targetPosition.z - transform.position.z);
            playerRot = Quaternion.LookRotation(lookAtTarget);
            moving = true;
        }
    }
    void Move()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition,  moveSpeed* Time.deltaTime);
        if (transform.position==targetPosition) 
        {

            moving = false;
                }
        }
   
}
