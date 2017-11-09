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
		
			 

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Flag"))
        {
            other.gameObject.SetActive(false);
        }
    }

}
