using UnityEngine;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using Improbable.Player;
using Improbable;
using Assets.Gamelogic.Core;


namespace Assets.Gamelogic.Player {

[WorkerType(WorkerPlatform.UnityClient)]
public class PlayerInputSender : MonoBehaviour
{

    [Require] private PlayerInput.Writer PlayerInputWriter;
	 


	//[Require] private HaveFlag.Writer HaveFlagWriter;

		private bool flagCollided = false;
		private bool walking;

    void Update ()
    {
        var xAxis = Input.GetAxis("Horizontal");
        var yAxis = Input.GetAxis("Vertical");

		walking = xAxis != 0f || yAxis != 0f;

			if (walking) {
				PlayerInputWriter.Send (new PlayerInput.Update ().AddMovement (new Movement ()));	
			} else {			
				PlayerInputWriter.Send (new PlayerInput.Update ().AddIdle (new Idle ()));
			}
				

        var update = new PlayerInput.Update();
        update.SetJoystick(new Joystick(xAxis, yAxis));
        PlayerInputWriter.Send(update);

    }

		private void OnCollisionEnter(Collision other)
		{
			if (other != null && other.gameObject.CompareTag("Flag"))
			{
				if ( PlayerInputWriter == null) {
					return;
				} else {
					//HaveFlagWriter.Send (new HaveFlag.Update ().SetHaveFlag (true));
					FlagPositionSender.isCaptured = true;
					PlayerInputWriter.Send (new PlayerInput.Update ().AddFlagcaptured (new Flagcaptured ()));
				}
			}
		}
			
	}

}
