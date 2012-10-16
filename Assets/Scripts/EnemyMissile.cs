using UnityEngine;
using System.Collections;

public class EnemyMissile : MonoBehaviour {
	private float mag = 3;
	private Vector3 dir;
	private float timeOut;	//Counter until detonation
	
	void Start () {
		//pass
	}
	public void InitEnemy(Vector3 direction, float s, float to) {
		dir = direction.normalized;//Set the direction of the object
		mag = s;	//Set the speed of the object
		Destroy(gameObject, to);//Destroy yourself after TimeOut time
	}
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(mag*dir * Time.fixedDeltaTime);
	}
	
	void OnTriggerEnter(Collider col){
		if(col.tag == "Arrow"){
			Debug.Log("EnemyMissile hit Arrow. Calling Arrow.Die()");
			Arrow a = GameObject.FindWithTag("Arrow").GetComponent<Arrow>(); //Grab a ref to the Arrow
			
			//Kill the arrow
			a.Die();
		}
		Destroy(gameObject); //Destroy this EnemyMissile
	}
}
