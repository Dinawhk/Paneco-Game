using Improbable;
using Improbable.Unity;
using Improbable.Player;
using Improbable.Unity.Visualizer;
using UnityEngine;
using System;
using Assets.Gamelogic.Core;
using Assets.Gamelogic.Utils;
using System.Collections;

[WorkerType(WorkerPlatform.UnityWorker)]
public class WorkerDyingHandler : MonoBehaviour
	{
	[Require] private Health.Writer HealthWriter;
	[Require] private Position.Writer PositionWriter;
	private bool isDead = false;


	private void OnEnable()
	{
		isDead = false;

		HealthWriter.CurrentHealthUpdated.Add (OnHealthUpdated);
	}

	private void OnDisable()
	{
		HealthWriter.CurrentHealthUpdated.Remove (OnHealthUpdated);

	}

	private void OnHealthUpdated(int currentHealth)
	{
		if (!isDead && currentHealth <= 0) 
		{
			Die ();
		}
	}

	private void Die()
	{
		isDead = true;
		StartCoroutine(DelayedAction(Respawn, 1f));
		
	}

	private IEnumerator DelayedAction(Action action, float delay) {
		yield return new WaitForSeconds(delay);
		action();
	}

	private void Respawn()
	{
		isDead = false;

		var position = new Vector3 (10f, 0f, 10f);
		transform.position = position;
		PositionWriter.Send (new Position.Update().SetCoords(new Coordinates(position.x, position.y, position.z)));

		HealthWriter.Send(new Health.Update()
			.SetCurrentHealth(100));
	}

	}


