using UnityEngine;
using Assets.Gamelogic.Core;
using Assets.Gamelogic.Utils;
using Improbable;
using Improbable.Core;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Visualizer;

public class PlayerShootingReceiver : MonoBehaviour {


	[Require] private PlayerInput.Reader PlayerInputReader;

	public int speed=20;
	ParticleSystem gunParticles;
	//AudioSource gunAudio;
	float effectsDisplayTime = 0.2f;


	public GameObject shot;

	void Awake ()
	{
		gunParticles = GetComponent<ParticleSystem> ();

		//gunAudio = GetComponent<AudioSource> ();
	}

	private void OnEnable()
	{
		PlayerInputReader.ShotfiredTriggered.Add (OnShoot);
	}

	private void OnDisable()
	{
		PlayerInputReader.ShotfiredTriggered.Remove (OnShoot);
	}


	private void OnShoot(Shotfired theshot){

		//gunAudio.Play ();

		gunParticles.Stop ();
		gunParticles.Play ();
	
		var myshot=Instantiate(shot, GetComponent<Transform> ().position, GetComponent<Transform> ().rotation) as GameObject;
		myshot.GetComponent <Rigidbody>().velocity = GetComponent<Transform> ().up * speed;
	}
}
