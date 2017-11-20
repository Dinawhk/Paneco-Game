using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShot : MonoBehaviour {


	[SerializeField]
	private float SecondsUntilDestroy = 4f;
	private float spawnTime;

	void Start()
	{
		spawnTime = Time.time;
	}

	void Update()
	{
		var lifeTime = Time.time - spawnTime;
		if (lifeTime > SecondsUntilDestroy)
		{
			Destroy(gameObject);
		}
	}
	private void OnTriggerEnter(Collider other){
		if (other != null) {
			Destroy (gameObject);	
		}
	
	}
}
