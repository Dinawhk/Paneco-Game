using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;
using Improbable.Player;
using Improbable;
using Assets.Gamelogic.Core;

[WorkerType(WorkerPlatform.UnityClient)]
public class FlagTaken : MonoBehaviour
	{

		[Require] private HaveFlag.Reader HaveFlagReader;


		private void OnEnable()
		{
			

		if (HaveFlagReader.Data.haveFlag) {
			gameObject.GetComponent<Transform> ().GetChild (1).GetComponent<MeshRenderer> ().enabled = true;

		} else 
		{
			gameObject.GetComponent<Transform> ().GetChild (1).GetComponent<MeshRenderer> ().enabled = false;
		}
			HaveFlagReader.HaveFlagUpdated.Add (OnFlagTaken);
		}

		private void OnDisable()
		{
			HaveFlagReader.HaveFlagUpdated.Remove (OnFlagTaken);
		}

	private void OnFlagTaken(bool have_flag)
		{
		
		//flag = GameObject.FindGameObjectWithTag ("Flag");

		if (have_flag) {
			gameObject.GetComponent<Transform> ().GetChild (1).GetComponent<Renderer> ().enabled = true;
			/*var rend = flag.GetComponentsInChildren<MeshRenderer> ();
			 
			foreach (Renderer r in rend)
			{
				r.enabled = false;
			}*/
		} else
		{
			gameObject.GetComponent<Transform> ().GetChild (1).GetComponent<MeshRenderer> ().enabled = false;
		}
		}

	}


