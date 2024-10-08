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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //GameObject effect = Instantiate(hitEffect,transform.position,quaternion.identity);
        //Destroy(effect,5f);
        Destroy(gameObject);


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            Destroy(gameObject);

    }
}
