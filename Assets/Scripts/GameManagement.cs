using UnityEngine;
using System.Collections;

public class GameManagement : MonoBehaviour{
	
	void Start(){}
	void Update(){}
	
	//Resets the currently loaded level, on request
	public void ResetLevel(){
		Application.LoadLevel(Application.loadedLevel);
	}
}
