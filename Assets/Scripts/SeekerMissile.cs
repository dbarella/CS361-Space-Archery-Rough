using UnityEngine;
using System.Collections;

public class SeekerMissile : MonoBehaviour {
	private float mag = 3;	//Rate at which the rocket speeds up
	private Vector3 current;
	private float timeOut;
	private SeekerMissileSpawner par;
	private GameObject target;	//The player missile in almost every case
	
	void Start(){
		//Pass
	}
	
	public void InitEnemy(Vector3 direction, float s, float to, GameObject t){
		current = s * direction.normalized;
		mag = s;
		Destroy(gameObject, to);
		target = t;
	}

	void FixedUpdate(){
		//If the target has been destroyed, die.
		if(target == null){
			Destroy (gameObject);
		}
		Vector3 toAdd = (mag*(target.transform.position - transform.position)* Time.fixedDeltaTime);  
		current = current + toAdd;
		transform.Translate(current * Time.fixedDeltaTime);	
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "Arrow"){
			Debug.Log("EnemyMissile hit Arrow. Calling Arrow.Die()");
			Arrow a = GameObject.FindWithTag("Arrow").GetComponent<Arrow>(); //Grab a ref to the Arrow
			
			//Kill the arrow
			a.Die();
		}
		Destroy(gameObject); //Destroy this SeekerMissile
	}



}
