using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocusSwarm : Attack
{

    public Transform locus { get { return owner.locus; } }
    public Transform locusSpawn { get { return owner.mouth; } }
    [SerializeField]
    float locusTravelSpeed = 10f;
    public ParticleSystem locusParticles { get { return owner.locusParticles; } }
    public Transform player { get { return owner.player; } }
    public AudioManager sound { get { return owner.soundControl; } private set { } }
    float delay = 2.2f; 

    bool hasPlayed;
    float counter;


    public override void Enter()
    {        

        base.Enter();
        sound.ChangeSFX(sound.clips[2]);
        hasPlayed = false;
        counter = 0;
        owner.anim.SetTrigger("Locus");
       // locusParticles.Play();
        
    }

    private void Update()
    {


        if(counter < delay)
        {
            counter += Time.deltaTime;
        }
        else if (!hasPlayed)
        {
            hasPlayed = true;
            locusParticles.Play();
        }
        if (hasPlayed)
        {
            locus.position = Vector3.MoveTowards(locus.position, player.position, locusTravelSpeed * Time.deltaTime);
            if (!locusParticles.IsAlive())
                owner.ChangeState<Idle>();
        }
    }

    public override void Exit()
    {
        base.Exit();
        locusParticles.Clear();        
    }



}
