using Assets.Gamelogic.EntityTemplates;
using Improbable;
using Improbable.Unity.Core.EntityQueries;
using Improbable.Entity.Component;
using Improbable.Core;
using Improbable.Unity;
using Improbable.Player;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;
using Improbable.Worker;

namespace Assets.Gamelogic.Core
{
    [WorkerType(WorkerPlatform.UnityWorker)]
    public class PlayerCreatingBehaviour : MonoBehaviour
    {
        [Require]
        private PlayerCreation.Writer PlayerCreationWriter;

        private void OnEnable()
        {
            PlayerCreationWriter.CommandReceiver.OnCreatePlayer.RegisterAsyncResponse(OnCreatePlayer);
        }

        private void OnDisable()
        {
            PlayerCreationWriter.CommandReceiver.OnCreatePlayer.DeregisterResponse();
        }

        private void OnCreatePlayer(ResponseHandle<PlayerCreation.Commands.CreatePlayer, CreatePlayerRequest, CreatePlayerResponse> responseHandle)
        {
			
            var clientWorkerId = responseHandle.CallerInfo.CallerWorkerId;

			var query = Query.HasComponent<PlayerTeam> ().ReturnCount ();
			var teamColor = ColorTeam.RED;

			SpatialOS.Commands.SendQuery (PlayerCreationWriter, query)
				.OnSuccess (result => {
					Debug.LogWarning ("Found " + result.EntityCount + " nearby entities with a team component");
					if((result.EntityCount % 2) == 0)
					{
						teamColor = ColorTeam.BLUE;

					}
				var playerEntityTemplate = EntityTemplateFactory.CreatePlayerTemplate(clientWorkerId, teamColor);
					SpatialOS.Commands.CreateEntity (PlayerCreationWriter, playerEntityTemplate)
						.OnSuccess (_ => responseHandle.Respond (new CreatePlayerResponse ((int) StatusCode.Success)))
						.OnFailure (failure => responseHandle.Respond (new CreatePlayerResponse ((int) failure.StatusCode)));
				})
				.OnFailure(errorDetails => Debug.LogWarning("Query failed with error: " + errorDetails));

        }
    }
}
