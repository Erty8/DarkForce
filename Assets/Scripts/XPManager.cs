using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPManager : MonoBehaviour
{
    public PlayerCombat playerCombatScript;
    public BasicAttack basicAttackScript;
    
    public GameObject levelUpButton;
    public GameObject gainHealth;
    public GameObject gainAttackDmg;
    
    
    public int XP_Reach;    
    public int playerLevel = 0;
    public float level_Modifier = 15;

    private int current_XP;
    private int XP_Margin;
    private float boost;
    

    private bool shouldGainXP = false;
    
    public Slider XP_Bar;

    private IDictionary XP_Values = new Dictionary<string, int>()
    {
        {"Demon", 50},
        {"Kobold", 5},
    };

    private static Dictionary<string, object> levelChoices;
    

    
    // Start is called before the first frame update
    void Start()
    {
        
        levelChoices = new Dictionary<string, object>();

        Debug.Log("XPManager Start method called.");
        XP_Bar.maxValue = XP_Reach;
        current_XP = 0;        
        shouldGainXP = false;


    }

    // Update is called once per frame
    void Update()
    {
        GainXP();
    }

    public void GainXP()
    {
        //Debug.Log("GainXP() is iterating.");       

        if (shouldGainXP)
        {
            current_XP += 50;

            if (current_XP >= XP_Reach)
            {        
                LevelUP();
            }
            else if (current_XP <= XP_Reach)
            {
                XP_Bar.value = current_XP;
                Debug.Log("Didn't level up.");
                Debug.Log("Curren XP: " + current_XP);
                shouldGainXP = false;           

            }
            else
            {
                Debug.Log("Something went horribly wrong...");
            }
        }

       

    }

    //Spaghetti code incoming!
    public void ShouldGainXP()
    {           
        //Preventing destroy() method, which is embedded inside of an if/else statement in EnemyCombatScript, from triggering GainXP multiple times.
        shouldGainXP = true;
    }


    public void LevelUP()
    {
        XP_Margin = current_XP - XP_Reach;
        XP_Bar.value = XP_Margin;
        current_XP = XP_Margin;
        playerLevel += 1;
        Debug.Log("Level UP");
        Debug.Log("Current XP: " + current_XP);
        Debug.Log("Player Level: " + playerLevel);
        XP_Reach += (XP_Reach * 1/5);
        XP_Bar.maxValue = XP_Reach;

        boost = playerLevel * level_Modifier;
        levelUpButton.SetActive(true);


        shouldGainXP = false;
        
    }

    public void SeeBoost()
    {

        gainHealth.SetActive(true);
        gainHealth.GetComponentInChildren<Text>().text = "Gain +" + boost * 2 + " Health";

        gainAttackDmg.SetActive(true);
        gainAttackDmg.GetComponentInChildren<Text>().text = "Gain +" + boost + " Attack Damage";

        levelUpButton.SetActive(false);
        

    }

    public void GainHealth()
    {
        
        playerCombatScript.takeDamage(-boost * 2);        

        CloseBoost();
    }

    public void GainAttackDmg()
    {
        
        basicAttackScript.attackDamage += boost;        

        CloseBoost();
    }

    public void CloseBoost()
    {
        gainHealth.SetActive(false); 
        gainAttackDmg.SetActive(false);
    }
}
