/*
Wormhole Behavior script:
Defines how wormhole objects interact with the world.
That is, a wormhole will teleport the arrow (preserving momentum and global direction of the arrow) to a sister wormhole, given in the inspector as a public variable. No other objects are affected. The wormhole will disable the teleportation effect of the sister wormhole for some time period, to prevent crazy spaz warping.
*/

using UnityEngine;
using System.Collections;

public class Wormhole : MonoBehaviour{
	
	public GameObject exit;	//The exit wormhole
	public float coolDown;		//The time the exit will be disabled after a teleport
	float timer;		//The time left until we can warp
	void Update(){
		//Decrement the timer, so we know when we can warp again
		if(timer >= 0)
			timer -= Time.deltaTime;
	}
	
	void Start(){
		timer = 0;
	}
	
	//When colliding with a arrow, teleport it and set the exit's timer
	void OnTriggerEnter(Collider other){
		Debug.Log(other.gameObject.name + " warped to: " + exit);
		if(other.tag == "Arrow" && timer<0){
			other.transform.parent.transform.position = exit.transform.position;
			exit.GetComponent<Wormhole>().timer = coolDown;
		}
	}
}
