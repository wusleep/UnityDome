using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour  {

	public float horizontal,vertical;

	public GameObject bulletPrefabs;
	public Transform bulletSpawn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isLocalPlayer == false ){return;}
		horizontal = Input.GetAxis ("Horizontal");
		vertical = Input.GetAxis ("Vertical");

		transform.Rotate (Vector3.up*horizontal*120*Time.deltaTime );
		transform.Translate (Vector3.forward*vertical*3*Time.deltaTime );

		if(Input.GetKeyDown (KeyCode .Space ))
		{
			CmdFire ();
		}

	}

	public override void OnStartLocalPlayer ()
	{
		GetComponent <MeshRenderer > ().material.color = Color.blue;
	}

	[Command ]//call in client ,ren in server;
	public void CmdFire()
	{
		GameObject bullet = Instantiate (bulletPrefabs, bulletSpawn.position, bulletSpawn.rotation)as GameObject ;
		bullet.GetComponent <Rigidbody > ().velocity = bullet.transform.forward * 10;//速度是正前方10米每秒；

		Destroy (bullet,2);

		NetworkServer.Spawn (bullet );//spawn perfabs in all client wiche are ready;
	}

}
