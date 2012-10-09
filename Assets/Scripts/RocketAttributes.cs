using UnityEngine;
using System.Collections;

public class RocketAttributes : MonoBehaviour {
	
	//Dan's additions
	//Ref to the game management
	GameManagement mgmt;
	
	public float fuel;
	public float health;
	public float spin = 2.0f;
	
	// Use this for initialization
	void Start () {
		mgmt = Camera.main.GetComponent<GameManagement>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//i felt like there wasn't enough in this script so i wrote these methods
	public float getFuel(){
		return fuel;
	}
	public float getHealth(){
		return health;
	}
	public void loseHealth(float damage){
		health -= damage;
	}
	public void useFuel(float spent){
		fuel -= spent;
	}
	
	/**
	 * Kills the rocket and calls management to reset the level
	 **/
	public void Die() {
		Destroy(gameObject);
		mgmt.ResetLevel();
	}
}
