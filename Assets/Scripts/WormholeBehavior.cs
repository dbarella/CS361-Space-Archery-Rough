/*
Wormhole Behavior script:
Defines how wormhole objects interact with the world.
That is, a wormhole will teleport the rocket (preserving momentum and global direction of the rocket) to a sister wormhole, given in the inspector as a public variable. No other objects are affected. The wormhole will disable the teleportation effect of the sister wormhole for some time period, to prevent crazy spaz warping.
*/

using UnityEngine;
using System.Collections;

public class WormholeBehavior : MonoBehaviour{
	
	public GameObject exit;	//The exit wormhole
	public float coolDown;		//The time the exit will be disabled after a teleport
	float timer;		//The time left until we can warp
	void Update(){
		//Decrement the timer, so we know when we can warp again
		timer -= Time.deltaTime;
	}
	
	void Start(){
		//Pass
	}
	
	//When colliding with a rocket, teleport it and set the exit's timer
	void OnTriggerEnter(Collider other){
		if(other.tag == "Rocket" && timer<0){
			other.transform.position = exit.transform.position;
			exit.GetComponent<WormholeBehavior>().timer = coolDown;
		}
	}
}
