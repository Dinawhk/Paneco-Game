using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;
using Improbable.Player;


public class FlagCaptured : MonoBehaviour

{


	[Require] private PlayerInput.Reader PlayerInputReader;
	 private GameObject flag;

	private void OnEnable()
	{
		PlayerInputReader.FlagcapturedTriggered.Add (OnFlagcaptured);
	}

	private void OnDisable()
	{
		PlayerInputReader.FlagcapturedTriggered.Remove (OnFlagcaptured);
	}

	private void OnFlagcaptured(Flagcaptured flag_captured)
	{
		flag = GameObject.FindGameObjectWithTag ("Flag");


		if (flag != null)
		{
			flag.SetActive (false);
			GetComponent<Transform> ().GetChild (1).GetComponent<MeshRenderer> ().enabled = true;
		}


	}



}
