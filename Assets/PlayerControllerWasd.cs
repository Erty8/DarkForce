using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerWasd : MonoBehaviour
{
    public float MoveSpeed = 5f;
    private Rigidbody playerrigidbody;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    // Start is called before the first frame update
    void Start()
    {
        playerrigidbody = GetComponent<Rigidbody>();
         
    }

    void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * MoveSpeed;
        
    }
    void FixedUpdate()
    {
        playerrigidbody.velocity = moveVelocity;
    }
}
