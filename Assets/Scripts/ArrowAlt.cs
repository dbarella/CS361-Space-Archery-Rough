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

public class ArrowAlt : Arrow {
	public float brakeForce = 200.0f;
	
	new public void Move() { //Override Arrow.Move()
		//Get steering input
		if(Input.GetKey(KeyCode.UpArrow) && fuel > 0) {
			transform.Rotate(rotationTorque * Vector3.forward * Time.fixedDeltaTime);
			base.UseFuel(rotateFuel*Time.fixedDeltaTime);
		}
		else if(Input.GetKey(KeyCode.DownArrow) && fuel > 0) {
			transform.Rotate(rotationTorque * -Vector3.forward * Time.fixedDeltaTime);
			base.UseFuel(rotateFuel*Time.fixedDeltaTime);
		}
		
		//Toggle boost
		if(Input.GetKey(KeyCode.Space) && fuel > 0){
			boostActive = true;
		} else {
			boostActive = false;
		}
		if(fuel > 0) { //If we have fuel
			float forwardSpeed = Vector3.Dot(rigidbody.velocity, transform.forward); //Find our forward speed
			
			//If we aren't boosting, we continue along our current heading, and can turn around to set up our next boost
			if(boostActive && forwardSpeed <= boostSpeedMultiplier * stdTopSpeed) { //If boosting, add boost force
				rigidbody.AddRelativeForce(boostMultiplier * standardForce * Vector3.right * Time.fixedDeltaTime);
				//Spend fuel
				base.UseFuel(boostFuel * Time.fixedDeltaTime);
			}
			if(!boostActive && forwardSpeed > stdTopSpeed) {
				rigidbody.AddRelativeForce (brakeForce * Vector3.left * Time.fixedDeltaTime);	
			}
		}		
	}
}