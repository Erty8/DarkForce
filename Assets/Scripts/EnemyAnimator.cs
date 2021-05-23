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
        aiScript = gameObject.GetComponentInParent<Enemy_AI>();
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
        //aiScript.test();
        aiScript.dealDamage();
        aiScript.attackIndex++;
    }
    void canWalk()
    {
        aiScript.canWalk();
    }
    void cannotWalk()
    {
        aiScript.cannotWalk();
        
    }
    void canRotate()
    {
        aiScript.canRotate();
    }
    void cannotRotate()
    {
        aiScript.cannotRotate();
    }
    void spawnSpikes()
    {
        aiScript.castSpikes(aiScript.spikeWaveCount,aiScript.timeBetweenSpikeWaves);
    }
    void crash()
    {
        transform.parent.transform.Translate(0, 0, 20);
    }
    void prepare()
    {
        aiScript.skillshotCanvas.SetActive(true);
    }
}
