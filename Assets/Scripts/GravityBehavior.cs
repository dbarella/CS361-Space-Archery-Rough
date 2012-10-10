using UnityEngine;
using System.Collections;

public class GravityBehavior : MonoBehaviour {
	
	public float gravForce = 20;
	public float orbitVelocity = 10;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.RotateAroundLocal(new Vector3(0, .9f, -.3f), .5f * Time.fixedDeltaTime);
	}
	
	void OnTriggerStay(Collider col) {
		//Find the radius vector from the collider to the center of this object
		Vector3 radius = (col.transform.position - transform.position).normalized;
		col.attachedRigidbody.AddForce(-gravForce * radius);
		col.transform.root.RotateAround(transform.position, Vector3.forward, orbitVelocity*Time.fixedDeltaTime);
	}
}
