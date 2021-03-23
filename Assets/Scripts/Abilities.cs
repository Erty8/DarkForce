using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    // Written by ertugrul

    [Header ("Ability 1")]
    public Image abilityImage1;
    public float cooldown1 = 3f;
    bool isCooldown1 = false;
    public KeyCode ability1;

    Vector3 position;
    public Canvas ability1Canvas;
    public Image skillshot;
    public Transform player;


    [Header("Ability 2")]
    public Image abilityImage2;
    public float cooldown2 = 3f;
    bool isCooldown2 = false;
    public KeyCode ability2;

    public Canvas ability2Canvas;
    public Image targetCircle;
    public Image rangeCircle;
    private Vector3 posUp;
    public float maxAbilitytoDistance;


    [Header("Ability 3")]
    public Image abilityImage3;
    public float cooldown3 = 3f;
    bool isCooldown3 = false;
    public KeyCode ability3;



    // Start is called before the first frame update
    void Start()
    {
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;
        skillshot.GetComponent<Image>().enabled = false;
        targetCircle.GetComponent<Image>().enabled = false;
        rangeCircle.GetComponent<Image>().enabled = false;
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Ability1();
        Ability2();
        Ability3();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //skillshot
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.collider.gameObject != this.gameObject)
            {
                posUp = new Vector3(hit.point.x, 10f, hit.point.z);
                position = hit.point;
            }
        }
        //ability1 input 
        Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
        transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, transRot.eulerAngles.z);
        ability1Canvas.transform.rotation = Quaternion.Lerp(transRot, ability1Canvas.transform.rotation, 0f);
        //ability2 input
        var hitPosDir = (hit.point - transform.position).normalized;
        float distance = Vector3.Distance(hit.point, transform.position);
        distance = Mathf.Min(distance, maxAbilitytoDistance);
        var newHitPos = transform.position + hitPosDir * distance;
        ability2Canvas.transform.position = (newHitPos);
    }

    void Ability1()
    {

        // cooldown system
        if (Input.GetKey(ability1) && isCooldown1 == false)
        {
            skillshot.GetComponent<Image>().enabled = true;
            targetCircle.GetComponent<Image>().enabled = false;
            rangeCircle.GetComponent<Image>().enabled = false;
            Debug.Log("Used ability 1");
            isCooldown1 = true;
            abilityImage1.fillAmount = 1;
        }
        if (skillshot.GetComponent<Image>().enabled == true && Input.GetMouseButton(0))
        {
            isCooldown1 = true;
            abilityImage1.fillAmount = 1;
        }
        if (isCooldown1)
        {
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;
            skillshot.GetComponent<Image>().enabled = false;
            if (abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                isCooldown1 = false;
            }
        }
    }
    void Ability2()
    {
        if (Input.GetKey(ability2) && isCooldown2 == false)
        {
            skillshot.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = true;
            rangeCircle.GetComponent<Image>().enabled = true;
            Debug.Log("Used ability 2");
            isCooldown2 = true;
            abilityImage2.fillAmount = 1;
        }
        if (isCooldown2)
        {
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;
            targetCircle.GetComponent<Image>().enabled = true;
            rangeCircle.GetComponent<Image>().enabled = true;
            if (abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }

    void Ability3()
    {
        if (Input.GetKey(ability3) && isCooldown3 == false)
        {
            Debug.Log("Used ability 2");
            isCooldown3 = true;
            abilityImage3.fillAmount = 1;
        }
        if (isCooldown3)
        {
            abilityImage3.fillAmount -= 1 / cooldown3 * Time.deltaTime;
            if (abilityImage3.fillAmount <= 0)
            {
                abilityImage3.fillAmount = 0;
                isCooldown3 = false;
            }
        }
    }
}
