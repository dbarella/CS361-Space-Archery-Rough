using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
	
	void OnTriggerEnter(Collider col){
		if(col.tag == "Arrow") { //If the object is an arrow, run Arrow.Die()
			Debug.Log("Wall: Hit by Arrow. Calling Arrow.Die()");
			
			//Kill the Arrow
			GameObject.FindWithTag("Arrow").GetComponent<Arrow>().Die();
		} else { //Otherwise, destroy the object
			string t = col.tag; //Get the collider's tag
			Debug.Log("Wall: Hit by " + t + ". Destroying " + t + ".");
			
			//Destroy the object
			Destroy(col.transform.root.gameObject);
		}
	}
}

