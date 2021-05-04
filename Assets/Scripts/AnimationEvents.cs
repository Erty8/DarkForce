using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    Abilities abilityScript;
    Attacking attackScript;
    public AudioSource audioSource;
    public AudioClip leftFootClip;
    public AudioClip rightFootClip;
    // Start is called before the first frame update
    void Start()
    {
        abilityScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Abilities>();
        attackScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Attacking>();
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
        Abilities.projectileLaunch = true;
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
        Movement.canMove = true;
    }
    void cannotMove()
    {
        Movement.canMove = false;
    }
    void rightFoot()
    {
        audioSource.clip = rightFootClip;
        audioSource.Play();
    }
}
