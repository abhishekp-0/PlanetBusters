using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

    public HealthSystem playerHealth;
    public PlayerMovement playerMovement;
    public int upgradesAvailable = 0;
    public float hpUpgradeVal=25f;
    public float armorUpgradeVal = 25f;
    public float thrustUpgradeVal = 5f;
    public float bulletUpgradeVal = 5f;
    public AudioSource upgradeAudio;

    // Start is called before the first frame update
    void Start()
    {
        if (upgradeAudio == null)
        {
            upgradeAudio = GetComponent<AudioSource>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("upgradeh")]
    void hpUpgrade()
    {
        if (playerHealth.maxHealth < 175 && upgradesAvailable>0)
        {
            playerHealth.maxHealth += hpUpgradeVal;
            upgradesAvailable--;
            upgradeAudio.Play();
        }
    }
    [ContextMenu("upgradea")]
    void armorUpgrade()
    {
        if (playerHealth.maxArmour < 175 && upgradesAvailable > 0)
        {
            playerHealth.maxArmour += armorUpgradeVal;
            upgradesAvailable--;
            AudioManager.Instance.PlayUISound(0);

        }
    }
    [ContextMenu("upgradet")]

    void thrustUpgrade()
    {
        if (playerMovement.thrustPower < 25 && upgradesAvailable > 0)
        {
            playerMovement.thrustPower += thrustUpgradeVal;
            playerMovement.movePower += playerMovement.thrustPower * 0.66f;
            upgradesAvailable--;
            upgradeAudio.Play();
        }

    }
    [ContextMenu("upgradeb")]
    void bulletUpgrade()
    {
        if(GameManager.Instance.bulletDamage<25 && upgradesAvailable > 0)
        {
            GameManager.Instance.bulletDamage += bulletUpgradeVal;
            upgradesAvailable--;
            upgradeAudio.Play();
        }

    }

    void increaseUpgradesAvl()
    {
        upgradesAvailable += 1;
    }
}
