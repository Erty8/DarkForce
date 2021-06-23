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
    GameObject droppedItem;
    public Abilities abilityScript;
    void Start()
    {
        abilityScript = GetComponent<Abilities>();

        for (int i = 0; i < itemSlots.Length; i++)
        {
            
            if (i == 0)
            {
                itemSlots[i].GetComponentInChildren<Button>().onClick.AddListener(delegate { dropItem(0); });
            }
            if (i == 1)
            {
                itemSlots[i].GetComponentInChildren<Button>().onClick.AddListener(delegate { dropItem(1); });
            }
            if (i == 2)
            {
                itemSlots[i].GetComponentInChildren<Button>().onClick.AddListener(delegate { dropItem(2); });
            }

            //itemSlots[i].GetComponentInChildren<Button>().onClick.AddListener(test);
            //itemSlots[i].GetComponentInChildren<Button>().onClick.AddListener;

            Debug.Log("slot buttons");
        }
    }
    private void Update()
    {
        
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (isfull[i] == false)
            {
                itemSlots[i].GetComponent<Image>().enabled = false;
            }
            else
            {
                itemSlots[i].GetComponent<Image>().enabled = true;
            }
            if (itemList.ElementAtOrDefault(i) == null)
            {
                isfull[i] = false;
                itemSlots[i].gameObject.GetComponent<Image>().sprite = null;
            }
            else
            {
                isfull[i] = true;
            }

            if (isfull[i])
            {
                itemSlots[i].GetComponent<Image>().sprite = itemList[i].gameObject.GetComponent<Image>().sprite;
                if (Input.GetKeyDown((i + 1).ToString()))
                {
                    useActiveSkill(i);
                }
            }
            

        }
        /*for (int i = 1; i <= itemList.Count ; i++)
        {
            isfull[i] = true;           
        }*/
        
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
        Debug.Log("item dropped");
        itemList[index].SetActive(true);
        droppedItem = Instantiate(itemList[index], transform.position, transform.rotation);
        droppedItem.GetComponent<Item>().random = false;
        droppedItem.transform.localScale = new Vector3(6, 6, 6);
        droppedItem.GetComponent<Item>().type = itemList[index].GetComponent<Item>().type;
        itemList[index].GetComponent<Item>().itemDropEffect(gameObject);
        Destroy(itemList[index]);
        itemList.RemoveAt(index);        
    }
    public void destroyItem(int index)
    {
        Debug.Log("item used");       
        Destroy(itemList[index]);
        itemList.RemoveAt(index);
    }
    public void useActiveSkill(int i)
    {
        if (itemList[i].GetComponent<Item>().type == Item.itemType.active)
        {
            itemList[i].GetComponent<Item>().activeEffect(gameObject);
            destroyItem(i);
            Debug.Log("used active item");
        }
        else if (itemList[i].GetComponent<Item>().type == Item.itemType.healthPotion)
        {
            usePotion();
            abilityScript.numPotion();
            //destroyItem(i);
            Debug.Log("used active item");
        }
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
    void test() { Debug.Log("test"); }
}
