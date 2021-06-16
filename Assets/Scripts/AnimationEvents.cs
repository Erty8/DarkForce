using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    Abilities abilityScript;
    Attacking attackScript;
    public Movement moveScript;
    public AudioSource audioSource;
    public AudioClip leftFootClip;
    public AudioClip rightFootClip;
    // Start is called before the first frame update
    void Start()
    {
        abilityScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Abilities>();
        attackScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Attacking>();
        moveScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void castFireball()
    {
        //Abilities.projectileLaunch = true;
        abilityScript.castFireball();
    }
    void basicAttack()
    {
        attackScript.basicAttack();
    }
    void projectileReady()
    {
        abilityScript.projectileLaunch = true;
    }
    void shatterReady()
    {
        abilityScript.shatterLaunch = true;
    }
    void castShatter()
    {
        abilityScript.castShatter();
    }
    void leftFoot()
    {
        audioSource.clip = leftFootClip;
        audioSource.Play();
    }
    void canMove()
    {
        moveScript.canMove = true;
    }
    void cannotMove()
    {
        moveScript.canMove = false;
    }
    void rightFoot()
    {
        audioSource.clip = rightFootClip;
        audioSource.Play();
    }
}
