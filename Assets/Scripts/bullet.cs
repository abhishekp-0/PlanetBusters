using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class bullet : MonoBehaviour
{
    //public GameObject hitEffect;
    public float timer = 3f;
    float t;
    public float Planet1HPReduction = 0.1f;

    public GameObject particle;
    private void Start()
    {
        t = timer;
    }
    public void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
            timer += t;
        }
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //if (collision.gameObject.tag == "Player")
        //{
        //    HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();
        //    playerHealth.TakeDamage(5);
        //    Debug.Log("collided");
        //}
        if(collision.tag == "asteroid" || collision.tag == "Planet")
        {
            //PlanetHP php = FindObjectOfType<PlanetHP>();
            //php.HP.value -= Planet1HPReduction;
            Destroy(gameObject);


            
        }
    }

}
