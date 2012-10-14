using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
	
	//Object References
	//Reference to the attached model
	Transform arrowModel;
	//Reference to the game management
	GameManagement mgmt;
	
	//General Attributes
	public float fuel = 50;
	public float health = 100;
	//Spin speed
	public float spin = 2.0f;
	//true if the boost is on, false otherwise
	private bool boostActive;
	
	//Speed Limits
	//Standard top speed
	public static float stdTopSpeed = 20.0f;
	//Boost top speed multiplier
	public float boostSpeedMultiplier = 1.2f;
	
	//Thrust Force
	//Standard force (without boost)
	public float standardForce = 500.0f;
	//Boost force mutliplier
	public float boostMultiplier = 1.75f;
	//Rotation torque
	public float rotationTorque = 100.0f;
	
	//Fuel Expediture
	//Fuel used by rotating
	private float rotateFuel = 4;
	//Fuel used by boosting
	private float boostFuel = 8;
	//Fuel used by normal travel
	private float stdFuel = 2;
	
	void Start () {
		//Source the Game Management
		mgmt = Camera.main.GetComponent<GameManagement>();
		
		//Source the Arrow Model
		arrowModel = transform.FindChild("ArrowModel");
		
		//Set boost field initially to false
		boostActive = false;
		
		//Restrict movement and rotation
		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezePositionZ;
	}
	
	void FixedUpdate() {
		Movearrow();
		
		//Dan's Additions
		RotateModel();
	}
	
	public void Movearrow() {
		transform.Translate(new Vector3(1,0,0) * Time.fixedDeltaTime);
		
		//Get steering input
		if(Input.GetKey(KeyCode.UpArrow) && fuel > 0) {
			transform.Rotate(rotationTorque * Vector3.forward * Time.fixedDeltaTime);
			UseFuel(rotateFuel*Time.fixedDeltaTime);
		}
		else if(Input.GetKey(KeyCode.DownArrow) && fuel > 0) {
			transform.Rotate(rotationTorque * -Vector3.forward * Time.fixedDeltaTime);
			UseFuel(rotateFuel*Time.fixedDeltaTime);
		}
		
		//Toggle boost
		if(Input.GetKey(KeyCode.Space) && fuel > 0){
			boostActive = true;
		} else {
			boostActive = false;
		}
		
		//Apply thrust
		if(fuel > 0) { //If we have fuel
			float forwardSpeed = Vector3.Dot(rigidbody.velocity, transform.forward); //Find our forward speed
			
			if(!boostActive && forwardSpeed <= stdTopSpeed) { //If not boosting and not at top speed, add force
				rigidbody.AddRelativeForce(standardForce * Vector3.right * Time.fixedDeltaTime);	
				//Spend fuel
				UseFuel(stdFuel*Time.fixedDeltaTime);
			} else if(boostActive && forwardSpeed <= boostSpeedMultiplier * stdTopSpeed) { //If boosting, add boost force
				rigidbody.AddRelativeForce(boostMultiplier * standardForce * Vector3.right * Time.fixedDeltaTime);
				//Spend fuel
				UseFuel(boostFuel * Time.fixedDeltaTime);
			}
		}	
	}
	
	/**
	 * Rotates the arrow model about its axis for visual flair
	 **/
	private void RotateModel() {
		arrowModel.RotateAroundLocal(Vector3.right, spin*Time.fixedDeltaTime);
	}
	
	/**
	 * Kills the arrow and calls management to reset the level
	 **/
	public void Die() {
		Destroy(gameObject);
		mgmt.ResetLevel();
	}
	
	//Getters and Setters
	public float GetFuel(){
		return fuel;
	}
	public float GetHealth(){
		return health;
	}
	public void LoseHealth(float damage){
		health -= damage;
	}
	public void UseFuel(float spent){
		fuel -= spent;
	}
}
