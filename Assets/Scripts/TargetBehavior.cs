using UnityEngine;
using System.Collections;

public class TargetBehavior : MonoBehaviour {
	
	//Reference to the GameManager
	GameManagement mgmt;
	
	//Dev Variable
	private float spin = 1.0f;
	public float xBuffer = 30;
	
	// Use this for initialization
	void Start () {
		mgmt = Camera.main.GetComponent<GameManagement>();
		rigidbody.AddRelativeTorque(Vector3.forward * spin, ForceMode.VelocityChange);
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - xBuffer, Camera.main.pixelHeight/2, -Camera.main.transform.position.z));
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
