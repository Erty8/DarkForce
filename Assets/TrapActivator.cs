using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivator : MonoBehaviour
{
    List<int> numbers = new List<int>();
    int random;
    // Start is called before the first frame update
    void Start()
    {
        while (numbers.Count < 4)
        {
            random = Random.Range(1, 10);
            if (!numbers.Contains(random))
            {
                numbers.Add(random);
                Debug.Log("added" + random);
            }
        }
        foreach (int number in numbers)
        {
            
            transform.GetChild(number - 1).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
