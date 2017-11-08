
using Improbable.Player;
using Improbable.Unity.Visualizer;
using Improbable.Worker;
using UnityEngine;

namespace Assets.Gamelogic.Player
{
    public class PlayerRotationReceiver : MonoBehaviour
    {
        [Require]
        private PlayerRotation.Reader PlayerRotationReader;

        void OnEnable()
        {
            transform.rotation = UnityEngine.Quaternion.Euler(Vector3.up * PlayerRotationReader.Data.bearing);

            PlayerRotationReader.ComponentUpdated.Add(OnComponentUpdated);
        }

        void OnDisable()
        {
            PlayerRotationReader.ComponentUpdated.Remove(OnComponentUpdated);
        }

        void OnComponentUpdated(PlayerRotation.Update update)
        {
            if (PlayerRotationReader.Authority == Authority.NotAuthoritative)
            {
                if (update.bearing.HasValue)
                {
                    transform.rotation = UnityEngine.Quaternion.Euler(Vector3.up * update.bearing.Value);
                }
            }
        }
    }
}