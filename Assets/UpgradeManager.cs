using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

    HealthSystem playerHealth;
    PlayerMovement playerMovement;
    public int upgradesAvailable = 0;

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
            playerHealth.maxHealth += 25;
            upgradesAvailable--;
        }
    }

    void armorUpgrade()
    {
        if (playerHealth.maxArmour < 175 && upgradesAvailable > 0)
        {
            playerHealth.maxArmour += 25;
            upgradesAvailable--;

        }
    }

    void thrustUpgrade()
    {
        if (playerMovement.thrustPower < 25 && upgradesAvailable > 0)
        {
            playerMovement.thrustPower += 5;
            playerMovement.movePower += playerMovement.thrustPower * 0.66f;
            upgradesAvailable--;
        }

    }

    void bulletUpgrade()
    {
        if(GameManager.Instance.bulletDamage<25 && upgradesAvailable > 0)
        {
            GameManager.Instance.bulletDamage += 5f;
            upgradesAvailable--;
        }

    }

    void increaseUpgradesAvl()
    {
        upgradesAvailable += 1;
    }
}
