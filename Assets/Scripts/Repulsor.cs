using UnityEngine;
using System.Collections;

/*This script for a repulsor object is to be attached to a trigger object, without a rigidbody or 
 * mesh. Any object with a rigidbody that enters the repulsor will be pushed along the repulsor's
 * z axis, in the positive direction, at a rate dependent on str.
 * By: Adam Stafford 10/16/12
 */
public class Repulsor : MonoBehaviour{
	public float str;	//The strength of the repulsor
	
	public void OnTriggerStay(Collider col){
		//Note that this will move any object in the local transform's +z direction.
		col.attachedRigidbody.AddForce(this.transform.forward * str);
	}
}
