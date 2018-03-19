using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSpin : Attack {

    float startDelay = 1.5f;
    float counter;
    public AudioManager sound { get { return owner.soundControl; } private set { } }
    public override void Enter()
    {
        counter = 0;
        base.Enter();
        sound.ChangeSFX(sound.clips[6]);
        anim.SetTrigger("TwoHandStrike");
        owner.leftArmWeapon.enabled = true;
        owner.rightArmWeapon.enabled = true;
        // StartCoroutine(AttackState());
    }


    private void Update()
    {

        counter += Time.deltaTime;


        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("TwoHandSmash") && counter >= startDelay)
        {
            owner.ChangeState<Idle>();
        }
        //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack"));
    }

    public override void Exit()
    {
        owner.leftArmWeapon.enabled = false;
        owner.rightArmWeapon.enabled = false;
        base.Exit();
    }


}
