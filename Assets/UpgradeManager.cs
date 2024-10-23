using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

    HealthSystem playerHealth;
    PlayerMovement playerMovement;
    public int upgradesAvailable = 0;
    public float hpUpgradeVal=25f;
    public float armorUpgradeVal = 25f;
    public float thrustUpgradeVal = 5f;
    public float bulletUpgradeVal = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void hpUpgrade()
    {
        if (playerHealth.maxHealth < 175 && upgradesAvailable>0)
        {
            playerHealth.maxHealth += hpUpgradeVal;
            upgradesAvailable--;
        }
    }

    void armorUpgrade()
    {
        if (playerHealth.maxArmour < 175 && upgradesAvailable > 0)
        {
            playerHealth.maxArmour += armorUpgradeVal;
            upgradesAvailable--;

        }
    }

    void thrustUpgrade()
    {
        if (playerMovement.thrustPower < 25 && upgradesAvailable > 0)
        {
            playerMovement.thrustPower += thrustUpgradeVal;
            playerMovement.movePower += playerMovement.thrustPower * 0.66f;
            upgradesAvailable--;
        }

    }

    void bulletUpgrade()
    {
        if(GameManager.Instance.bulletDamage<25 && upgradesAvailable > 0)
        {
            GameManager.Instance.bulletDamage += bulletUpgradeVal;
            upgradesAvailable--;
        }

    }

    void increaseUpgradesAvl()
    {
        upgradesAvailable += 1;
    }
}
