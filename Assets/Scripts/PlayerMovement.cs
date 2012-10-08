using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	//True if the player is allowed to move, false otherwise
	bool canMove;
	float speed = 5.0f;
	
	// Use this for initialization
	void Start () {
		canMove = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(canMove) {
			if(Input.GetKey(KeyCode.UpArrow)) { //If the player presses the 'up' key
				Debug.Log("Moving up.");
				transform.Translate(speed * Vector3.up * Time.deltaTime); //Move the player up
			} if(Input.GetKey(KeyCode.DownArrow)) { //
				transform.Translate(speed * -Vector3.up * Time.deltaTime); //Move the player down
			}
		}
	}
	
	/**
	 * Disables the player movement (sets canMove to false). Called by other scripts
	 **/
	public void DisableMovement() {
		canMove = false;
	}
}
