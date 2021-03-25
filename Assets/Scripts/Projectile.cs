using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Rigidbody projectilePhycs;
    public float speed;
    void Start()
    {
        //projectilePhycs = GetComponent<Rigidbody>();
        //projectilePhycs.velocity = transform.up*speed;
        
    }

   
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
