using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isfull;
    public GameObject[] itemSlots;
    public GameObject[] potionSlots;
    private List<Item> itemList;
    public Inventory()
    {
        itemList = new List<Item>();
    }
    public void addItem(Item item)
    {
        itemList.Add(item);
    }
}
