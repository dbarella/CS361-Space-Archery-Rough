using UnityEngine;
using System.Collections;

/* DEPRICATED CLASS
 * All functionality is now in MissileSpawner.cs
 * */
public class SeekerMissileSpawner : MonoBehaviour {
	
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
	
	//Fire at any Rocket objects within the "vision radius"
	void OnTriggerStay (Collider other) {
		Debug.Log("other: " + other.tag);
		if(other.tag == "Arrow" && Time.fixedTime >= nextShot){
			//create missile at current position, headed along the spawner's axis
			GameObject o = Instantiate(Enemy, transform.position, Quaternion.identity) as GameObject;
			SeekerMissile e = o.GetComponent<SeekerMissile>();
			//Fire a shot along the y-axis of the spawner, at speed speed
			e.InitEnemy(transform.up,speed,missileTimeOut, other.transform.parent.gameObject);
			//update time of next shot
			nextShot = Time.fixedTime + interval;
		}
	}
}
