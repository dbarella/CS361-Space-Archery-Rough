using UnityEngine;
using System.Collections;

public class EnemyMissile : MonoBehaviour {
	private float mag = 3;
	private Vector3 dir;
	private float timeOut;	//Counter until detonation
	private EnemyMissileSpawner par;	//Pointer to the parent missile launcher
	
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
		if(col.tag == "Rocket"){	//If we hit the rocket, reset the game
			Debug.Log("Rocket hit by Missile! Game Over");
			Camera.main.GetComponent<GameManagement>().ResetLevel();
		}
		Destroy(gameObject);
	}
}
