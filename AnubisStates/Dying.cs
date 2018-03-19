using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dying : State {


    public AudioManager sound { get { return owner.soundControl; } private set { } }

    public override void Enter()
    {
        base.Enter();
        sound.ChangeSFX(sound.clips[8]);
        owner.anim.SetBool("Dying", true);
        StartCoroutine(Fall());
    }

    IEnumerator Fall()
    {

        Tweener tweener = owner.gameObject.transform.MoveTo(new Vector3(owner.gameObject.transform.position.x, owner.gameObject.transform.position.y - 25f, owner.gameObject.transform.position.z)
           , 1.5f, EasingEquations.EaseInExpo);
        while (tweener != null)
        {
            yield return null;
        }
    }




    public override void Exit()
    {
        base.Exit();
    }

}
