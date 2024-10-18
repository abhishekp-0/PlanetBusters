using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class shooting : MonoBehaviour
{
    public Transform firePoint; public GameObject bulletPrefab;
    public float bulletForce = 20f;

    public float timer = 0.1f;
    float t;

    [Space]
    public float shakeIntensity = 1f;
    public float shakeTimer;
    // Update is called once per frame
    private void Start()
    {
        t = timer;
    }

    void Update()
    {
        
            if (Input.GetButton("Fire1"))
            {
                timer -= Time.deltaTime;
                
                if(timer < 0)
            {
                Shoot();
                timer = t;

            }

        }
        
        
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); 
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        CameraShake.instance.ShakeCamera(shakeIntensity, shakeTimer);
    }
}
