 using UnityEngine;
 using System.Collections;
 

public class Bow : MonoBehaviour{
	
	//Direction to the user's cursor
    protected Vector3 dir;
	
	//Angle between the x-axis and the user's cursor
    protected float theta;
	
    public void Start () {
		//Pass
	}
    
    public void Update () {
//		Debug.Log("Direction: " + dir + "Theta:" + theta);			
    }
	
	public void OnGUI() {
		if(Event.current.type == EventType.MouseDown) { //If the left mouse button is down
			//Set the parameters of the cursor
			SetParams();
			
			//Draw the mouse cursor
			DrawCursor();
		}
	}
	
	/**
	 * This method launches the rocket upon releasing the mouse button.
	 **/
	public void Fire() {
		//Pass	
	}
	
	/**
	 * Draws a sphere primitive under the mouse pointer so the user has an indication
	 * of where they're aiming and the aiming boundaries.
	 **/
	public void DrawCursor() {
		//Working Here
	}
	
	/**
	 * Sets the rocket headings on mouse down.
	 * @see dir
	 * @see theta
	 **/
	public void SetParams() {
		//Temporary mouse vector
		Vector3 mouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		//Set our parameters
        this.dir = new Vector3(mouseVector.x - transform.position.x, mouseVector.y, 0);
        this.theta = Mathf.Atan(dir.y/dir.x);
		
		//Draw the ray just to be sure we've got the correct vector calculations.
		Debug.DrawRay(transform.position, dir, Color.blue);	
	}
    
	/**
	 * Returns the vector from this object to the mouse cursor.
	 **/
    public Vector3 GetDir () {
        return dir;
    }
    
	/**
	 * Returns theta from the mouse cursor to the x-axis.
	 **/
    public float GetTheta () {
        return theta;
    }
}