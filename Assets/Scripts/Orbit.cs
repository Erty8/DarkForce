using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform target;
    public float orbitDistance = 10.0f;
    public float orbitDegreesPerSec = 180.0f;

    // Use this for initialization
    void Start()
    {

    }

    void OrbitMovement()
    {
        if (target != null)
        {
            // Keep us at orbitDistance from target
            transform.position = target.position + (transform.position - target.position).normalized * orbitDistance;
            transform.RotateAround(target.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {

        // Call from LateUpdate instead...
        //OrbitMovement();

    }
    // Call from LateUpdate if you want to be sure your
    // target is done with it's move.
    void LateUpdate()
    {

        OrbitMovement();

    }
   

    // Update is called once per frame
    /*void Update()
    {
        transform.RotateAround(player.position,Vector3.up,speed*Time.deltaTime);
    }*/
}
