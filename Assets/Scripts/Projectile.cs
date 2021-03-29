using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Rigidbody projectilePhycs;
    public float speed;
    Vector3 castVector;
    //public GameObject player;
    //public GameObject castPos;
    void Start()
    {
        //projectilePhycs = GetComponent<Rigidbody>();
        //projectilePhycs.velocity = transform.up*speed;
        
    }

   
    void FixedUpdate()
    {
       
        //var posVector = player.transform.position;
        //var castPoint = castPos.transform.position;
        //castVector = (castPoint - posVector);
        transform.Translate(transform.forward* Time.deltaTime * speed);
        //transform.Translate(castVector* Time.deltaTime * speed);
    }
}
