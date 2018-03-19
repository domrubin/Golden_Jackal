using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulnerable : State {

    float timer;
    float healthOnEnter;
    public AudioManager sound { get { return owner.soundControl; } private set {  } }

    public override void Enter()
    {
        owner.anim.SetLookAtWeight(0, 0, 0, 0, 1);
        //owner.warning.text = "He's tired! I should attack his head!";

        base.Enter();
        sound.ChangeSFX(sound.clips[0]);
        owner.rightArmWeapon.enabled = false;
       // owner.warning.gameObject.SetActive(true);
        owner.anim.SetBool("Vulnerable", true);
        timer = 0f;
        healthOnEnter = owner.CurrentHealth;
        owner.weakSpot.enabled = true;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 10 || (healthOnEnter - owner.CurrentHealth) >= 25)
        {
            owner.anim.SetBool("Vulnerable", false);
            sound.ChangeSFX(sound.clips[7]);
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                owner.ChangeState<Idle>();
        }
    }

    public override void Exit()
    {

        owner.anim.SetLookAtWeight(1, .2f, .5f, 0, 1);
        owner.weakSpot.enabled = false;
        owner.rightArmWeapon.enabled = true;
        
        base.Exit();
        
    }
}
