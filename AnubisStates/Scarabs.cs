using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarabs : Attack {

    Animator scarabAnim;
    public AudioManager sound { get { return owner.soundControl; } private set { } }
    float duration = 6f;
    float startDelay = 1.5f;
    Collider[] damageBox = new Collider[3];
    float counter;

    Transform[] models = new Transform[3];

    ParticleSystem[] innerEmitters;
    ParticleSystem[] middleEmitters;
    ParticleSystem[] outerEmitters;
    bool finished;

    public override void Enter()
    {
       // owner.warning.text = "He's summoning his scarabs, find high ground!!";
        finished = false;
        base.Enter();
        sound.ChangeSFX(sound.clips[3]);
        //owner.warning.gameObject.SetActive(true);
        scarabAnim = owner.scarabs.GetComponent<Animator>();
        models[0] = owner.scarabs.GetChild(0);
        models[1] = owner.scarabs.GetChild(1);
        models[2] = owner.scarabs.GetChild(2);

        innerEmitters = models[0].GetComponentsInChildren<ParticleSystem>();
        middleEmitters = models[1].GetComponentsInChildren<ParticleSystem>();
        outerEmitters = models[2].GetComponentsInChildren<ParticleSystem>();

        counter = 0f;
        damageBox[0] = models[0].GetComponent<Collider>();
        damageBox[1] = models[1].GetComponent<Collider>();
        damageBox[2] = models[2].GetComponent<Collider>();


        owner.anim.SetTrigger("Scarabs");
        StartCoroutine(move(1.2f, startDelay));
    }

    private void Update()
    {
        counter += Time.deltaTime;
                
     
        if(counter > startDelay)
        {
            damageBox[0].enabled = !damageBox[0].enabled;
            if(counter > startDelay * 2)
                damageBox[1].enabled = !damageBox[1].enabled;
            if(counter > startDelay * 3)
                damageBox[2].enabled = !damageBox[2].enabled;

            if (counter >= duration && finished == false)
            {
                
                StartCoroutine(move(-1.2f, .5f));
                finished = true;
            }
        }
    }

    IEnumerator move (float distance, float speed)
    {
            
        
        Tweener tweener = models[0].MoveTo(new Vector3(models[0].position.x, models[0].position.y + distance, models[0].position.z)
            , speed, EasingEquations.Linear);
        while(tweener != null)
        {
            yield return null;
        }
        foreach(ParticleSystem ps in innerEmitters)
        {
            ps.Play();
        }

        Tweener tweener2 = models[1].MoveTo(new Vector3(models[1].position.x, models[1].position.y + distance, models[1].position.z)
          , speed, EasingEquations.Linear);
        while (tweener2 != null)
        {
            yield return null;
        }
        

        foreach (ParticleSystem ps in middleEmitters)
        {
            ps.Play();
        }

        Tweener tweener3 = models[2].MoveTo(new Vector3(models[2].position.x, models[2].position.y + distance, models[2].position.z)
       , speed, EasingEquations.Linear);
        while (tweener3 != null)
        {
            yield return null;
        }

        foreach (ParticleSystem ps in outerEmitters)
        {
            ps.Play();
        }


        if (counter >= duration)
        {
            owner.ChangeState<Idle>();
        }

    }



    public override void Exit()
    {

       
        damageBox[0].enabled = false;
        damageBox[1].enabled = false;
        damageBox[2].enabled = false;      
        base.Exit();
    }


}
