 using UnityEngine;
 using System.Collections;
 

public class Bow : MonoBehaviour{
	
	//Development Variables
	Vector3 offset = new Vector3(0,0,0);
	
	//Direction to the user's cursor
    protected Vector3 dir;
	
	//Angle between the x-axis and the user's cursor
    protected float theta;
	
	//Cursor texture
	public Texture2D cursorImage;
	
	//Rocket GO
	public GameObject rocket;
	
    public void Start () {
		Screen.showCursor = false;
	}
    
    public void Update () {
//		Debug.Log("Direction: " + dir + "Theta:" + theta);			
    }
	
	public void OnGUI() {
		//Draw the mouse cursor
		DrawCursor();
		
		if(Input.GetMouseButton(0)) { //If the left mouse button is down
			//Set the parameters of the cursor
			SetParams();
		} if(Event.current.type == EventType.mouseUp) {
			Fire();
		}
	}
	
	/**
	 * This method launches the rocket upon releasing the mouse button.
	 **/
	public void Fire() {
//		Debug.Log(theta);
//		Debug.Log (Quaternion.AngleAxis(theta, Vector3.forward));
		GameObject rocket = Instantiate(this.rocket, transform.position + offset, Quaternion.LookRotation(-dir)/*AngleAxis(theta, new Vector3(1, 0, 0))*/) as GameObject;
		
		//Set the rocket's direction
		RocketMovement rck = rocket.GetComponent<RocketMovement>();
		rck.setDir(dir);
		
		//Rotate the model along its axis just for fun
		//Transform rckModel = rocket.transform.FindChild("RocketModel");
		//rigidbody.AddRelativeTorque(Vector3.forward * spin, ForceMode.VelocityChange);
	}
	
	/**
	 * Draws a sphere primitive under the mouse pointer so the user has an indication
	 * of where they're aiming and the aiming boundaries.
	 **/
	public void DrawCursor() {
		//Working Here
		Rect cur = new Rect(Input.mousePosition.x - transform.position.x, Screen.height - Input.mousePosition.y, 32, 32);
//		Debug.Log(cur.y);
		GUI.Label(cur, cursorImage);
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
        this.dir = new Vector3(mouseVector.x - transform.position.x, mouseVector.y - transform.position.y, 0);
        this.theta = Mathf.Atan(dir.y/dir.x) * (180.0f / Mathf.PI); //Convert to degrees
		
		//Draw the ray just to be sure we've got the correct vector calculations.
		Debug.DrawRay(transform.position, dir, Color.green);	
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