using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    public PlayerControl player;

	// Use this for initialization
	void Start () {
        player = GetComponentInParent<PlayerControl>();
	}

    private void OnTriggerEnter(Collider other)
    {
        player.canJump = true;
        player.soundController.ChangeSFX(player.soundController.clips[1]);
        player.animControl.SetBool("Jump", false);
    }
}
