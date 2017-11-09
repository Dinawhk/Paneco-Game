using Assets.Gamelogic.Core;
using Improbable.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Improbable.Player;

namespace Assets.Gamelogic.Player
{
    public class FirstPersonCamera : MonoBehaviour
    {

        [Require]
        private PlayerRotation.Writer PlayerRotationWriter;

        [Require]
        private ClientAuthorityCheck.Writer ClientAuthorityCheckWriter;

        private Transform camera;
        private float pitch;

        private void OnEnable()
        {
            // Grab the camera from the Unity scene
            camera = Camera.main.transform;
            // Set the camera as a child of the Player to easily ensure the camera follows the Player
            camera.parent = transform;
            // Position the camera at the same location as the Player
            camera.localPosition = SimulationSettings.FirstPersonCameraOffset;

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            SetCameraRotation();
        }

        // Update the pitch of camera object and the yaw of the Player object
        private void SetCameraRotation()
        {
            // Take the new yaw (horizontal look) based on the position on the lateral position of the mouse in the viewport
            var yaw = (transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * SimulationSettings.FirstPersonCameraSensitivity) % 360f;
            // Take the new pitch (vertical look) based on the position on the vertical position of the mouse in the viewport
            pitch = Mathf.Clamp(pitch - Input.GetAxis("Mouse Y") * SimulationSettings.FirstPersonCameraSensitivity, -SimulationSettings.FirstPersonCameraMaxPitch, -SimulationSettings.FirstPersonCameraMinPitch);
            // Update the pitch value of the camera
            camera.localRotation = Quaternion.Euler(new Vector3(pitch, 0, 0));

            if (!Input.GetKey(KeyCode.LeftAlt))
            {
                // Update the yaw value of the Player transform
                transform.rotation = Quaternion.Euler(new Vector3(0, yaw, 0));
                PlayerRotationWriter.Send(new PlayerRotation.Update().SetBearing(transform.eulerAngles.y));

            }

        }
    }
}