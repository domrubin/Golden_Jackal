using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : StateMachine, IDamageable{

    public float health; 
    public Transform mouth;
    public ParticleSystem locusParticles;
    public Transform player;
    public Transform locus;
    public Transform scarabs;
    public Animator anim;
    public Collider weakSpot;
    public ParticleSystem smashFx;
    public Collider rightArmWeapon;
    public Collider leftArmWeapon;
    public Transform hint;
    public Text warning;
    public Transform[] rockSpawns;
    public GameObject rock;
    public Transform[] mummySpawns;
    public GameObject mummy; 


    public Transform weapon;

    [SerializeField]
    Slider healthbar;
    [SerializeField]
    Slider secondaryHealthbar;
    [HideInInspector]
    public AudioManager soundControl;
    [HideInInspector]
    public CameraShake Cam { get; private set; } 
    
    public float CurrentHealth {get; private set;}


    private bool immortal;
    private float countDown;
    private float immortalTimer = .5f;
    private float rHweight = 0;

    private void Start()
    {
        secondaryHealthbar.maxValue = health;
        secondaryHealthbar.value = health;
        CurrentHealth = health;
        healthbar.maxValue = health;
        ChangeState<Idle>();
        soundControl = GetComponent<AudioManager>();
        Cam = Camera.main.GetComponent<CameraShake>();

    }

    private void Update()
    {     


        healthbar.value = CurrentHealth;

        if (secondaryHealthbar.value > CurrentHealth)
        {
            secondaryHealthbar.value -= Time.deltaTime * 2;
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


    public void TakeDamage(float amount, Vector3 hit)
    {
        if (!immortal)
        {
            immortal = true;
            countDown = immortalTimer;

            CurrentHealth -= amount;
            anim.SetTrigger("Ouch");
            soundControl.ChangeSFX(soundControl.clips[9]);
            
        }

    }

    private void OnAnimatorIK()
    {
        
            
            anim.SetLookAtPosition(player.position);
        

        rHweight = 0;

       

        //anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rHweight);
        //anim.SetIKPosition(AvatarIKGoal.RightHand, weapon.position);

        

        //anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, rHweight);
        //anim.SetIKHintPosition(AvatarIKHint.RightElbow, hint.position);
        

        //if (playerTempPos != null)
        //{

        //    rHweight = anim.GetFloat("RightHand");

        //  

        //    //anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rHweight);
        //    // anim.SetIKRotation(AvatarIKGoal.RightHand, playerTempPos.rotation);

        //    anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 0);
        //    anim.SetIKHintPosition(AvatarIKHint.LeftElbow, hint.position);
        //}

    }

    public void TakeDamage(float amount)
    {
        throw new NotImplementedException();
    }
}
