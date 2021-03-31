using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    // Written by ertugrul
    RaycastHit hit;
    Movement moveScript;
    public Animator anim;
    [Header ("Ability 1")]
    public Image abilityImage1;
    public float cooldown1 = 3f;
    bool isCooldown1 = false;
    public KeyCode ability1;
    public Transform ability1Transform;
    public GameObject ability1object;
    PlayerController pControl;
    

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
    public Transform ability2Transform;
    public GameObject ability2object;


    [Header("Ability 3")]
    public Image abilityImage3;
    public float cooldown3 = 3f;
    bool isCooldown3 = false;
    public KeyCode ability3;



    // Start is called before the first frame update
    void Start()
    {
        
        moveScript = GetComponent<Movement>();
        
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;
        skillshot.GetComponent<Image>().enabled = false;
        targetCircle.GetComponent<Image>().enabled = false;
        rangeCircle.GetComponent<Image>().enabled = false;
        pControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
           
    }
    void FixedUpdate()
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
        //ability 2 pos
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject != this.gameObject)
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


    // Update is called once per frame
    void Update()
    {
        
        
        
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
            
            abilityImage1.fillAmount = 1;
        }
        if (skillshot.GetComponent<Image>().enabled == true && Input.GetMouseButton(0))
        {
            //pControl.SetTurnPosition();
            //pControl.turn();
            
            Quaternion rotationToLookAt = Quaternion.LookRotation(position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y,
            ref moveScript.rotateVelocity, 0);

            transform.eulerAngles = new Vector3(0, rotationY, 0);

            moveScript.agent.SetDestination(transform.position);
            moveScript.agent.stoppingDistance = 0;
            StartCoroutine(animateFireball());
            Instantiate(ability1object, ability1Transform.transform.position, ability1Transform.transform.rotation);

            isCooldown1 = true;
            abilityImage1.fillAmount = 1;
            /*Quaternion rotationtoLookat = Quaternion.LookRotation(position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationtoLookat.eulerAngles.y, ref moveScript.rotateVelocity, 0);
            transform.eulerAngles = new Vector3(0, rotationY, 0);
            moveScript.agent.SetDestination(transform.position);
            moveScript.agent.stoppingDistance = 0;*/
            //GameObject projectileObject = Instantiate(ability1object);
            //projectileObject.transform.position = ability1Transform.transform.position;
            //Instantiate(ability1object, ability1Transform.transform.position, Quaternion.Euler(-90, Quaternion.identity.y, -ability1Canvas.transform.eulerAngles.y));

        }
        if (skillshot.GetComponent<Image>().enabled == true && Input.GetMouseButton(1)||Input.GetKey(ability2))
        {
            abilityImage1.fillAmount = 0;
            skillshot.GetComponent<Image>().enabled = false;
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
            
            targetCircle.GetComponent<Image>().enabled = true;
            rangeCircle.GetComponent<Image>().enabled = true;
            skillshot.GetComponent<Image>().enabled = false;
            Debug.Log("Used ability 2");
            
            abilityImage2.fillAmount = 1;
        }
        if (targetCircle.GetComponent<Image>().enabled == true && Input.GetMouseButton(0))
        {
            Instantiate(ability2object, ability2Transform.transform.position, Quaternion.Euler(0, 0, 0));
            isCooldown2 = true;
            abilityImage2.fillAmount = 1;
        }
        if (targetCircle.GetComponent<Image>().enabled == true && Input.GetMouseButton(1))
        {
            abilityImage2.fillAmount = 0;
            targetCircle.GetComponent<Image>().enabled = false;
        }
        if (isCooldown2)
        {
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;
            targetCircle.GetComponent<Image>().enabled = false;
            rangeCircle.GetComponent<Image>().enabled = false;
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
            Debug.Log("Used ability 3");
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
    IEnumerator animateFireball()
    {
        Debug.Log("animated");
        //canSkillshot = false;
        anim.SetBool("Fireball", true);

        yield return new WaitForSeconds(1f);

        anim.SetBool("Fireball", false);
    }
}
