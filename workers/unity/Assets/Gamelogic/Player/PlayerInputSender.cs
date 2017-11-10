using UnityEngine;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using Improbable.Player;


namespace Assets.Gamelogic.Player {

[WorkerType(WorkerPlatform.UnityClient)]
public class PlayerInputSender : MonoBehaviour
{

    [Require] private PlayerInput.Writer PlayerInputWriter;

		private bool flagCollided;
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

			if (flagCollided) 
			{
				PlayerInputWriter.Send (new PlayerInput.Update ().AddFlagcaptured (new Flagcaptured ()));
			}

        var update = new PlayerInput.Update();
        update.SetJoystick(new Joystick(xAxis, yAxis));
        PlayerInputWriter.Send(update);

    }

		private void OnTriggerEnter(Collider other)
		{
			if (other != null && other.gameObject.CompareTag("Flag"))
			{
				flagCollided = true;
			}
		}

		private void OnTriggerExit(Collider other) 
		{
			if (other != null && other.gameObject.CompareTag ("Flag")) 
			{
				flagCollided = false;
			}
					
		}
	}

}
