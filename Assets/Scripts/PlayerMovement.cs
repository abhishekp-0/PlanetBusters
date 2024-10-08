using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpaceshipMovement : MonoBehaviour
{
    public float thrustPower = 10f;  
    public float movePower = 5f;
    public float drag = 0.98f;       
    public Rigidbody2D rb;
    public Camera cam;

    private Vector2 mousePos;        
    private float thrustInput;       
    private float moveInput;

    void Start()
    {
        rb.drag = 0;  
    }

    void Update()
    {
        thrustInput = Input.GetAxisRaw("Vertical");  
        moveInput = Input.GetAxisRaw("Horizontal");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        
        Vector2 lookDir = mousePos - rb.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        rb.rotation = angle;

        rb.AddForce(transform.up * thrustInput * thrustPower);
        rb.AddForce(transform.right * moveInput * (movePower));

        rb.velocity *= drag;
    }
}
