using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isfull;
    public GameObject[] itemSlots;
    public GameObject[] potionSlots;
    public List<GameObject> itemList;
    public Inventory()
    {
        itemList = new List<GameObject>();
    }
    public void addItem( int index, GameObject gameObject)
    {
        itemList.Insert(index,gameObject);
    }
    public void dropItem(int index)
    {
        itemList.RemoveAt(index);
    }
}
