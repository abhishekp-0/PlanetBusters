using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class finalPlanet : MonoBehaviour
{
    public Slider sld;
    public Slider player;
    public GameObject won;
    public GameObject loss;

    bool wonn = false;
    bool losss = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (wonn == false) {

            if (sld.value <= 0)
            {
                Invoke("Inwon", 2f);
            }
        }
        if (losss == false)
        {

            if (player.value <= 0)
            {
                Invoke("Iloss", 2f);
            }
        }

    }

    void Inwon()
    {
        won.SetActive(true);
        wonn = true;
    }
    void Iloss()
    {
        loss.SetActive(true);
        losss = true;
    }
}
