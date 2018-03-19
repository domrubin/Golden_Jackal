using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SummonMummies : Attack {

    List<GameObject> mummies = new List<GameObject>();
    public AudioManager sound { get { return owner.soundControl; } private set { } }
    int mummyAmount = 4;
    float startDelay = 1f;
    float counter;
    
    public override void Enter()
    {
        counter = 0;
        sound.ChangeSFX(sound.clips[5]);
        base.Enter();
        owner.anim.SetTrigger("Scarabs");
        SpawnMummies();
    }

    private void Update()
    {
        if (counter < startDelay)
            counter += Time.deltaTime;
        else
        {
            if (ListIsEmpty(mummies))
            {
                owner.ChangeState<Idle>();
            }
        }
    }

    public void SpawnMummies()
    {
        //Create a list that'll hold numbers. 1 number for each potential spawn point
        List<int> numbers = new List<int>(owner.mummySpawns.Length);
        for (int i = 0; i < owner.mummySpawns.Length; i++)
        {
            numbers.Add(i);
        }

        //An array of 4 random numbers picked from the list of numbers. 
        //Once a number is chosen, it is removed from the list so it cannot be chosen again
        int[] randNumbers = new int[mummyAmount];
        for (int i = 0; i < randNumbers.Length; i++)
        {
            int thisNumber = Random.Range(0, numbers.Count);
            randNumbers[i] = numbers[thisNumber];
            numbers.RemoveAt(thisNumber);

        }

        for (int i = 0; i < mummyAmount; i++)
        {
            mummies.Add(Instantiate(owner.mummy, owner.mummySpawns[randNumbers[i]].position, owner.mummySpawns[randNumbers[i]].rotation));
            mummies[i].GetComponent<MummBehaviour>().player = owner.player;           
        }

    }

    private bool ListIsEmpty(List<GameObject> myList)
    {

        foreach (GameObject item in myList)
        {
            if (item != null)
            {
                return false;
            }
        }

        return true;

    }

    public override void Exit()
    {
        for (int i = 0; i < mummies.Count; i++)
        {
            Destroy(mummies[i]);
            mummies.RemoveAt(i);
        }
        base.Exit();
    }
}
