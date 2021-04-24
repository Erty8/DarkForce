using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator anim;
    Enemy_AI aiScript;
    // Start is called before the first frame update
    void Start()
    {
        aiScript = gameObject.GetComponent<Enemy_AI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    void recover()
    {
        anim.SetBool("takeHit", false);
    }
    void attack()
    {
        aiScript.dealDamage();
    }
}
