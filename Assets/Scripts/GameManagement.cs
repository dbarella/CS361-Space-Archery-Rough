using UnityEngine;
using System.Collections;

public class GameManagement : MonoBehaviour{
	
	bool timingEvent;
	float duration;

	void Awake() {}	
	void Start(){}
	void Update(){}
	
	//Resets the currently loaded level, on request
	public void ResetLevel(){
		Debug.Log("GameManagement: Resetting Level \"" + Application.loadedLevelName + "\"");
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void ArrowExploded(GameObject detonator) {
		this.duration = detonator.GetComponent<Detonator>().duration;
		//Debug.Log("GameManagement: Panic?");
		//Wait for time
		ArrowExplosionTimer();
	}
	
	//Yield for the time passed in
	IEnumerator ArrowExplosionTimer() {
		yield return new WaitForSeconds(3);
		ResetLevel();
	}
}
