using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerMove : MonoBehaviour {

    [SerializeField]
    float speed;
    [SerializeField]
    Transform boss;

    private float radius;

    Vector3 dir;
    bool immortal;
    float immortalTimer = .5f;
    float countDown;

    private void Start()
    {
       radius = Vector3.Distance(transform.position, boss.position);     

    }


    // Update is called once per frame
    void Update () {

        if(Input.GetKey(KeyCode.A))
            transform.RotateAround(boss.position, Vector3.up, speed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.D))
            transform.RotateAround(boss.position, Vector3.up, -speed * Time.deltaTime);

        if(immortal)
        {
            countDown -= Time.deltaTime;
            if(countDown <= 0)
            {
                immortal = false;
            }
        }
    }


    void TakeDamage(GameObject source, float amount)
    {
        if (!immortal)
        {
            Debug.Log("OUCH " + amount + " " + source);
            immortal = true;
            countDown = immortalTimer;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Harmful") { }
            //TakeDamage(other.gameObject, other.GetComponentInParent<Attack>().damage);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Harmful") { }
            //TakeDamage(other, other.GetComponentInParent<Attack>().damage);
    } 



}
