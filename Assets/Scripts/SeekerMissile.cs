using UnityEngine;
using System.Collections;

public class SeekerMissile : MonoBehaviour {
	private float mag = 3;	//Rate at which the rocket speeds up
	private Vector3 current;
	private float timeOut;
	private SeekerMissileSpawner par;
	private GameObject target;	//The player missile in almost every case

	void Start(){
		//pass
	}
	
	public void InitEnemy(Vector3 direction, float s, float to, GameObject t){
		current = s * direction.normalized;
		mag = s;
		Destroy(gameObject, to);
		target = t;
	}

	void FixedUpdate(){
		Vector3 toAdd = (mag*(target.transform.position - transform.position)* Time.fixedDeltaTime);  
		current = current + toAdd;
		transform.Translate(current * Time.fixedDeltaTime);	
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "Rocket"){
			Debug.Log("Rocket hit by Seeker Missile! Game Over");
			Camera.main.GetComponent<GameManagement>().ResetLevel();
		}
		Destroy(gameObject);
	}



}
