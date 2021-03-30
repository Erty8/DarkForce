using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 targetPosition;
    public Vector3 lookAtTarget;
    public Quaternion playerRot;
    public float rotSpeed = 1f;
    public float moveSpeed = 10f;
    public float rotateVelocity;
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
    public void SetTargetPosition()
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
    public void SetTurnPosition()
    {
        moving = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            targetPosition = hit.point;
            //this.transform.LookAt(targetPosition);
            lookAtTarget = new Vector3(targetPosition.x - transform.position.x, transform.position.y, targetPosition.z - transform.position.z);
            playerRot = Quaternion.LookRotation(lookAtTarget);
            moving = false;
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
    public void turn()
    {
        StartCoroutine(turning());
        /*transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotSpeed * Time.deltaTime);
        moveSpeed = 0f;
        if (transform.rotation == playerRot)
        {
            moveSpeed = 6f;
        }*/
    }
    IEnumerator turning()
    {
        rotSpeed = 7f;
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotSpeed * Time.deltaTime);
        //moveSpeed = 0f;
        yield return new WaitForSeconds(0.3f);
        //moveSpeed = 6f;
        rotSpeed = 5f;


    }

}
