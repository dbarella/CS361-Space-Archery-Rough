using UnityEngine;
using System.Collections;

public class DemoFollowCam : MonoBehaviour {
	
	GameObject rocket;
	public float followDist = -5.0f;
	
	// Use this for initialization
	void Start () {
		rocket = GameObject.FindWithTag("Rocket");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3(rocket.transform.position.x, rocket.transform.position.y, followDist);
	}
}
