using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {
	
	//Reference to the GameManager
	//GameManagement mgmt;
	
	//Dev Variable
	private float spin = 1.0f;
	public float xBuffer = 30;
	
	// Use this for initialization
	void Start () {
//		mgmt = Camera.main.GetComponent<GameManagement>();
		rigidbody.AddRelativeTorque(Vector3.forward * spin, ForceMode.VelocityChange); //Rotate the target
		
		//Lock the target to be xBuffer pixels to the left of the right camera bound
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - xBuffer, Camera.main.pixelHeight/2, -Camera.main.transform.position.z));
	}
	
	// Update is called once per frame
	void Update () {
		//Pass
	}
	
	void OnTriggerEnter(Collider col) {
		if(col.tag == "Arrow") { //If we're hit by a arrow
			Debug.Log("Target: Hit by arrow.");
			GameObject.FindWithTag("Arrow").GetComponent<Arrow>().Die(); //Kill the arrow
		}
	}
}
