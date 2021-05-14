using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum itemType
    {
        active, 
        damage,
        attackSpeed,
        movementSpeed,
        healthPotion,
        manaPotion
    }
    public itemType type;
    public int amount;
}
