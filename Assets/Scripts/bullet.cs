using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class bullet : MonoBehaviour
{
    //public GameObject hitEffect;
    public float timer = 3f;
    float t;

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
        Destroy(gameObject);

        if (collision.gameObject.tag == "Player")
        {
            HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();
            playerHealth.TakeDamage(5);
            Debug.Log("collided");
        }
    }

}
