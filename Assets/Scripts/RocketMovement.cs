using UnityEngine;
using System.Collections;

public class RocketMovement : MonoBehaviour {
	private bool boostActive;
	Vector3 dir = new Vector3(1,0,0);
	private float boostStart;
	RocketAttributes attributes;
	private float mag;
	
	//here are some values made up values that should probably be adjusted for actual gameplay. 
	//# of seconds the boost lasts
	private static float boostDuration = 5;
	//degrees per rotation
	private static float rotangle = 20;
	//speed without boost
	private static float speed = 1;
	//boost mutliplier
	private static float boost = 2;
	//fuel use: rotate
	private static float rotateFuel = 1;
	//fuel use: boost
	private static float boostFuel = 2;
	
	// Use this for initialization
	void Start () {
		boostActive = false;
		attributes = GetComponent<RocketAttributes>();
		mag = speed;
	}
	
	// Update is called once per frame
	void Update () {
		//end boost once boostDuration has passed
		if(boostActive && Time.time - boostStart >= boostDuration){
			boostActive = false;
			mag /= boost;
		}
		
		//inputs: 
		//perform rotation, and update direction vector to reflect this
		if(Input.GetKeyDown(KeyCode.UpArrow) && attributes.getFuel() >= rotateFuel){
			rotate(rotangle);
			attributes.useFuel(rotateFuel);
		}
		else if(Input.GetKeyDown(KeyCode.DownArrow) && attributes.getFuel() >= rotateFuel){
			rotate (360-rotangle);
			attributes.useFuel(rotateFuel);

		}
		//start boost
		else if(Input.GetKeyDown(KeyCode.Space) && attributes.getFuel() >= boostFuel){
			boostActive = true;
			boostStart = Time.time;
			mag*=boost;
			attributes.useFuel(boostFuel);
			
		}
		
		//move rocket
		transform.position = mag*Time.deltaTime*dir + transform.position;

	}
	private void rotate(float angle){
		transform.RotateAround(transform.position, Vector3.forward, angle);
		dir = Quaternion.AngleAxis(angle, Vector3.forward)*dir;
		dir.Normalize();
	}
}
