using UnityEngine;
using System.Collections;

public class GameManagement : MonoBehaviour{
	
	bool timingEvent;
	float duration;
	
	void Start(){}
	void Update(){}
	
	//Resets the currently loaded level, on request
	public void ResetLevel(){
		Debug.Log("Resetting Level \"" + Application.loadedLevelName + "\"");
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void ArrowExploded(GameObject detonator) {
		this.duration = detonator.GetComponent<Detonator>().duration;
		//Debug.Log("GameManagement: Panic?");
		//Wait for time
		StartCoroutine(ArrowExplosionTimer());
		
		ResetLevel();
	}
	
	//Yield for the time passed in
	IEnumerator ArrowExplosionTimer() {
		yield return new WaitForSeconds(duration);
	}
}
