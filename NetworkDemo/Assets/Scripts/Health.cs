using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour  {

	public const int maxHealth = 100;
	[SyncVar(hook = "OnChangeHealth")]//将服务器端的当前血量同步到每个已经准备好的客户端；
	public int currentHealth = maxHealth;
	public Slider healthSlider;
	public bool destroyOnDeath = false;

	private NetworkStartPosition[] spawnPositions;

	void Start()
	{
		if(isLocalPlayer )
		{
			spawnPositions = FindObjectsOfType<NetworkStartPosition > ();//获取重生点；
		}
	}

	//受到伤害后血量减10,，当血量为0或者0一下，将当前血量改为0；
	public void TakeDamage(int damage)
	{
		if(isServer == false ) return;//血量的变化只在服务器端进行；
		currentHealth -= damage;
		if(currentHealth <= 0)
		{
			if(destroyOnDeath )//判断是否消灭物体；
			{
				Destroy (this.gameObject );
				return;
			}
			currentHealth = maxHealth ;//当血量为0时，恢复最大血量；
			RpcReSpawn ();//当player血量为0时，重置player位置；
		}
		healthSlider.value = currentHealth / (float )maxHealth;
	}

	//每次当前血量变化，就会调用这个方法；
	void OnChangeHealth(int currentHealth)
	{
		healthSlider.value = currentHealth / (float )maxHealth;
		
	}

	[ClientRpc ]//所有客户端都执行；
	void RpcReSpawn()
	{
		//如果不是本地player，则直接返回，否则执行重置位置；
		if (isLocalPlayer == false)
			return;

		Vector3 spawnPosition = Vector3.zero;

		if(spawnPositions !=null && spawnPositions.Length >0)
		{
			spawnPosition = spawnPositions [Random.Range (0, spawnPositions.Length)].transform.position;//获取随机出生点；
		}
		transform.position = spawnPosition;
	}

}
