using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliisonFix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionExit(Collision collision)
    {
        StartCoroutine(collisionFix());
    }
    IEnumerator collisionFix()
    {

        GetComponentInParent<Rigidbody>().isKinematic = true;
        //agent.updateRotation = false;
        yield return new WaitForSeconds(0.1f);
        GetComponentInParent<Rigidbody>().isKinematic = false;
        Debug.Log("collision fix");

    }
}
