using Improbable.Unity;
using Improbable.Player;
using Improbable.Unity.Visualizer;
using UnityEngine;
using Assets.Gamelogic.Core;

public class PlayerDeath : MonoBehaviour
{
	[Require] private Health.Reader HealthReader;
	[Require] private HaveFlag.Writer HaveFlagWriter;

	private void OnEnable()
	{
		/*if (HealthReader.Data.currentHealth <= 0) 
		{
			gameObject.GetComponent<Transform> ().GetChild (1).GetComponent<MeshRenderer> ().enabled = false;

		}*/
		HealthReader.CurrentHealthUpdated.Add (OnCurrentHealthUpdated);
	}

	private void OnDisable()
	{
		HealthReader.CurrentHealthUpdated.Remove (OnCurrentHealthUpdated);
	}

	private void OnCurrentHealthUpdated(int currentHealth)
	{
		if (currentHealth <= 0)
		{
			//gameObject.GetComponent<Transform> ().GetChild (1).GetComponent<MeshRenderer> ().enabled = false;

			if (HaveFlagWriter != null && gameObject.GetComponent<Transform>().GetChild(1).GetComponent<MeshRenderer>().enabled)
			{
				//Instantiate (flag, gameObject.GetComponent<Transform> ().position, gameObject.GetComponent<Transform> ().rotation);
				HaveFlagWriter.Send (new HaveFlag.Update ().SetHaveFlag (false));

			}
			//HealthWriter.Send (new Health.Update().AddDeath(new Death()));
			//HaveFlagWriter.Send (new HaveFlag.Update ().SetHaveFlag (false));
			//gameObject.transform.position = new Vector3 (10f, 0f, 10f);
			//flag.transform.position = new Vector3 (0f, 0f, 0f);
			//flagRespawns ();
			//gameObject.SetActive (false);

		}
	}
}
