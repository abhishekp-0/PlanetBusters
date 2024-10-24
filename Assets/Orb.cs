using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public UpgradeManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindAnyObjectByType<UpgradeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.increaseUpgradesAvl();
    }
}
