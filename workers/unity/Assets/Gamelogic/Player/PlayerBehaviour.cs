using System;
using Improbable;
using Improbable.Core;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using Improbable.Worker;
using UnityEngine;
using Improbable.Player;

public class PlayerBehaviour : MonoBehaviour
	{
	Animator anim;

	void Awake () 
	{
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate () 
	{
		// Store the input axes.
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		// Turn the player to face the mouse cursor.
		Turning (h,v);
	}

	void Turning(float x, float y)
	{
		// Create a boolean that is true if either of the input axes is non-zero.
		bool walking = x != 0f || y != 0f;

		// Tell the animator whether or not the player is walking.
		anim.SetBool ("IsRunning", walking);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Flag"))
        {
            other.gameObject.SetActive(false);
        }
    }

}
