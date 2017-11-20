using UnityEngine;
using Assets.Gamelogic.Core;
using Assets.Gamelogic.Utils;
using Improbable;
using Improbable.Unity.Core.EntityQueries;
using Improbable.Core;
using Improbable.Unity.Core;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Visualizer;


public class TeamSetup: MonoBehaviour {
	
	[Require] PlayerTeam.Reader PlayerTeamReader;
	[SerializeField] GameObject playerBody;
	[SerializeField] Material new_mat;

	void OnEnable() 
	{
			
		if((int)PlayerTeamReader.Data.team.colorteam == (int)ColorTeam.RED)
		{
			playerBody.GetComponent<Renderer> ().material = new_mat;
		}
	}

}
