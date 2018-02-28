using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	void OnCollisionEnter(Collision col)
	{
		GameObject hit = col.gameObject;//引入被碰撞的物体；
		Health health = hit.GetComponent <Health >();//获取目标身上的Health组件；

		if(health != null )//如果health不为空，则执行TakeDamege（）方法；
		{
			health.TakeDamage (10);
		}

		Destroy (this.gameObject );//销毁自身；
	}
		

}
