using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
        
    public float power;

    private IDamageable target;

    private void OnTriggerEnter(Collider other)
    {

        if(gameObject.tag == other.gameObject.tag)
        {
            return;
        }

        target = other.GetComponent<IDamageable>();

        if(target == null && other.tag == "WeakSpot")
        {
            target = other.GetComponentInParent<IDamageable>();
        }

        if(target !=null)
            target.TakeDamage(power, transform.position);       

    }

    private void OnParticleCollision(GameObject other)
    {
        target = other.GetComponent<IDamageable>();

        if (target == null && other.tag == "WeakSpot")
        {
            target = other.GetComponentInParent<IDamageable>();
        }

        if (target != null)
            target.TakeDamage(power, transform.position);
    }


}
