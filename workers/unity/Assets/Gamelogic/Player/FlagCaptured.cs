using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;
using Improbable.Player;
using Improbable;


public class FlagCaptured : MonoBehaviour

{

	[Require] private PlayerInput.Reader PlayerInputReader;
	[Require] private HaveFlag.Writer HaveFlagWriter;

	private GameObject flag;
	private bool flagCaptured = false;

	private void OnEnable()
	{
		/*if(HaveFlagWriter.Data.haveFlag)
		{
			GetComponent<Transform> ().GetChild (1).GetComponent<MeshRenderer> ().enabled = true;
		}*/
		PlayerInputReader.FlagcapturedTriggered.Add (OnFlagcaptured);
	}

	private void OnDisable()
	{
		
		PlayerInputReader.FlagcapturedTriggered.Remove (OnFlagcaptured);
	}

	private void OnFlagcaptured(Flagcaptured flag_captured)
	{
		/*flag = GameObject.FindGameObjectWithTag ("Flag");
		flagCaptured = true;

		if (flag != null)
		{
			flag.SetActive (false);
			GetComponent<Transform> ().GetChild (1).GetComponent<MeshRenderer> ().enabled = true;
		}*/
		if (HaveFlagWriter != null)
		{

			HaveFlagWriter.Send (new HaveFlag.Update ().SetHaveFlag (true));
		}

	}



}
