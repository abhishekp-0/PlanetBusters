using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{

    public HealthSystem playerHealth;
    public PlayerMovement playerMovement;
    public int upgradesAvailable = 0;
    public float hpUpgradeVal = 25f;
    public float armorUpgradeVal = 25f;
    public float thrustUpgradeVal = 5f;
    public float bulletUpgradeVal = 5f;
    public AudioSource upgradeAudio;

    public int hpu = 0;
    public int armoru = 0;
    public int thrustu = 0;
    public int bulletu = 0;

    public GameObject[] hpupgrades;
    public GameObject[] armorupgrades;
    public GameObject[] thrustupgrades;
    public GameObject[] bulletupgrades;

    public TextMeshProUGUI tmp;
    public GameObject upgradePanel;

    // Start is called before the first frame update
    void Start()
    {
        if (upgradeAudio == null)
        {
            upgradeAudio = GetComponent<AudioSource>();
        }
        upgradePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = "POINTS: " + upgradesAvailable;

        // Check for Tab key press to toggle the upgradePanel
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleUpgradePanel();
        }

    }

    public void ToggleUpgradePanel()
    {
        // Toggle the active state of the upgrade panel
        upgradePanel.SetActive(!upgradePanel.activeSelf);
    }

    [ContextMenu("upgradeh")]
    public void hpUpgrade()
    {
        if (playerHealth.maxHealth < 175 && upgradesAvailable > 0)
        {
            playerHealth.maxHealth += hpUpgradeVal;
            upgradesAvailable--;
            upgradeAudio.Play();
            hpu++;
            hpupgrades[hpu - 1].SetActive(true);
        }



    }
    [ContextMenu("upgradea")]
    public void armorUpgrade()
    {
        if (playerHealth.maxArmour < 175 && upgradesAvailable > 0)
        {
            playerHealth.maxArmour += armorUpgradeVal;
            upgradesAvailable--;
            armoru++;
            armorupgrades[armoru - 1].SetActive(true);
        }
    }
    [ContextMenu("upgradet")]

    public void thrustUpgrade()
    {
        if (playerMovement.thrustPower < 30 && upgradesAvailable > 0)
        {
            playerMovement.thrustPower += thrustUpgradeVal;
            playerMovement.movePower = playerMovement.thrustPower * 0.66f;
            upgradesAvailable--;
            upgradeAudio.Play();
            thrustu++;
            thrustupgrades[thrustu - 1].SetActive(true);
        }

    }
    [ContextMenu("upgradeb")]
    public void bulletUpgrade()
    {
        if (GameManager.Instance.bulletDamage < 25 && upgradesAvailable > 0)
        {
            GameManager.Instance.bulletDamage += bulletUpgradeVal;
            upgradesAvailable--;
            upgradeAudio.Play();
            bulletu++;
            bulletupgrades[bulletu - 1].SetActive(true);
        }

    }

    public void increaseUpgradesAvl()
    {
        upgradesAvailable += 1;
    }
}