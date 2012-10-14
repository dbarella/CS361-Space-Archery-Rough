using UnityEngine;
using System.Collections;

public class DemoFollowCam : MonoBehaviour {
	
	GameObject arrow;
	public float followDist = -5.0f;
	
	// Use this for initialization
	void Start () {
		arrow = GameObject.FindWithTag("arrow");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3(arrow.transform.position.x, arrow.transform.position.y, followDist);
	}
}
