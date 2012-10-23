using UnityEngine;
using System.Collections;

/**
 * Gravity class. Attached to a 'GravBody' object, such as a planet or a black hole.
 * 
 * The naming convention on this class is:
 * 	'GBody/GravBody' refers to the object to which this code is attached
 * 	'OBody/Orbital body' refers to any object within this GravBody's sphere of influence; 
 * 		i.e. An object orbiting this GBody.
 * 
 * Note: This class violates our scope convention - it modifies the rotation of an orbiting OBody
 * so that the OBody remains in the same orientation about this GBody while within the sphere of influence.
 **/
public class Gravity : MonoBehaviour {
	
	//Toggle method debug statements
	public bool debugMode = false;
	
	public float gravForce = 20;
	
	//Random axis of rotation
	private Vector3 rotationAxis;
	//Random spin speed
	private float spinSpeed;
	//Tracking the objects in orbit around this GBody
	private Hashtable orbitObjects;
	
	// Use this for initialization
	void Start () {
		rotationAxis = new Vector3(Random.Range(-90.0f, 90.0f), Random.Range(-90.0f, 90.0f), Random.Range(-90.0f, 90.0f));
		spinSpeed = Random.Range(-1.0f, 1.0f);
		orbitObjects = new Hashtable();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		RotateGBody();
		//RotateOrbitalBody();
		//UpdateRadialVector();
	}
	
	void OnTriggerEnter(Collider col) {
		AddOrbitalBody(col);
	}
	
	void OnTriggerStay(Collider col) {
		//Attract the OBody radially inward
		AttractOrbitalBody(col);
		
		//Set the orbital GO's rotation
		RotateOrbitalBody(col);
	}
	
	void OnTriggerExit(Collider col) {	
		RemoveOrbitalBody(col);
	}
	
	private void AddOrbitalBody(Collider col) {
		int ident = col.transform.root.gameObject.GetInstanceID(); //Get the GO's instance ID
		
		if(debugMode) Debug.Log(this.GBodyIdent() + ": Current tracked OBodies:\n" + orbitObjects.ToString());
		
		if(!orbitObjects.ContainsKey(ident)) {
			if(debugMode) Debug.Log(this.GBodyIdent()+ ": Adding " + ident);
			orbitObjects.Add(ident, col.transform.root.rotation); //Add the collider's root GO to the HT
		}
	}
	
	private void RemoveOrbitalBody(Collider col) {
		int ident = col.transform.root.gameObject.GetInstanceID(); //Get the GO's instance ID
		if(debugMode) Debug.Log(this.GBodyIdent() + ": Removing " + ident);
		
		orbitObjects.Remove(ident); //Remove the GO from the HT
	}
	
	private void AttractOrbitalBody(Collider col) {
		//Find the radius vector from the collider to the center of this object
		Vector3 radius = (col.transform.position - transform.position).normalized;
		col.attachedRigidbody.AddForce(-gravForce * radius);
	}
	
	/**
	 * Updates the orbital body's rotation to be in the same orbital orientation about this GBody.
	 **/
	private void RotateOrbitalBody(Collider col) {
		GameObject root = col.transform.root.gameObject;
		int ident = root.GetInstanceID(); //Get the GO's instance ID
		if(orbitObjects.ContainsKey(ident)) { //If we're currently tracking this object
			Quaternion prevRad = (Quaternion) orbitObjects[ident]; //Get the previous radius vector
			Quaternion currRad = (Quaternion) root.transform.rotation;
			
			root.transform.rotation = Quaternion.Slerp(prevRad, currRad, 5);//Time.deltaTime);
			//Vector3 difference = currRad - prevRad; //Find the difference between the previous and the current vector
			//Vector3 angDiff = Vector3.
			
		} else { //Otherwise log the error
			Debug.LogError(this.GBodyIdent() + " is not currently tracking GameObject " + ident);
		}
	}
	
	/**
	 * Runs a lerp on an orbital GO's radial vector to transform it from its initial direction
	 * to the GO's local y-axis
	 **/
	private void UpdateRadialVector() {
		//Pass
	}
	
	/**
	 * Rotate this body around the rotationAxis at speed spinSpeed
	 **/
	private void RotateGBody() {
		transform.RotateAroundLocal((rotationAxis), spinSpeed * Time.fixedDeltaTime);
	}
	
	/**
	 * Returns this GBody's instance ID as a string with 'GBody' prepended
	 **/
	private string GBodyIdent() {
		return "GBody " + this.GetInstanceID();
	}
}
