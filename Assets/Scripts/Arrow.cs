using UnityEngine;
using System.Collections;
public class Arrow : MonoBehaviour {

	//Object References
	//Reference to the Explosion object
	public GameObject explosion;
	//Reference to the game management
	protected GameManagement mgmt;
	//Reference to the attached model
	protected Transform arrowModel;

	//General Attributes
	public float fuel = 50;
	public float health = 100;
	//Spin speed
	public float spin = 2.0f;
	//true if the boost is on, false otherwise
	protected bool boostActive;
	//true if we brake, false otherwise;
	protected bool brakeActive;
	//Fade out time after the rocket explodes
	protected float fadeOutTime;

	//Speed Limits
	public static float minSpeed = 5.0f;
	public static float stdSpeed = 15.0f;
	public static float maxSpeed = 50.0f;
	
	//Multipliers!
	public float boostMultiplier = 1.5f;
	public float brakeMultiplier = 0.75f;
	//public float reAccelMultiplier = 1.2f;
	public float accelMultiplier = 1.01f;
	public float decelMultiplier = 0.9f;

	//our forward speed
	public float forwardSpeed;
	//the speed we want
	public float desiredSpeed;
	//Standard force
	//public float standardForce = 750.0f;
	//re-acceleration force (to get back up to std after braking)
	//public float reAccelForce = 400.0f;
	//Braking force
	//public float brakeForce = 500.0f;
	//Rotation torque
	public float rotationTorque = 100.0f;

	//Fuel Expediture
	//Fuel used by braking
	protected float brakeFuel = 6;
	//Fuel used by rotating
	protected float rotateFuel = 4;
	//Fuel used by boosting
	protected float boostFuel = 8;
	//Fuel used by normal travel
	protected float stdFuel = 2;//Brendan: Do we have a way to decide how much fuel is given per level yet?

	void Awake() {
		//Source the Game Management
		mgmt = Camera.main.GetComponent<GameManagement>();

		//Source the Arrow Model
		arrowModel = transform.FindChild("ArrowModel");	
	}

	public void Start () {
		//Set boost field initially to false
		boostActive = false;
		brakeActive = false;
		forwardSpeed = stdSpeed;
		desiredSpeed = stdSpeed;
		//Restrict movement and rotation
		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezePositionZ;
	}

	public void FixedUpdate() {
		Move();
		RotateModel();

		if(fuel <= 0) { //If the arrow runs out of fuel, it's dead
			Die();
			//Brendan: Is this going to be arrow's behavior from now on, or a placeholder?
		}
	}

	public void Move() {
		//Get steering input
		if(Input.GetKey(KeyCode.A) && fuel > 0) {
			transform.Rotate(rotationTorque * Vector3.forward * Time.deltaTime);
			//UseFuel(rotateFuel*Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.D) && fuel > 0) {
			transform.Rotate(rotationTorque * -Vector3.forward * Time.deltaTime);
			//UseFuel(rotateFuel*Time.deltaTime);
		}
		
		if(Input.GetKey(KeyCode.S) && fuel > 0) {//if we brake
			brakeActive = true;
		}else{
			brakeActive = false;
		}
		
		//Toggle boost
		if((Input.GetKey(KeyCode.Space) || (Input.GetKey(KeyCode.W))) && fuel > 0){
			boostActive = true;
		} else {
			boostActive = false;
		}

		//Apply thrust
		if(fuel > 0) { //If we have fuel
			
			if(boostActive && desiredSpeed < maxSpeed) { //If boosting and not at top speed
				desiredSpeed = Accelerate(desiredSpeed);	//add the boost multiplier
				
			}
			if(brakeActive && desiredSpeed > minSpeed) {//if braking and not at minspeed
				desiredSpeed = Decelerate(desiredSpeed);	//add brake multiplier
			}
			if(forwardSpeed < stdSpeed) { //If below stdSpeed, get back up there
				desiredSpeed = Accelerate(desiredSpeed);
				
			}
			if(forwardSpeed < desiredSpeed) {
				forwardSpeed = Accelerate(forwardSpeed);	
			}else if(forwardSpeed > desiredSpeed) {
				forwardSpeed = Decelerate(forwardSpeed);	
			}
			transform.Translate(forwardSpeed * Vector3.right * Time.deltaTime);
		}
	}

	private float Accelerate(float fs){
		return (fs * accelMultiplier);
	}
	
	private float Decelerate(float fs){
		return (fs * decelMultiplier);
	}

	/**
	 * Rotates the arrow model about its axis for visual flair
	 **/
	private void RotateModel() {
		arrowModel.RotateAroundLocal(Vector3.right, spin*Time.deltaTime);
	}

	/**
	 * Kills the arrow and calls management to reset the level
	 **/
	public void Die() {//Brendan: Oooh, this is fancy now!
		//Instantiate the Explosion
		GameObject d = Instantiate(explosion, transform.position, transform.rotation) as GameObject;

		//Inform Management of the event
		Debug.Log("Arrow: Calling GameManagement.ArrowExploded()");
		mgmt.ArrowExploded(d);

		//Destroy this Arrow
		Destroy(gameObject);
	}

	//Getters and Setters
	public float GetFuel(){
		return fuel;
	}//Brendan: Would a setFuel method be helpful here?
	public void SetFuel(float f){//Brendan: I wrote one for the hell of it, just in case.
		fuel = f;
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
	public float GetTopSpeed() {
		return maxSpeed;
	}
}