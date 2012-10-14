using UnityEngine;
using System.Collections;

public class EnemyMissileSpawner : MonoBehaviour {
	
	public float interval;	//Amount of time, in seconds, between shots
	float nextShot;		//Internal timer
	public GameObject Enemy;//Missile object to spawn 
	public float speed;	//Speed at which missiles are fired
	public float missileTimeOut;	//Time taken for missiles to disappear
	
	void Start () {
		//Start shooting immediately
		BeginShooting(interval);
	}
	
	//fixed interval firing
	public void BeginShooting(float interval){
		nextShot = Time.fixedTime + interval;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.fixedTime >= nextShot){
			//create missile at current position, headed along the spawner's 
			GameObject o = Instantiate(Enemy, transform.position, Quaternion.identity) as GameObject;
			EnemyMissileBehavior e = o.GetComponent<EnemyMissileBehavior>();
			//Fire a shot along the y-axis of the spawner, at speed speed
			e.InitEnemy(transform.up,speed,missileTimeOut);
			//update time of next shot
			nextShot = Time.fixedTime + interval;
		}
	}
}
