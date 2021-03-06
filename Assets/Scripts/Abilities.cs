using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Abilities : MonoBehaviour
{
    // Written by ertugrul

    //RaycastHit hit;
    [SerializeField] private LayerMask layermask;
    Movement moveScript;
    public Attacking attacking;
    public PlayerCombat playerCombat;
    public Animator anim;
    //public NavMeshAgent agent;
    float oldSpeed;
    public bool projectileLaunch = false; 
    public bool shatterLaunch = false; 

    [Header ("Ability 1")]
    public Image abilityImage1;
    public float cooldown1 = 3f;
    bool isCooldown1 = false;
    public KeyCode ability1;
    public Transform ability1Transform;
    public Transform emptyProjectileTransform;
    public GameObject fireballObject;
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
    public Image ability2Image;
    public Image rangeCircle;
    private Vector3 posUp;
    public float maxAbilitytoDistance;
    public Transform ability2Transform;
    public GameObject emptyTransform;
    public GameObject shatterObject;
    Shatter shatterScript;
    [SerializeField] Canvas shatterCanvas;
    


    [Header("Ability 3")]
    public Image abilityImage3;
    public float cooldown3 = 3f;
    bool isCooldown3 = false;
    public KeyCode ability3;
    public GameObject iceShield;
    IceShield shieldScript;
    public float shieldDuration = 5f;

    [Header("Ability 4")] 
    public Image abilityImage4;
    [SerializeField] Image ultimateImage;
    [SerializeField] Sprite teleportSprite;
    [SerializeField] Sprite ultimateSprite;
    public Image ultimateSkillshot;
    public float cooldown4 = 3f;
    bool isCooldown4 = false;
    public KeyCode ability4;
    public GameObject ultimateObject;
    public Transform ultimateTransform;
    public Transform emptyUltimateTransform;
    Vector3 ultimatePosition;
    public Canvas ultimateCanvas;
    bool ultimatebool;
    bool canteleport = false;
    bool ultRefresh;
    bool ultimateActive;
    bool ultimateDestroyed;
    bool teleportDestroy = false;
    bool selfDestroy = false;
    public float cooldownAfterSeconds = 10f;
    public float waitbetweenUltimates = 1f;
    float ultimateCountdown ;
    int ultimateIndex = 0;

    [Header("Inventory")]
    public Inventory inventory;
    public int potionCount;
    public KeyCode potionKey;
    public float potionDuration = 10f;






    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
        //shatterTransform = emptyTransform.transform;
        moveScript = GetComponent<Movement>();
        shieldScript = GetComponent<IceShield>();
        shatterScript = GetComponent<Shatter>();
        playerCombat = GetComponent<PlayerCombat>();
        oldSpeed = moveScript.agent.speed;

        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;
        abilityImage4.fillAmount = 0;
        skillshot.GetComponent<Image>().enabled = false;
        ability2Image.GetComponent<Image>().enabled = false;
        rangeCircle.GetComponent<Image>().enabled = false;
        ultimateSkillshot.GetComponent<Image>().enabled = false;
        pControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        shieldScript = iceShield.gameObject.GetComponent<IceShield>();
        ultimateCountdown = cooldownAfterSeconds;



    }
    void FixedUpdate()
    {
        Ability1();
        Ability2();
        Ability3();
        ultimate();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //skillshot
        if (Physics.Raycast(ray, out hit, Mathf.Infinity,layermask))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
        //ability 2 pos
        if (Physics.Raycast(ray, out hit, Mathf.Infinity,layermask))
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
        ultimateCanvas.transform.rotation = Quaternion.Lerp(transRot, ultimateCanvas.transform.rotation, 0f);
        shatterCanvas.transform.rotation = Quaternion.Lerp(transRot, ultimateCanvas.transform.rotation, 0f);
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
        potion();
        if (gameObject.transform.Find("Ice Shield").gameObject.activeInHierarchy==false)
        {
            //IceShield.enemies.Clear();
            shieldScript.damageCd = false;
            //Debug.Log(IceShield.enemies.Count);
        }
        if (GameObject.Find(ultimateObject.name + "(Clone)") != null)
        {
            canteleport = true;
            ultimateActive = true;
            ultimateImage.sprite = teleportSprite;
            //Debug.Log("can teleport");
            if (Input.GetKeyDown(ability4)&&!teleportDestroy)
            {
                
                ultimateSkillshot.GetComponent<Image>().enabled = false;
                //ultimateImage.sprite = ultimateSprite;
                Debug.Log("ultimate sprite");
                transform.position = 
                    new Vector3(GameObject.Find(ultimateObject.name + "(Clone)").gameObject.transform.position.x,transform.position.y, GameObject.Find(ultimateObject.name + "(Clone)").gameObject.transform.position.z);
                //moveScript.agent.velocity = new Vector3(0, 0, 0);
                moveScript.agent.SetDestination(transform.position);
                moveScript.agent.stoppingDistance = 0;
                Destroy(GameObject.Find(ultimateObject.name + "(Clone)").gameObject);
                teleportDestroy = true;
                ultimateDestroyed = true;
                StartCoroutine(teleportEnd());
                
                //ultimateActive = false;
                if (!isCooldown4)
                {
                    abilityImage4.fillAmount = 1;
                    ultRefresh = true;
                }

            }            
        }
        else
        {
            ultimateImage.sprite = ultimateSprite;
            if (ultimateIndex >= 1 && ultimateIndex < 3) 
            {
                ultimateDestroyed = true;
                StartCoroutine(teleportEnd());
            }
            
        }
        if (ultimateDestroyed&&!teleportDestroy)
        {
            if (!isCooldown4)
            {
                //ultRefresh = true;
                //ultimateImage.sprite = ultimateSprite;
            }
        }
        if (ultRefresh&&ultimateIndex<3)
        {
            abilityImage4.fillAmount -= 1 / waitbetweenUltimates * Time.deltaTime;
        }


    }

    void Ability1()
    {

        // cooldown system
        if (Input.GetKey(ability1) && isCooldown1 == false)
        {
            skillshot.GetComponent<Image>().enabled = true;
            ability2Image.GetComponent<Image>().enabled = false;
            rangeCircle.GetComponent<Image>().enabled = false;
            Debug.Log("Used ability 1");
            
            abilityImage1.fillAmount = 1;
        }
        if (skillshot.GetComponent<Image>().enabled == true && Input.GetMouseButton(0))
        {
            //pControl.SetTurnPosition();
            //pControl.turn();
            StartCoroutine(projectileTransform());
            Quaternion rotationToLookAt = Quaternion.LookRotation(position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y,
            ref moveScript.rotateVelocity, 0);
            transform.eulerAngles = new Vector3(0, rotationY, 0);

            moveScript.agent.SetDestination(transform.position);
            moveScript.agent.stoppingDistance = 0;
            ultimatebool = false;
            StartCoroutine(animateFireball());
            
            skillshot.GetComponent<Image>().enabled = false;
            abilityImage1.fillAmount = 0;

            /*Quaternion rotationtoLookat = Quaternion.LookRotation(position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationtoLookat.eulerAngles.y, ref moveScript.rotateVelocity, 0);
            transform.eulerAngles = new Vector3(0, rotationY, 0);
            moveScript.agent.SetDestination(transform.position);
            moveScript.agent.stoppingDistance = 0;*/
           

        }
        if (skillshot.GetComponent<Image>().enabled == true && (Input.GetMouseButton(1)
            ||Input.GetKey(ability2) || Input.GetKey(ability3) || Input.GetKey(ability4)))
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
            
            ability2Image.GetComponent<Image>().enabled = true;
            rangeCircle.GetComponent<Image>().enabled = true;
            skillshot.GetComponent<Image>().enabled = false;
            Debug.Log("Used ability 2");
            
            abilityImage2.fillAmount = 1;
        }
        if (ability2Image.GetComponent<Image>().enabled == true && Input.GetMouseButton(0))
        {
            StartCoroutine(shatterTransform());
            emptyTransform.transform.position = ability2Transform.transform.position;
    
            
            Quaternion rotationToLookAt = Quaternion.LookRotation(position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y,
            ref moveScript.rotateVelocity, 0);

            transform.eulerAngles = new Vector3(0, rotationY, 0);

            moveScript.agent.SetDestination(transform.position);
            moveScript.agent.stoppingDistance = 0;
            //moveScript.agent.speed = 0;
            StartCoroutine(animateShatter());
            //isCooldown2 = true;
            abilityImage2.fillAmount = 0;
            ability2Image.GetComponent<Image>().enabled = false;
        }
        if (ability2Image.GetComponent<Image>().enabled == true && (Input.GetMouseButton(1)
            || Input.GetKey(ability1) || Input.GetKey(ability3) || Input.GetKey(ability4)))
        {
            abilityImage2.fillAmount = 0;
            ability2Image.GetComponent<Image>().enabled = false;
        }
        if (isCooldown2)
        {
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;
            ability2Image.GetComponent<Image>().enabled = false;
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
            StartCoroutine(castIceShield());
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
    void ultimate()
    {

        // cooldown system
        if (Input.GetKey(ability4) && isCooldown4 == false&& canteleport ==false)
        {
            ultimateSkillshot.GetComponent<Image>().enabled = true;
            skillshot.GetComponent<Image>().enabled = false;
            ability2Image.GetComponent<Image>().enabled = false;
            rangeCircle.GetComponent<Image>().enabled = false;
            Debug.Log("Used ability 1");

            //abilityImage4.fillAmount = 1;
        }
        if (ultimateSkillshot.GetComponent<Image>().enabled == true && Input.GetMouseButton(0))
        {
            ultimateSkillshot.GetComponent<Image>().enabled = false;
            StartCoroutine(projectileTransform());
            Quaternion rotationToLookAt = Quaternion.LookRotation(position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y,
            ref moveScript.rotateVelocity, 0);
            transform.eulerAngles = new Vector3(0, rotationY, 0);

            moveScript.agent.SetDestination(transform.position);
            moveScript.agent.stoppingDistance = 0;
            ultimatebool = true;
            StartCoroutine(animateFireball());
            /*ultimateCD();
            ultimateIndex++;
            Debug.Log(ultimateIndex);
            ultimateCountdown += cooldownAfterSeconds;*/
            
            

                      
        }
        if (ultimateSkillshot.GetComponent<Image>().enabled == true && (Input.GetMouseButton(1)
            || Input.GetKey(ability2) || Input.GetKey(ability3) || Input.GetKey(ability1)))
        {
            abilityImage4.fillAmount = 0;
            ultimateSkillshot.GetComponent<Image>().enabled = false;
        }
        if (isCooldown4)
        {
            //Debug.Log("ultimate cooldown");
            abilityImage4.fillAmount -= 1 / cooldown4 * Time.deltaTime;
            ultimateSkillshot.GetComponent<Image>().enabled = false;
            if (abilityImage4.fillAmount <= 0)
            {
                abilityImage4.fillAmount = 0;
                isCooldown4 = false;
            }
        }
        if (ultimateIndex == 3)
        {
            isCooldown4 = true;
            abilityImage4.fillAmount = 1;
            StartCoroutine(ultIndexreset());
            //ultimateIndex = 0;
        }
    }
    IEnumerator animateFireball()
    {
        Debug.Log("animated fireball");
        //canSkillshot = false;
        anim.SetBool("Fireball", true);

        yield return new WaitForSeconds(0.4f);

        anim.SetBool("Fireball", false);
        
    }
    IEnumerator animateShatter()
    {
        Debug.Log("animated shatter");
        //canSkillshot = false;
        anim.SetBool("Shatter", true);

        yield return new WaitForSeconds(0.5f);

        anim.SetBool("Shatter", false);
        //moveScript.agent.speed = oldSpeed;
        //Debug.Log(moveScript.agent.speed);
    }
    IEnumerator animateUltimate()
    {
        Debug.Log("animated ultimate");
        //canSkillshot = false;
        anim.SetBool("Ultimate", true);

        yield return new WaitForSeconds(0.4f);

        anim.SetBool("Ultimate", false);

    }
    IEnumerator castIceShield()
    {
        iceShield.gameObject.SetActive(true);
        yield return new WaitForSeconds(shieldDuration);
        //iceShield.gameObject.SetActive(false);

    }


    IEnumerator projectileTransform()
    {
        yield return new WaitUntil(() => projectileLaunch == true);
        
        if (ultimatebool)
        {
            emptyProjectileTransform.transform.position = ultimateTransform.transform.position;
        }
        else
        {
            emptyProjectileTransform.transform.position = ability1Transform.transform.position;
        }
        
        emptyProjectileTransform.transform.rotation = ability1Canvas.transform.rotation;
        projectileLaunch = false;

    }
    IEnumerator shatterTransform()
    {
        yield return new WaitUntil(() => shatterLaunch == true);

        emptyProjectileTransform.transform.position = ability2Transform.transform.position;
        emptyProjectileTransform.transform.rotation = shatterCanvas.transform.rotation;
        shatterLaunch = false;

    }
    /*IEnumerator projectileTransform()
    {
        
        yield return new WaitForSeconds(0.3f);
        emptyProjectileTransform.transform.position = ability1Transform.transform.position;
        emptyProjectileTransform.transform.rotation = ability1Canvas.transform.rotation;

    }*/
    IEnumerator ultimateCooldown()
    {

        yield return new WaitForSeconds(cooldownAfterSeconds);
        isCooldown4 = true;
        abilityImage4.fillAmount = 1;
        
        

    }
    IEnumerator teleportEnd()
    {

        yield return new WaitForSeconds(waitbetweenUltimates);
        canteleport = false;
        teleportDestroy = false;
        ultRefresh = false;
        

    }
    IEnumerator ultIndexreset()
    {

        yield return new WaitForSeconds(1f);
        ultimateIndex = 0;

    }
    public void castFireball()
    {
        //projectileLaunch = true;
        if (!ultimatebool) 
        {
            isCooldown1 = true;
            abilityImage1.fillAmount = 1;
            Instantiate(fireballObject, emptyProjectileTransform.transform.position, emptyProjectileTransform.transform.rotation); 
        }
        else
        {
            if (ultimateIndex == 0)
            {
                StartCoroutine(ultimateCooldown());
                //ultimateCD();
            }
            //ultimateImage.sprite = teleportSprite;
            ultimateIndex++;
            Debug.Log(ultimateIndex);
            //ultimateCountdown += cooldownAfterSeconds;
            Instantiate(ultimateObject, emptyProjectileTransform.transform.position, emptyProjectileTransform.transform.rotation);
        }
        
    }
    public void castShatter()
    {
        Instantiate(shatterObject, emptyProjectileTransform.transform.position, emptyProjectileTransform.transform.rotation);
        isCooldown2 = true;
        abilityImage2.fillAmount = 1;
    }
    /*public void castShatter()
    {
        Instantiate(shatterObject, emptyTransform.transform.position, Quaternion.Euler(0, 0, 0));
        isCooldown2 = true;
        abilityImage2.fillAmount = 1;
    }
    */
    void ultimateCD()
    {
        ultimateCountdown -= Time.deltaTime;
        Debug.Log(ultimateCountdown);
        if (ultimateCountdown == 0)
        {
            isCooldown4 = true;
            abilityImage4.fillAmount = 1;
        }
    }
    public void potion()
    {
        
        if (Input.GetKeyDown(potionKey)&& potionCount>0)
        {
            Debug.Log("used potion");
            StartCoroutine(usePotion());
            potionCount--;
            inventory.usePotion();
            
        }
    }
    public void numPotion()
    {

        
            Debug.Log("used potion");
            StartCoroutine(usePotion());
            //potionCount--;
            inventory.usePotion();

        
    }
    public IEnumerator usePotion()
    {
        playerCombat.potion = true;
        yield return new WaitForSeconds(potionDuration);
        playerCombat.potion = false;
    }
   

}
