using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSmash : Attack {

    float startDelay = 1.5f;
    float counter;
    public AudioManager sound { get { return owner.soundControl; } private set { } }
    public override void Enter()
    {
        counter = 0;
        base.Enter();
        sound.ChangeSFX(sound.clips[1]);                
        anim.SetTrigger("SmashOnce");
        owner.rightArmWeapon.enabled = true;
        owner.leftArmWeapon.enabled = true;
       // StartCoroutine(AttackState());
    }


    private void Update()
    {

        counter += Time.deltaTime;


        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack") && counter >= startDelay)
        {
            owner.ChangeState<Idle>();
        }
        //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack"));
    }

    public override void Exit()
    {
        owner.rightArmWeapon.enabled = false;
        owner.leftArmWeapon.enabled = false;
        base.Exit();
    }


}
