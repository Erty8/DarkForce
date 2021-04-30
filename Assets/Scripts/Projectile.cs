using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Rigidbody projectilePhycs;
    public float speed;
    public float destroySecond = 2f;
    public float stopSecond = 0;
    bool stop = false;
    Vector3 castVector;
    //public GameObject player;
    //public GameObject castPos;
    void Start()
    {
        //projectilePhycs = GetComponent<Rigidbody>();
        //projectilePhycs.velocity = transform.up*speed;
        if (stopSecond > 0)
        {
            StartCoroutine(stopatSecond());
        }
        
    }

   
    void FixedUpdate()
    {
        StartCoroutine(DestroyObject());
        if (!stop)
        {
            gameObject.transform.TransformDirection(Vector3.forward);
            gameObject.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }
       
        //var posVector = player.transform.position;
        //var castPoint = castPos.transform.position;
        //castVector = (castPoint - posVector);
        //transform.Translate(transform.forward* Time.deltaTime * speed);
        //transform.Translate(castVector* Time.deltaTime * speed);
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(destroySecond);
        Destroy(gameObject);
    }
    IEnumerator stopatSecond()
    {
        yield return new WaitForSeconds(stopSecond);
        stop = true;
       
    }
}
