using UnityEngine;
using System.Collections;
public class ArrowOld : MonoBehaviour {
	
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
	//Fade out time after the rocket explodes
	protected float fadeOutTime;
	
	//Speed Limits
	//Standard top speed
	public static float stdTopSpeed = 35.0f;
	//Boost top speed multiplier
	public float boostSpeedMultiplier = 1.2f;
	
	//Thrust Force
	//Standard force (without boost)
	public float standardForce = 200.0f;
	//Boost force mutliplier
	public float boostMultiplier = 1.75f;
	//Rotation torque
	public float rotationTorque = 100.0f;
	
	//Fuel Expediture
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
			float forwardSpeed = Vector3.Dot(rigidbody.velocity, transform.right); //Find our forward speed
			if(!boostActive && forwardSpeed <= stdTopSpeed) { //If not boosting and not at top speed, add force
				rigidbody.AddRelativeForce(standardForce * Vector3.right * Time.fixedDeltaTime);	
				//Spend fuel
				UseFuel(stdFuel*Time.fixedDeltaTime);
			} else if(boostActive && forwardSpeed <= boostSpeedMultiplier * stdTopSpeed) { //If boosting, add boost force
				rigidbody.AddRelativeForce(boostMultiplier * standardForce * Vector3.right * Time.fixedDeltaTime);
				//Spend fuel
				UseFuel(boostFuel * Time.fixedDeltaTime);
			} //else if(forwardSpeed > stdTopSpeed) { //If we're at the top speed, we'll apply a dampener
				rigidbody.AddRelativeForce(-Vector3.right * Time.fixedDeltaTime);
			//}
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
		return stdTopSpeed;
	}
}
