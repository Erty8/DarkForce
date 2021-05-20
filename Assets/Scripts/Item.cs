using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Linq;
using System;

public class Item : MonoBehaviour
{
    public enum itemType
    {        
        damage,
        attackSpeed,
        movementSpeed,
        maxHealth,
        healthPotion,
        active
    }
    public enum activeType
    {
        trap,
        arena
    }
    public itemType type;
    public activeType aType;
   
    public int amount = 1 ;
    public int itemLevel = 1;
    public bool random = true;
    [SerializeField] Sprite attackDamageSprite;
    [SerializeField] Sprite attackSpeedSprite;
    [SerializeField] Sprite movementSpeedSprite;
    [SerializeField] Sprite healthPotionSprite;
    [SerializeField] Sprite manaPotionSprite;
    [SerializeField] Sprite maxHealthSprite;
    [SerializeField] Sprite trapSprite;
    [SerializeField] Sprite arenaSprite;

    [SerializeField] GameObject trapObject;
    [SerializeField] GameObject arenaObject;
    
    void Start()
    {
        
        if (random)
        {
            type = (itemType) UnityEngine.Random.Range(0, Enum.GetNames(typeof(itemType)).Length-1) ;
            
            Debug.Log("item level= " + itemLevel);           
        }
        
        
    }   
    private void Update()
    {
        sprites();
    }
    void sprites()
    {
        switch (type)   
        {            
            case (itemType.damage):
                gameObject.GetComponent<Image>().sprite = attackDamageSprite;
                break;
            case (itemType.attackSpeed):
                gameObject.GetComponent<Image>().sprite = attackSpeedSprite;
                break;
            case (itemType.healthPotion):
                gameObject.GetComponent<Image>().sprite = healthPotionSprite;
                break;
            case (itemType.movementSpeed):
                gameObject.GetComponent<Image>().sprite = movementSpeedSprite;
                break;
            case (itemType.maxHealth):
                gameObject.GetComponent<Image>().sprite = maxHealthSprite;
                break;
            case (itemType.active):
                activeSprite();
                break;
        }
        
    }
    public void itemEffect(GameObject player)
    {
        switch (type)
        {
            
            case (itemType.damage):
                player.GetComponent<Attacking>().attackDamage += 5*itemLevel;
                break;
            case (itemType.attackSpeed):
                //player.GetComponent<Attacking>().anim.SetFloat("Attack Speed",2);
                player.GetComponentInChildren<Animator>().SetFloat("Attack Speed",
                player.GetComponentInChildren<Animator>().GetFloat("Attack Speed") + (itemLevel/2f));
                break;
            case (itemType.healthPotion):
                player.GetComponent<Abilities>().potionCount += itemLevel;
                break;
            case (itemType.movementSpeed):
                player.GetComponent<NavMeshAgent>().speed += itemLevel;
                break;
            case (itemType.maxHealth):
                player.GetComponent<PlayerCombat>().health = player.GetComponent<PlayerCombat>().healthPercentage*
                    (player.GetComponent<PlayerCombat>().maxhealth + 50 * itemLevel);
                player.GetComponent<PlayerCombat>().maxhealth += 50 * itemLevel;
                           
                break;
        }
    }
    void activeSprite()
    {
        switch (aType)
        {
            case (activeType.trap):
                gameObject.GetComponent<Image>().sprite = trapSprite;
                break;
            case (activeType.arena):
                gameObject.GetComponent<Image>().sprite = arenaSprite;
                break;

        }                      
    }
    public void activeEffect()
    {
        switch (aType)
        {
            case (activeType.trap):
                Instantiate(trapObject, transform.parent.position, transform.parent.rotation);
                Debug.Log("trap placed");
                break;
            case (activeType.arena):
                Instantiate(arenaObject, transform.parent.position, transform.parent.rotation);
                break;
        }
    }
}
