using UnityEngine;
using Assets.Gamelogic.Core;
using Assets.Gamelogic.Utils;
using Improbable;
using Improbable.Core;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Visualizer;



[WorkerType(WorkerPlatform.UnityWorker)]
public class PlayerMover : MonoBehaviour {

	[Require] private Position.Writer PositionWriter;
	[Require] private Rotation.Writer RotationWriter;
	[Require] private PlayerInput.Reader PlayerInputReader;


	Rigidbody rigidbody;
	float camRayLenght = 100f;

	void OnEnable ()
	{
		rigidbody = GetComponent<Rigidbody>();

	}

	void FixedUpdate ()
	{
		var joystick = PlayerInputReader.Data.joystick;

		var direction = new Vector3(joystick.xAxis, 0, joystick.yAxis);

		transform.Translate (direction * Time.deltaTime * SimulationSettings.PlayerAcceleration);

		var pos = rigidbody.position;
		var positionUpdate = new Position.Update()
			.SetCoords(new Coordinates(pos.x, pos.y, pos.z));
		PositionWriter.Send(positionUpdate);

	/*	var rotation = new Vector3 ();
		rotation = camera.ScreenToWorldPoint (Input.mousePosition);
		transform.rotation = UnityEngine.Quaternion.LookRotation (Vector3.forward, rotation - transform.position);


		var rotationUpdate = new Rotation.Update()
			.SetRotation(rigidbody.rotation.ToNativeQuaternion());
		RotationWriter.Send(rotationUpdate);*/

	}


	void Turning ()
	{
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;

		// Perform the raycast and if it hits something on the floor layer...
		if (Physics.Raycast (camRay, out floorHit,camRayLenght )) {
			// Create a vector from the player to the point on the floor the raycast from the mouse hit.
			Vector3 playerToMouse = floorHit.point - transform.position;

			// Ensure the vector is entirely along the floor plane.
			playerToMouse.y = 0f;

			// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
			UnityEngine.Quaternion newRotation = UnityEngine.Quaternion.LookRotation (playerToMouse);

			// Set the player's rotation to this new rotation.
			rigidbody.MoveRotation (newRotation);
		}
	}


}
