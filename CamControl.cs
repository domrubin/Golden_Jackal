using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour {
    [SerializeField]
    public Transform player;
    [SerializeField]
    private bool isCenter;
    // Use this for initialization
    void Start () {

}

// Update is called once per frame
void Update () {
        transform.LookAt(player);

       
    }
}
