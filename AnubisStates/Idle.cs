using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State {


    public Transform player { get { return owner.player; } }
    [SerializeField]
    float rotationSpeed = 50;
    [SerializeField]
    float angleToTrack = 45;
    [SerializeField]
    int[] stageOnePattern;
    [SerializeField]
    int[] stageTwoPattern;
    [SerializeField]
    int[] stageThreePattern;
    [SerializeField]
    int[] stageFourPattern;


    [SerializeField]
    float timeBetweenAttacks = 5;


    private Vector3 target;
    private Quaternion targetRot;
    private float angle;
    private Quaternion current;

    private int attackIndex = -1;
    private float attackCD;
    private bool turning;



    public override void Enter()
    {
        if(attackIndex == -1)
            timeBetweenAttacks = 2.5f;

        attackIndex++;
        //if (attackIndex >= stageOnePattern.Length)
        //    attackIndex = 0;
        attackCD = timeBetweenAttacks;
        base.Enter();
    }

    private void Update()
    {
        if(owner.CurrentState != this)
        {
            return;
        }      

        TrackPlayer();
        attackCD -= Time.deltaTime;

        if (attackCD <= 0 && !turning)
            GetNextAttack(attackIndex);
    }

    private void TrackPlayer()
    {
        target = player.position - transform.position;

        float angle = Vector3.Angle(target, this.transform.forward);
        float dotProduct = Vector3.Dot(transform.right, target);

        if (angle > angleToTrack && dotProduct > 0)
        {

            StartCoroutine(RotateMe(Vector3.up * angleToTrack, rotationSpeed));
            turning = true;

        }
        else if (angle > angleToTrack && dotProduct < 0)
        {
            StartCoroutine(RotateMe(Vector3.up * -angleToTrack, rotationSpeed));
            turning = true;
        }
        else
            turning = false;
    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (float t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

    //public void StartLocus()
    //{
    //    owner.ChangeState<LocusSwarm>();
    //}

    //public void StartSmash()
    //{
    //    owner.ChangeState<MeleeSmash>(); 
    //}

    private void GetNextAttack(int index)
    {

        if (owner.CurrentHealth > 75)
        {
            switch (stageOnePattern[index])
            {
                case 0:
                    attackIndex = 0;
                    owner.ChangeState<Vulnerable>();
                    break;
                case 1:
                    owner.ChangeState<MeleeSmash>();
                    break;
                case 2:
                    owner.ChangeState<LocusSwarm>();
                    break;
                case 3:
                    owner.ChangeState<Scarabs>();
                    break;
                case 4:
                    owner.ChangeState<RocksFalling>();
                    break;
                case 5:
                    owner.ChangeState<SummonMummies>();
                    break;
                case 6:
                    owner.ChangeState<MeleeSpin>();
                    break;
            }            
        }
        else if (owner.CurrentHealth <= 75 && owner.CurrentHealth > 50)
        {
            switch (stageTwoPattern[index])
            {
                case 0:
                    attackIndex = 0;
                    owner.ChangeState<Vulnerable>();
                    break;
                case 1:
                    owner.ChangeState<MeleeSmash>();
                    break;
                case 2:
                    owner.ChangeState<LocusSwarm>();
                    break;
                case 3:
                    owner.ChangeState<Scarabs>();
                    break;
                case 4:
                    owner.ChangeState<RocksFalling>();
                    break;
                case 5:
                    owner.ChangeState<SummonMummies>();
                    break;
                case 6:
                    owner.ChangeState<MeleeSpin>();
                    break;
            }
        }
        else if (owner.CurrentHealth <= 50 && owner.CurrentHealth > 25)
        {
            switch (stageThreePattern[index])
            {
                case 0:
                    attackIndex = 0;
                    owner.ChangeState<Vulnerable>();
                    break;
                case 1:
                    owner.ChangeState<MeleeSmash>();
                    break;
                case 2:
                    owner.ChangeState<LocusSwarm>();
                    break;
                case 3:
                    owner.ChangeState<Scarabs>();
                    break;
                case 4:
                    owner.ChangeState<RocksFalling>();
                    break;
                case 5:
                    owner.ChangeState<SummonMummies>();
                    break;
                case 6:
                    owner.ChangeState<MeleeSpin>();
                    break;
            }
        }
        else if (owner.CurrentHealth <= 25)
        {
            switch (stageFourPattern[index])
            {
                case 0:
                    attackIndex = 0;
                    owner.ChangeState<Vulnerable>();
                    break;
                case 1:
                    owner.ChangeState<MeleeSmash>();
                    break;
                case 2:
                    owner.ChangeState<LocusSwarm>();
                    break;
                case 3:
                    owner.ChangeState<Scarabs>();
                    break;
                case 4:
                    owner.ChangeState<RocksFalling>();
                    break;
                case 5:
                    owner.ChangeState<SummonMummies>();
                    break;
                case 6:
                    owner.ChangeState<MeleeSpin>();
                    break;
            }
        }

    }

    public override void Exit()
    {
        timeBetweenAttacks = 0.8f;
    }

}
