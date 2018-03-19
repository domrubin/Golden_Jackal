using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocksFalling : Attack {

    List<GameObject> rocks = new List<GameObject>();
    public AudioManager sound { get { return owner.soundControl; } private set { } }
    int rockAmount = 15;
    float waitTime = .5f;

    float startDelay = 1.5f;

    float counter;
    public override void Enter()
    {
        counter = 0;
        base.Enter();
        sound.ChangeSFX(sound.clips[4]);
        owner.anim.SetTrigger("Rocks");
        StartCoroutine(dropRocks());
    }

    private void Update()
    {
        if (counter < startDelay)
            counter += Time.deltaTime;
        else
        {         

            if (ListIsEmpty(rocks))
            {
                owner.ChangeState<Idle>();
            }
        }
    }

    IEnumerator dropRocks()
    {

        //Create a list that'll hold numbers. 1 number for each potential spawn point
        List<int> numbers = new List<int>(owner.rockSpawns.Length);
        for (int i = 0; i < owner.rockSpawns.Length; i++)
        {
            numbers.Add(i);
        }

        //An array of 4 random numbers picked from the list of numbers. 
        //Once a number is chosen, it is removed from the list so it cannot be chosen again
        int[] randNumbers = new int[rockAmount];
        for (int i = 0; i < randNumbers.Length; i++)
        {
            int thisNumber = Random.Range(0, numbers.Count);
            randNumbers[i] = numbers[thisNumber];
            numbers.RemoveAt(thisNumber);

        }

        for (int i = 0; i < rockAmount; i++)
        {
            rocks.Add(Instantiate(owner.rock, owner.rockSpawns[randNumbers[i]].position, owner.rockSpawns[randNumbers[i]].rotation));
            //rocks[i] = Instantiate(owner.rock, owner.rockSpawns[randNumbers[i]].position, owner.rockSpawns[randNumbers[i]].rotation) as GameObject;
            yield return new WaitForSeconds(waitTime);
        }
        
    }

    private bool ListIsEmpty(List<GameObject> myList) {

        foreach(GameObject item in myList)
        {
            if(item != null)
            {
                return false; 
            }
        }

        return true;

    }

    public override void Exit()
    {
        for (int i = 0; i < rocks.Count; i++)
        {
            Destroy(rocks[i]);
            rocks.RemoveAt(i);
        }
        base.Exit();
    }

}
