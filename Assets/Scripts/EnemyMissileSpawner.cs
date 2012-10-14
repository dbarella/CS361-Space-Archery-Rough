using UnityEngine;
using System.Collections;

public class EnemyMissileSpawner : MonoBehaviour {
	bool random;
	float interval;
	float nextShot;
	//need to set this to the enemy prefab
	public GameObject Enemy;
	//range, in seconds, in which next random missile spawn can happen
	public static float rLowerBound = 2.0f, rUpperBound=12.0f;
	//direction rockets will fire in. feel free to play with this individually as necessary, or let me know if there's something more complex you want this to do. 
	public Vector3 direction;
	public Quaternion rotation;
	void Start () {
		//this will just shoot a rocket straight left of the screen
		transform.rotation = new Quaternion(0,0,180,0);
		direction = Vector3.right;
		//init to shoot at a random interval
		BeginShooting();
	}
	
	//one the following two methods needed to begin shooting
	//random interval firing
	public void BeginShooting(){
		random = true;
		nextShot = Time.fixedTime + Random.Range(rLowerBound, rUpperBound);
	}
	//fixed interval firing
	public void BeginShooting(float interval){
		random = false;
		this.interval = interval;
		nextShot = Time.fixedTime + interval;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.fixedTime >= nextShot){
			//create rocket at current position, headed in dir
			GameObject o = Instantiate(Enemy, transform.position, transform.rotation) as GameObject;
			EnemyMissileBehavior e = o.GetComponent<EnemyMissileBehavior>();
			e.InitEnemy(direction);
			//update time of next shot
			if(random){
				nextShot = Time.fixedTime + Random.Range(rLowerBound, rUpperBound);
			}
			else{
				nextShot= Time.fixedTime + interval;
			}
		}
	}
}
