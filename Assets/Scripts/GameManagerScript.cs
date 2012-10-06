using UnityEngine;

public class GameManager: MonoBehaviour{
	
	void Start(){}
	void Update(){}
	
	//Resets the currently loaded level, on request
	void Reset(){
		Application.LoadLevel(Application.loadedLevel);
	}
}
