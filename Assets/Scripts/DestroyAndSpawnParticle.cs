using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAndSpawnParticle : MonoBehaviour
{
    public GameObject spawnParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "asteroid" ||  collision.tag == "Player" || collision.tag == "bulletNew")
        {
            Instantiate(spawnParticle, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        
    }
}
