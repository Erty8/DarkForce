using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public bool[] isfull;
    public GameObject[] itemSlots;
    public GameObject[] potionSlots;
    public List<GameObject> itemList;
    private void Update()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
           if (!isfull[i])
            {
                //itemSlots[i].GetComponent<Image>().sprite = null;
            }
           
        }
        /*for (int i = 1; i <= itemList.Count ; i++)
        {
            isfull[i] = true;           
        }*/
        for (int i = 0; i < itemSlots.Length  ; i++)
        {
            if (itemList.ElementAtOrDefault(i) == null)
            {
                isfull[i] = false;
                itemSlots[i].gameObject.GetComponent<Image>().sprite = null;
            }
            else
            {
                isfull[i] = true;
            }
            
        }
    }
    public Inventory()
    {
        itemList = new List<GameObject>();
    }
    public void addItem( int index, GameObject gameObject)
    {
        gameObject.SetActive(false);
        itemList.Insert(index,gameObject);
        
    }
    public void dropItem(int index)
    {
        Instantiate(itemList[index]);
        itemList.RemoveAt(index);        
    }
    public void usePotion()
    {
        foreach (GameObject item in itemList)
        {
            Debug.Log("Looking for potion");
            if (item.GetComponent<Item>().type == Item.itemType.healthPotion)
            {
                //isfull[itemList.IndexOf(item)] = false;
                itemList.Remove(item);
                
                break;
            }
        }
    }
}
