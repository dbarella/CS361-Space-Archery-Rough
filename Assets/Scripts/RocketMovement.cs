using UnityEngine;
using System.Collections;

public class RocketMovement : MonoBehaviour {
	
	//Dan's additions
	private float spin;
	Transform rocketModel;
	
	//Reference to RocketAttributes.cs
	RocketAttributes attributes;
	
	//The rocket's direction vector. Defaults to a (1,0,0) vector.
	Vector3 dir = new Vector3(1,0,0);
	
	//true if the boost is on, false otherwise
	private bool boostActive;
	
	//# of seconds the boost lasts
//	private float boostDuration = 1;
	//Timer for the rocket's boost
	//private float boostTimer = 0f;
	
	//Standard top speed
	public static float stdTopSpeed = 20.0f;
	//Square of the top speed for cheaper iterations
	private static float sqrStdTopSpeed;
	
	//Rocket thrust without boost
	private float standardForce = 1000.0f;
	
	//Boost force mutliplier
	private float boostMultiplier = 1.5f;
	//Rotation torque force
	private float rotationTorque = 100.0f;
	
	//Fuel used by rotating
	private float rotateFuel = 4;
	//Fuel used by boosting
	private float boostFuel = 8;
	//Fuel used by normal travel
	private float stdFuel = 2;

	//degrees per rotation
	//DEPRECATED - We want discrete movement. private static float rotangle = 20;
	
	void Start () {
		//Source the RocketAttributes.cs class
		attributes = GetComponent<RocketAttributes>();
		
		//Dan's additions
		this.spin = attributes.spin;
		rocketModel = transform.FindChild("RocketModel");
		
		boostActive = false;
		sqrStdTopSpeed = stdTopSpeed * stdTopSpeed;
		
		//Restrict movement and rotation
		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezePositionZ;

//		rigidbody.velocity=dir;
	}
	
	void Update () {
	}
	
	void FixedUpdate() {
		MoveRocket();
		
		//Dan's Additions
		RotateModel();
	}
	
	public void MoveRocket() {
		//transform.Translate(new Vector3(1,0,0) * Time.fixedDeltaTime);
		
		//End boost if timed out
//		if(boostActive && boostTimer <= 0) {
//			boostActive = false;
//		}
		
		//Get steering input
		if(Input.GetKey(KeyCode.UpArrow) && attributes.getFuel() > 0){
			//rigidbody.AddRelativeTorque(rotationTorque * Vector3.forward * Time.fixedDeltaTime);
//			rigidbody.AddRelativeForce(rotationTorque * Vector3.up * Time.fixedDeltaTime);
			transform.Rotate(rotationTorque * Vector3.forward * Time.fixedDeltaTime);
			attributes.useFuel(rotateFuel*Time.fixedDeltaTime);
		}
		else if(Input.GetKey(KeyCode.DownArrow) && attributes.getFuel() > 0){
			//rigidbody.AddRelativeTorque(rotationTorque * -Vector3.forward * Time.fixedDeltaTime);
//			rigidbody.AddRelativeForce(rotationTorque * -Vector3.up * Time.fixedDeltaTime);
			transform.Rotate(rotationTorque * -Vector3.forward * Time.fixedDeltaTime);
			attributes.useFuel(rotateFuel*Time.fixedDeltaTime);
		}
		
		//Toggle boost
		if(Input.GetKey(KeyCode.Space) && attributes.getFuel() > 0){
			boostActive = true;
			attributes.useFuel(boostFuel * Time.fixedDeltaTime);
		} else {
			boostActive = false;
		}
		
		//Apply thrust
		if(attributes.getFuel() > 0) {
			if(rigidbody.velocity.sqrMagnitude <= sqrStdTopSpeed) { //If we're lower than the top speed
				
				rigidbody.AddRelativeForce(standardForce * Vector3.right * Time.fixedDeltaTime); //Apply thrust
			} if(boostActive && rigidbody.velocity.sqrMagnitude <= 2*sqrStdTopSpeed) { //If we're boosting
				rigidbody.AddRelativeForce(boostMultiplier * standardForce * Vector3.right * Time.fixedDeltaTime); //Apply thrust
			}
			//Spend fuel
			attributes.useFuel(stdFuel*Time.fixedDeltaTime);
		}	
	}
	
	private void Rotate(float angle){
		dir = Quaternion.AngleAxis(angle, Vector3.forward)*dir;
		dir.Normalize();
	}
	
	public void SetDir(Vector3 direction){
		dir = direction;
	}
	
	//Dan's Additions
	/**
	 * Rotates the rocket model about its axis for visual flair
	 **/
	private void RotateModel() {
		rocketModel.RotateAroundLocal(Vector3.right, spin*Time.fixedDeltaTime);
	}
}
