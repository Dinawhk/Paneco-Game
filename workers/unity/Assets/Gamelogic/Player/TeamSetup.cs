using UnityEngine;
using Assets.Gamelogic.Core;
using Assets.Gamelogic.Utils;
using Improbable;
using Improbable.Core;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Visualizer;


public class TeamSetup: MonoBehaviour {
	[Require] PlayerTeam.Reader PlayerTeamReader;
	[SerializeField] GameObject playerBody;

	void OnEnable() 
	{
		if((int)PlayerTeamReader.Data.team.colorteam == (int)ColorTeam.RED)
		{
			playerBody.GetComponent<Renderer> ().material.color = Color.red;
		}
	}

}
