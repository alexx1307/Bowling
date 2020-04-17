using System;
using UnityEngine;
public class Shooter : MonoBehaviour
{
    //prefaby
    public GameObject ballPrefab;
    public GameObject forceBar;

    //parametry
    public float forceOscillation = 1f;
    public float directionBaseOscillation = 1f;
    public float forceInfluenceOnDirectionSpeed = 1f;
    public float forceMultiplier = 1f;
    public float directionAmplitudeInDegrees = 30f;
    
    //stany
    public bool isPossibleToShoot = true;
    public bool forceAdjusting = false;
    public bool directionAdjusting = false;

    //zmienne pomocnicze
    private float force;
    private float directionInDegrees;
    private float timeSinceStartAdjusting;
    Renderer renderer;




    // Start is called before the first frame update
    void Start()
    {
        renderer = forceBar.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        determineState();
        
        if (forceAdjusting)
        {
            timeSinceStartAdjusting += Time.deltaTime;
            force = (1f + Mathf.Sin(forceOscillation * timeSinceStartAdjusting)) / 2;
        }

        if (directionAdjusting)
        {
            timeSinceStartAdjusting += Time.deltaTime;
            float oscillation = directionBaseOscillation + force * forceInfluenceOnDirectionSpeed;
            float sinValue = Mathf.Sin(oscillation * timeSinceStartAdjusting);
            directionInDegrees = sinValue * directionAmplitudeInDegrees;
        }

        DrawForceDirectionArrow();  
    }

    private void determineState()
    {
        if (isPossibleToShoot && Input.GetKeyDown(KeyCode.Space))
        {
            isPossibleToShoot = false;
            forceAdjusting = true;
            timeSinceStartAdjusting = 0f;
        }
        if (forceAdjusting && Input.GetKeyUp(KeyCode.Space))
        {
            forceAdjusting = false;
            directionAdjusting = true;
            timeSinceStartAdjusting = 0f;
        }
        if (directionAdjusting && Input.GetKey(KeyCode.Space))
        {
            directionAdjusting = false;
            isPossibleToShoot = true;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        Vector3 shootDirection = GetShootDirection();

        rb.velocity = shootDirection * force * forceMultiplier;

        force = 0f;
        directionInDegrees = 0f;
    }

    private Vector3 GetShootDirection()
    {
        return Quaternion.AngleAxis(directionInDegrees, transform.up) * transform.forward.normalized;
    }

    private void DrawForceDirectionArrow()
    {
       

        if (forceAdjusting || directionAdjusting)
        {
            renderer.enabled = true;
            renderer.material.color = Color.Lerp(Color.green, Color.red, force);
            forceBar.transform.parent.localScale = new Vector3(1f, 1f, force);
            forceBar.transform.parent.localEulerAngles = new Vector3(0f,directionInDegrees,0f); 
        } else
        {
            renderer.enabled = false;
        }
        
    }
}
