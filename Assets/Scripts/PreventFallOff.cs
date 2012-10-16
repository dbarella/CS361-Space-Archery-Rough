using UnityEngine;
using System.Collections;

public class PreventFallOff : MonoBehaviour {
	
	//Reference to GameManagement
	GameManagement mgmt;
	
	//x-axis boundaries
	private float max_x;
	private float min_x;

	//y-axis boundaries
	private float max_y;
	private float min_y;

	public void Start() {
		//Source the management
		mgmt = Camera.main.GetComponent<GameManagement>();
		
		max_y = Camera.main.orthographicSize;
		min_y = - max_y;
	
		max_x = (max_y) * Camera.main.aspect;
		min_x = -max_x;
	}

	public void Update() {
		//Reset the game if this object falls off the screen
		if((transform.position.x > max_x) || (transform.position.x < min_x)
			|| (transform.position.y > max_y) || (transform.position.y < min_y)) {
				Debug.Log("PreventFallOff: " + this.tag + " fell off the map.");
				if(this.transform.root.gameObject.tag == "Arrow") {
					Debug.Log("PreventFallOff: Calling Arrow.Die()");
					GetComponent<Arrow>().Die();
				} else {
					mgmt.ResetLevel();
				}
		}
	}
}