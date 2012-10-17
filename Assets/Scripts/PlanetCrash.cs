using UnityEngine;
using System.Collections;

public class PlanetCrash : MonoBehaviour {


	
	void OnTriggerEnter(Collider col){
		//print ("Derp!");
		Arrow other;
		other = col.gameObject.GetComponent("Arrow") as Arrow;
		if(other != null) other.Die();
		//fixed!
		
		
		
	}
}

