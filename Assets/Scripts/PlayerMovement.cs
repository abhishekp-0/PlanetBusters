using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float thrustPower = 10f;
    public float movePower = 5f;
    public float drag = 0.98f;
    //public float oscillationDuration = 3f;
    //public float oscillationFrequency = 20f;
    //public float oscillationMagnitude = 15f;

    public Rigidbody2D rb;
    public Camera cam;

    private Vector2 mousePos;
    private float thrustInput;
    private float moveInput;
    private bool isOscillating = false;

    public float MouseOffset;

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


        if (!isOscillating) // Don't control rotation if oscillating
        {
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }
        
        float dis = Vector2.Distance(rb.position, mousePos);
        if(dis > MouseOffset && thrustInput>0)
        {
            rb.AddForce(transform.up * thrustInput * thrustPower);
        }
        
        rb.AddForce(transform.right * moveInput * movePower);
        rb.velocity *= drag;


    }
    [ContextMenu("onHit")]
    public void OnHit()
    {
        Debug.Log("coroutinestarted");
      //  StartCoroutine(RotateOscillate());
    }

    //private IEnumerator RotateOscillate()
    //{
    //    isOscillating = true;
    //    rb.velocity = Vector2.zero;
    //    float elapsedTime = 0f;


    //    while (elapsedTime < oscillationDuration)
    //    {
    //        float oscillationAngle = Mathf.Sin(elapsedTime * oscillationFrequency) * oscillationMagnitude;
    //        rb.rotation += oscillationAngle;
    //        elapsedTime += Time.fixedDeltaTime;
    //        yield return new WaitForFixedUpdate();
    //    }

    //    isOscillating = false;
    //}
}
