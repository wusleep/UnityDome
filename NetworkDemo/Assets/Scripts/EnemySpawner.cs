using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine .Networking ;

public class EnemySpawner : NetworkBehaviour  {

	public GameObject Enemy;//创建敌人；
	public int NumberOfEnemies;//定义敌人个数；

	public override void OnStartServer ()
	{
		for(int i = 0; i<NumberOfEnemies ;i++)
		{
			Vector3 position = new Vector3 (Random .Range (-6,6),0,Random.Range (-6,6));//定义敌人出现的范围；

			Quaternion rotate = Quaternion.Euler (0,Random.Range(0,360),0);//定义敌人旋转角度；

			GameObject enemy = Instantiate (Enemy ,position ,rotate );//创建敌人；

			NetworkServer.Spawn (enemy );//将敌人同步到网络；
		}
	}

}
