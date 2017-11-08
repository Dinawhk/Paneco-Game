using System;
using Improbable;
using Improbable.Core;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using Improbable.Worker;
using UnityEngine;
using Improbable.Player;

[WorkerType(WorkerPlatform.UnityClient)]
public class PlayerBehaviour : MonoBehaviour
	{
	
	[Require] private PlayerInput.Reader PlayerInputReader;
	Animator anim;


	private void OnEnable()
	{
		PlayerInputReader.MovementTriggered.Add (OnMovement);
		PlayerInputReader.IdleTriggered.Add (OnIdle);
	}

	private void OnDisable()
	{
		PlayerInputReader.MovementTriggered.Remove (OnMovement);
		PlayerInputReader.IdleTriggered.Remove (OnIdle);

	}

	void Awake () 
	{
		anim = GetComponent<Animator> ();
		anim.SetBool ("IsRunning", false);

	}

	private void OnMovement(Movement movement)
	{
		anim.SetBool ("IsRunning", true);
	}

	private void OnIdle(Idle idle)
	{
		anim.SetBool ("IsRunning", false);
	}

	/*void FixedUpdate () 
	{
		// Store the input axes.
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Turning (h, v);
	}*/


	/* void Turning(float x, float y)
	{
		// Create a boolean that is true if either of the input axes is non-zero.
		bool walking = x != 0f || y != 0f;

		// Tell the animator whether or not the player is walking.
		anim.SetBool ("IsRunning", walking);
	}*/
			 

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Flag"))
        {
            other.gameObject.SetActive(false);
        }
    }

}
