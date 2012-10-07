using UnityEngine;
using System.Collections;

public class TargetBehavior : MonoBehaviour {
	
	//Reference to the GameManager
	GameManagement mgmt;
	
	//Dev Variable
	private float spin = 1.0f;
	
	// Use this for initialization
	void Start () {
		mgmt = Camera.main.GetComponent<GameManagement>();
		rigidbody.AddRelativeTorque(Vector3.forward * spin, ForceMode.VelocityChange);
	}
	
	// Update is called once per frame
	void Update () {
		//Pass
	}
	
	void OnTriggerEnter(Collider col) {
		if(col.tag == "Rocket") { //If we're hit by a rocket
			Debug.Log("Target hit by rocket.");
			Destroy(col);
			mgmt.ResetLevel();
		}
	}
}
