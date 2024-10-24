using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public float thrustPower = 10f;
    public float movePower = 12f;
    public float drag = 0.98f;
    public TrailRenderer trailRenderer;  // Reference to the TrailRenderer component
    public float minVolume = 0.2f;  // Minimum volume of the audio source
    public float maxVolume = 1.0f;  // Maximum volume of the audio source
    public float maxTrailLength = 10f;  // Max trail length for which the volume is at max

    //public float oscillationDuration = 3f;
    //public float oscillationFrequency = 20f;
    //public float oscillationMagnitude = 15f;

    public Rigidbody2D rb;
    public Camera cam;
    public AudioSource audioSource;

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
        //audioSource.volume = 0.2f +(float) + (float)(0.8*Mathf.Abs(rb.velocity.magnitude));

        float currentTrailLength = Mathf.Min(trailRenderer.time * rb.velocity.magnitude, maxTrailLength);
        float normalizedTrailLength = Mathf.InverseLerp(0, maxTrailLength, currentTrailLength);
        audioSource.volume = Mathf.Lerp(minVolume, maxVolume, normalizedTrailLength);
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
