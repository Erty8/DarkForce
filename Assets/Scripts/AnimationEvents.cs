using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    Abilities abilityScript;
    public AudioSource audioSource;
    public AudioClip leftFootClip;
    public AudioClip rightFootClip;
    // Start is called before the first frame update
    void Start()
    {
        abilityScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Abilities>();
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
    void rightFoot()
    {
        audioSource.clip = rightFootClip;
        audioSource.Play();
    }
}
