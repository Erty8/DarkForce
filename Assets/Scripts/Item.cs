using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Linq;

public class Item : MonoBehaviour
{
    public enum itemType
    {
        active, 
        damage,
        attackSpeed,
        movementSpeed,
        maxHealth,
        healthPotion,
        manaPotion
    }
    public itemType type;
    public int amount = 1 ;
    public int itemLevel = 1;
    public bool random = true;
    [SerializeField] Sprite attackDamageSprite;
    [SerializeField] Sprite attackSpeedSprite;
    [SerializeField] Sprite movementSpeedSprite;
    [SerializeField] Sprite healthPotionSprite;
    [SerializeField] Sprite manaPotionSprite;
    [SerializeField] Sprite maxHealthSprite;
    
    void Start()
    {
        
        if (random)
        {
            type = (itemType)Random.Range(0, 7);
            itemLevel = Random.Range(1, 4);
            Debug.Log("item level= " + itemLevel);           
        }
        sprites();

    }
    void sprites()
    {
        switch (type)   
        {
            case (itemType.active):
                Debug.Log("active item");
                break;
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

        }
    }
    public void itemEffect(GameObject player)
    {
        switch (type)
        {
            case (itemType.active):
                Debug.Log("active item");
                break;
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
}
