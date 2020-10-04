using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{
    [SerializeField] float thrust = 25000f;
    [SerializeField] float lift = 50f;
    [SerializeField] float rollSpeed = 1f;
    [SerializeField] float gravityMultiplier = 1f;
    [SerializeField] ParticleSystem laserParticles;
    [SerializeField] AudioClip laser;
    Rigidbody rigidBody;
    AudioSource audioSource;
    // [Tooltip("In m/s^-1")][SerializeField] float xSpeed = 200f;
    // [Tooltip ("In m/s^-1")][SerializeField] float ySpeed = 200f;
    // [Tooltip("In m")][SerializeField] float xRange = 40f;
    // [Tooltip("In m")][SerializeField] float yRange = 25f;

    // float originX;
    // float originY;

    // Start is called before the first frame update
    void Start()
    {
        // originX = transform.localPosition.x;
        // originY = transform.localPosition.y;
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Thrust() {
        //rigidBody.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);
        if (Input.GetKey(KeyCode.Space)){
            rigidBody.AddRelativeForce(Vector3.forward * thrust * Time.deltaTime);
        }else{
            rigidBody.AddRelativeForce(Vector3.forward * 10000f * Time.deltaTime);
        }
    }

    void Shoot(){
        if (Input.GetMouseButtonDown(0)){
            if (!audioSource.isPlaying){
                audioSource.PlayOneShot(laser);
                laserParticles.Play();
            } 
        }
    }


    void processRotation() {
        rigidBody.angularVelocity = Vector3.zero;
        float horizontalThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float verticalThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float pitchThisFrame = verticalThrow * rollSpeed;
        float rollThisFrame = horizontalThrow * rollSpeed;
        transform.Rotate(pitchThisFrame, 0f, -rollThisFrame, Space.Self);
    }

    void OnCollisionEnter(Collision collision) {
        print("collided");
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;   
        rigidBody.Sleep();
    }

    // void processTraslation() {
    //     float horizontalThrow = CrossPlatformInputManager.GetAxis("Horizontal");
    //     float verticalThrow = CrossPlatformInputManager.GetAxis("Vertical");
    //     float xOffsetThisFrame = xSpeed * horizontalThrow * Time.deltaTime;
    //     float yOffsetThisFrame = ySpeed * verticalThrow * Time.deltaTime;
    //     float newXPos = transform.localPosition.x + xOffsetThisFrame;
    //     float newYPos = transform.localPosition.y + yOffsetThisFrame;
    //     float clampedXPos = Mathf.Clamp(newXPos, originX - xRange, originX + xRange);
    //     float clampedYPos = Mathf.Clamp(newYPos, originY - yRange, originY + yRange);
    //     transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    // }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        Thrust();
        processRotation();
        rigidBody.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);
    }
}
