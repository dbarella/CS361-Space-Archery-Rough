using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	//True if the player is allowed to move, false otherwise
	bool canMove;
	float speed = 5.0f;
	
	//Movement Boundaries
	private float max_y;
	private float min_y;
	float buffer = -2;
	
	// Use this for initialization
	void Start () {
		canMove = true;
		
		//Define movement boundaries
		max_y = Camera.main.orthographicSize + buffer;
		min_y = - max_y;
	}
	
	// Update is called once per frame
	void Update () {
		if(canMove) {
			if(Input.GetKey(KeyCode.UpArrow) && (transform.position.y < max_y)) { //If the player presses the 'up' key
				transform.Translate(speed * Vector3.up * Time.deltaTime); //Move the player up
			} if(Input.GetKey(KeyCode.DownArrow) && (transform.position.y > min_y)) { //
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
