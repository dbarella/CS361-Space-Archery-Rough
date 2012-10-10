using System.Collections;
using UnityEngine;

/*HUD
 * Handles the display of fuel and health bars to the user
 * Currently takes up the top 20 pixels of the screen.
 * By Adam Stafford - 10/9/12
 * */
public class HUD : MonoBehaviour {
	
	float maxFuel=0;	//The amount of fuel the rocket started with
	float curFuel=0;	//The amount of fuel the rocket has right now
	float maxHealth=0;	//Maximum Health
	float curHealth=0;	//Current Health
	RocketAttributes ra;	//The RocketAttributes script we'll be referencing
	
	//Initialize values
	void Start(){
		ra = GameObject.FindWithTag("Rocket").GetComponent<RocketAttributes>();	//TODO placeholder - I need to figure out how to reference this later.
		maxFuel = ra.getFuel();	//Only need to do this once.
		maxHealth = ra.getHealth();
	}
	
	//Update the counters to reflect the actual rocket's fuel/health
	void Update(){
		//Update the current values, without changing the maximum values.
		curFuel = ra.getFuel();
		curHealth = ra.getHealth();
	}
	
	//Draws the fuel bar on the screen
	void OnGUI(){
		//This will draw a shorter rectangle if the player has less health. Max is half screen.
		GUI.Box(new Rect(10,10,((Screen.width)/ 2) / (maxHealth/curHealth), 10),"H:" + curHealth + "/" + maxHealth);
		//Same for fuel
		GUI.Box(new Rect(20,20,((Screen.width)/ 2) / (maxFuel/curFuel), 10),"F:" + curFuel + "/" + maxFuel);
	}
}

