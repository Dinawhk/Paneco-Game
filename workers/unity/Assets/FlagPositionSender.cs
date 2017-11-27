using Assets.Gamelogic.Utils;
using Assets.Gamelogic.Core;
using Improbable;
using Improbable.Core;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

public class FlagPositionSender : MonoBehaviour {

	[Require] Position.Writer PositionWriter;

	public static bool isCaptured = false;


	private void Update()
	{
		
		if (isCaptured) {
			PositionWriter.Send (new Position.Update ().SetCoords (new Coordinates (0f, -9999f, 0f)));
		} else
		{
			
		}
	}



	/*private void OnCollisionEnter(Collision coll)
	{
		if (PositionWriter == null)
			return;
		

		if (coll.gameObject.tag == "Player")
		{
			PositionWriter.Send (new Position.Update ().SetCoords (new Coordinates (0f, -9999f, 0f)));
		}
	}*/

	 
}
