 using UnityEngine;
 using System.Collections;
 

public class Bow : MonoBehaviour{
	
	//Rocket GO
	public GameObject rocket;
	
	//Player GO
	public GameObject player;
	
	//Development Variables
	Vector3 offset = new Vector3(1,0,0);
	
	//Direction to the user's cursor
    protected Vector3 dir;
	
	//Angle between the x-axis and the user's cursor
    protected float theta;
	
	//Maximum allowed top speed
	public float stdTopSpeed;
	
	//Cursor texture
	public Texture2D cursorImage;
	
	//rocketFired - true if the rocket has been fired, false otherwise
	private bool rocketFired;
	
	//position where mouse is first clicked
	private Vector3 mouseStart;
	
    public void Start () {
		Screen.showCursor = false;
		player = GameObject.FindWithTag("Player");
		stdTopSpeed = RocketMovement.stdTopSpeed;
		rocketFired = false;
		mouseStart = transform.position;
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
		} if(Event.current.type == EventType.mouseUp && !rocketFired) {
			Fire();
		} else if(Event.current.type == EventType.mouseDown && !rocketFired){
			mouseStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
	}
	
	/**
	 * This method launches the rocket upon releasing the mouse button.
	 **/
	public void Fire() {
		GameObject rocket = Instantiate(this.rocket, transform.position + offset, Quaternion.LookRotation(dir, Vector3.forward)/*AngleAxis(theta, new Vector3(1, 0, 0))*/) as GameObject;
		rocket.transform.Rotate(new Vector3(-90,90,180)); //Fix the rocket's rotation
		
		//Set the rocket's direction
		RocketMovement rck = rocket.GetComponent<RocketMovement>();
		rck.SetDir(dir); //Set the rocket's direction vector
		
		//Set the rocket's initial velocity
		float dirMag = dir.magnitude;
		if(dirMag > stdTopSpeed) { //If the magnitude that the use wants is too great, set it to the top speed
			rck.rigidbody.velocity = stdTopSpeed*dir.normalized;
		} else { //Otherwise, se the velocity to dir
			rck.rigidbody.velocity = dir;
		}
			
		//Disable player movement
		player.GetComponent<PlayerMovement>().DisableMovement();
		rocketFired = true;
	}
	
	/**
	 * Draws a mousepointer under the mouse pointer so the user has an indication
	 * of where they're aiming and the aiming boundaries.
	 **/
	public void DrawCursor() {
		//Working Here
		Rect cur = new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 32, 32);
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
        this.dir = new Vector3(mouseStart.x - mouseVector.x, mouseStart.y - mouseVector.y, 0);
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