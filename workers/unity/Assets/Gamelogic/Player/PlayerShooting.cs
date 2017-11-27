using UnityEngine;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using Improbable.Player;


[WorkerType(WorkerPlatform.UnityClient)]
public class PlayerShooting : MonoBehaviour
{

	[Require] private PlayerInput.Writer PlayerInputWriter;

    float timer;
	private float nextFire;
	public float fireRate;

    void Update ()
    {
        
		if(Input.GetButton ("Fire1") && Time.time > nextFire)
        {
			nextFire = Time.time + fireRate;
			PlayerInputWriter.Send(new PlayerInput.Update().AddShotfired(new Shotfired()));

        }
			
    }
		
}
