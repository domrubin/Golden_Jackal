using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPos : MonoBehaviour {
    [SerializeField]
    private Transform playerT;
    [SerializeField]
    private float camLookHeight;

    private Transform thisT;
	// Use this for initialization
	void Start () {
        thisT = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        thisT.position = new Vector3(playerT.position.x, camLookHeight, playerT.position.z);
	}
}
