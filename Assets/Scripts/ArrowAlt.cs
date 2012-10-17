using UnityEngine;
using System.Collections;

/*
 * An alternative ArrowMovement for consideration.
 * Heavily based of the script Arrow.cs by the esteemable Mr. D. Barella, Esq.
 * Arrow behaves more like a ship in Asteroids, less like a boat.
 * 
 * The main difference between the two scripts is actually in teh object - setting Object drag to 0 allows for Asteroids movement.
 * 
 */

public class ArrowAlt : MonoBehaviour {
	Transform arrowModel;
	
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
	//Braking force (if over speed limit)
	public float brakeForce = 200.0f;
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
		Move();
		
		
		RotateModel();
	}
	
	public void Move() {
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
		if(fuel > 0) { //If we have fuel
			float forwardSpeed = Vector3.Dot(rigidbody.velocity, transform.forward); //Find our forward speed
			
			//if we aren't boosting, we continue along our current heading, and can turn around to set up our next boost
			
			if(boostActive && forwardSpeed <= boostSpeedMultiplier * stdTopSpeed) { //If boosting, add boost force
				rigidbody.AddRelativeForce(boostMultiplier * standardForce * Vector3.right * Time.fixedDeltaTime);
				//Spend fuel
				UseFuel(boostFuel * Time.fixedDeltaTime);
			}
			if(!boostActive && forwardSpeed > stdTopSpeed) {
				rigidbody.AddRelativeForce (brakeForce * Vector3.left * Time.fixedDeltaTime);	
			}
		}	
		
	}

			
	

	
	private void Rotate(float angle){
		dir = Quaternion.AngleAxis(angle, Vector3.forward)*dir;
		dir.Normalize();
	}
	
	public void SetDir(Vector3 direction){
		dir = direction;
	}
	
	
	private void RotateModel() {
		arrowModel.RotateAroundLocal(Vector3.right, spin*Time.fixedDeltaTime);
	}
}