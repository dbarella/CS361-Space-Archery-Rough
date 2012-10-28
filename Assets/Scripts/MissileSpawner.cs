using UnityEngine;
using System.Collections;

public class MissileSpawner : MonoBehaviour {
	
	//Randomization interval - set equal for no randomization.
	public float minInterval;	//Minimum time, in seconds, between shots
	public float maxInterval;	//Max time, in seconds, between shots
	
	float nextShot;		//Internal timer
	public GameObject missile;//Missile object to spawn 
	public GameObject guidedMissile;	//Guided Missile to spawn
	public float speed;	//Speed at which missiles are fired
	public float missileTimeOut;	//Time taken for missiles to disappear
	public bool isGuided;	//Determines the type of missile fired. 
	
	void Start () {
		//Start shooting after an initial delay
		nextShot = Random.Range(minInterval, maxInterval);
	}
	
	/* This code is used when isGuided is false
	 * Fires unguided missiles in one direction, at a steady rate.
	 * */
	void Update () {
		if(!isGuided && nextShot <= 0){
			//create missile at current position, headed along the spawner's +x axis
			SpawnMissile (null);
			//update time of next shot
			nextShot = Random.Range(minInterval, maxInterval);
		}
		//And decriment the nextShot counter if it's over 0
		if(nextShot > 0)
			nextShot -= Time.deltaTime;
	}
	
	/* This code is used when isGuided is true
	 * Fires guided missiles at the arrow if the arrow is within the trigger area.
	 * Missiles can seek outside of the trigger area.
	 * */
	void OnTriggerStay (Collider other) {
		Debug.Log("other: " + other.tag);
		if(isGuided && other.tag == "Arrow" && nextShot <= 0){
			//create missile at current position, headed along the spawner's +x axis
			SpawnMissile(other.transform.parent.gameObject);
			//Fire a shot along the x-axis of the spawner, at speed speed
			//update time of next shot
			nextShot = Random.Range(minInterval, maxInterval);
		}
	}
	
	//Spawns the missile gameObject, and returns the spawned missile for further initialization
	//Requires a target gameObject if guided. Pass in null if unguided.
	void SpawnMissile(GameObject target){
		GameObject o;
		if(isGuided && target != null){
			o = Instantiate(guidedMissile, transform.position, Quaternion.identity) as GameObject;
			SeekerMissile e = o.GetComponent<SeekerMissile>();
			//Initilize the missile script
			e.InitEnemy(transform.right,speed,missileTimeOut, target);
		}
		else{
			o = Instantiate(missile, transform.position, Quaternion.identity) as GameObject;
			EnemyMissile m = o.GetComponent<EnemyMissile>();
			//Fire a shot along the x-axis of the spawner, at speed speed
			m.InitEnemy(transform.right,speed,missileTimeOut);
		}
	}
}
