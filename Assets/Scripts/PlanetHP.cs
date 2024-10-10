using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetHP : MonoBehaviour
{
    public Slider HP;
    public float HPReduction;

    public GameObject WHolePlanet;
    public GameObject particle;

    public GameObject bulletParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(HP.value <= 0)
        {
            Instantiate(particle, transform.position, transform.rotation);
            WHolePlanet.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "bulletNew")
        {
            HP.value -= HPReduction / 10;
            Instantiate(bulletParticle, collision.transform.position, Quaternion.identity);
        }
    }
}
