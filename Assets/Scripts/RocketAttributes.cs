using UnityEngine;
using System.Collections;

public class RocketAttributes : MonoBehaviour {
	public float fuel;
	public float health;
	public float spin = 2.0f;
	
	// Use this for initialization
	void Start () {
		//Pass
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
}
