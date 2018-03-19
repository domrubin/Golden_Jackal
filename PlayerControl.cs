using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour, IDamageable {

    private Rigidbody rBody;
    private CapsuleCollider coll;
    private Transform cam;
    private GameObject pickaxe;

    [SerializeField]
    private float speed; //Movement speed
    private float holdSpeed;
    [SerializeField]
    private float turnspeed; //The speed the character turns while moving
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float groundDis;
    [SerializeField]
    private float health;

    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashCooldown;
    [SerializeField]
    private float dashTime;
    private bool candash;
    private bool canAttack;

    [SerializeField]
    Slider healthbar;
    [SerializeField]
    Slider secondaryHealthbar;

    public Collider weapon;
    [HideInInspector]
    public Animator animControl;

    private Vector3 directionPos;   //The direction the player is looking

    private Vector3 storeDir;   //stored direction to note where the player is loking

    float horizontal;   //horizontal input
    float vertical; //vertical input
    bool onGround = true;
    public bool canJump = true;
    private float currentHealth;
    private bool immortal;
    private float countDown;
    private float immortalTimer = .5f;
    private bool onWall;
    [HideInInspector]
    public AudioManager soundController;

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        private set
        {
            currentHealth = value;
        }
    }

    // Use this for initialization
    void Start () {
        soundController = GetComponent<AudioManager>();
        rBody = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
        animControl = GetComponent<Animator>();
        cam = Camera.main.transform;
        currentHealth = health;
        healthbar.maxValue = health;
        secondaryHealthbar.maxValue = health;
        secondaryHealthbar.value = health;
        holdSpeed = speed;
        onWall = false;
	}
	
	// Update is called once per frame
	void Update () {

        dashTime += Time.deltaTime;
        
        healthbar.value = currentHealth;
        if(secondaryHealthbar.value > currentHealth)
        {
            secondaryHealthbar.value -= Time.deltaTime * 2;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            animControl.SetTrigger("BasicAttack");
           // soundController.ChangeSFX(soundController.clips[4]);
            rBody.velocity = Vector3.zero;
        }

        if (immortal)
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0)
            {
                immortal = false;
            }
        }

    }

    void FixedUpdate()
    {
        //Player Input
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if(Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0)
        {
            soundController.ChangeSFX(soundController.clips[0], true);
        }
        else
        {
            soundController.StopAudio();
        }

        if (onWall && vertical < 0)
        {
            vertical = 0; 
        }

        storeDir = cam.right;
        //print("Horizontal = " + horizontal);
        Vector3 dwn = transform.TransformDirection(Vector3.down);

        /*if (Physics.Raycast(transform.position, dwn, groundDis))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }*/
        
        if (Input.GetButtonDown("Fire2") && (dashTime >= dashCooldown) && (canJump == true))
        {
            PlayerDash();
            //animControl.SetTrigger("Dash");
            dashTime = 0;
        }
        if (rBody.velocity.y < 0 && !canJump)
        {
            rBody.velocity += Vector3.up * Physics.gravity.y * (10f) * Time.deltaTime;
        }
        else if (rBody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rBody.velocity += Vector3.up * Physics.gravity.y * (7f) * Time.deltaTime;
        }

        if (onGround)
        {

            rBody.AddForce(((storeDir * horizontal) + (cam.forward * vertical)) * speed / Time.deltaTime, ForceMode.Force);
            //rBody.velocity = new Vector3((storeDir.x * horizontal), 0, (cam.forward.z * vertical)).normalized * speed * Time.deltaTime; 
            float animValue = Mathf.Abs(vertical) + Mathf.Abs(horizontal);

            animControl.SetFloat("Forward", animValue, .1f, Time.deltaTime);
           // animControl.speed = rBody.velocity.magnitude/speed;

            if ((canJump == true) &&(Input.GetButtonDown("Jump")))
            {
                animControl.SetBool("Jump", true);
                rBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                canJump = false;
                
            }
        }

  

        //Find a position infront of the camera
        directionPos = transform.position + (storeDir * horizontal) + (cam.forward * vertical);
        //Find the direction from that position
        Vector3 dir = directionPos - transform.position;
        dir.y = 0;

        if (horizontal != 0 || vertical != 0)
        {
            //Find the angle between where the player is looking and where he should be looking
            float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(dir));
            //...and as long as it's not 0
            if (angle != 0)
                rBody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnspeed * Time.deltaTime);
        }
    }

    public void TakeDamage(float amount, Vector3 hit)
    {

        if (!immortal)
        {        
            currentHealth -= amount;

            if(currentHealth <= 0)
            {
                //PLAY SOUND
                soundController.ChangeSFX(soundController.clips[2]);
                animControl.SetBool("IsDead", true);

            }

            if(amount >= 5)
            {
                //Debug.Log(transform.position + (Vector3.forward * .1f));
                rBody.AddExplosionForce(5000, hit, 10f);
            }

            immortal = true;
            countDown = immortalTimer;
            //Debug.Log("Player has " + currentHealth);
        }
        
    }
    
    public void PlayerDash()
    {
        rBody.AddForce(((storeDir * horizontal) + (cam.forward * vertical)) * dashSpeed / Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "ArenaWall")
        {
            ContactPoint contact = other.contacts[0];
            rBody.AddExplosionForce(1000, contact.normal, 3f);
            onWall = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "ArenaWall")
        {
            onWall = false;
        }
    }

    public void PlayWhoosh()
    {
        soundController.ChangeSFX(soundController.clips[4]);
    }

    public void TakeDamage(float amount)
    {
        throw new NotImplementedException();
    }
}
