 using UnityEngine;
 using System.Collections;
 //Brendan Wuz Here Also!

public class Bow : MonoBehaviour{
	
	//Inspector Fields
	//Arrow GO
	public GameObject arrow;
	
	//Player GO
	public GameObject player;
	
	//Cursor texture
	public Texture2D cursorImage;
	
	//Internal Fields
	//Offset from the bow at which the Arrow is instantiated
	private Vector3 offset = new Vector3(1,0,0);
	
	//Direction to the user's cursor
    protected Vector3 dir;
	
	//Angle between the x-axis and the user's cursor
    protected float theta;
	
	//Reference to the Arrow's maximum allowed top speed
	private float stdTopSpeed;
	
	//arrowFired - true if the arrow has been fired, false otherwise
	private bool arrowFired;
	
	//position where mouse is first clicked
	private Vector3 mouseStart;
	
    public void Start () {
		//Disable the system cursor - we draw our own custom cursor
		Screen.showCursor = false;
		
		//Find the player to which this script is attached
		player = transform.root.gameObject;
		
		//Source the Arrow's top speed
		stdTopSpeed = Arrow.stdTopSpeed;

		arrowFired = false;
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
		} if(Event.current.type == EventType.mouseUp && !arrowFired) {
			Fire();
		} else if(Event.current.type == EventType.mouseDown && !arrowFired){
			mouseStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
	}
	
	/**
	 * This method launches the arrow upon releasing the mouse button.
	 **/
	public void Fire() {
		if(dir == Vector3.zero) { //Ignores false input when clicking on the window from another window
			return;
		}
		
		GameObject arrow = Instantiate(this.arrow, transform.position + offset, Quaternion.LookRotation(dir, Vector3.forward)) as GameObject;
		arrow.transform.Rotate(new Vector3(-90,90,180)); //Fix the arrow's rotation
		
		//Source the Arrow
		Arrow a = arrow.GetComponent<Arrow>();
		
		//Set the arrow's initial velocity
		float dirMag = dir.magnitude;
		if(dirMag > stdTopSpeed) { //If the magnitude that the use wants is too great, set it to the top speed
			arrow.rigidbody.velocity = stdTopSpeed*dir.normalized;
		} else { //Otherwise, se the velocity to dir
			a.rigidbody.velocity = dir;
		}
			
		//Disable player movement
		Debug.Log("Bow: calling Player.DisableMovement()");
		player.GetComponent<Player>().DisableMovement();
		arrowFired = true;
	}//Brendan: This all looks good to me - never seen this mess up, so we're good here.
	
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
	 * Sets the arrow headings on mouse down.
	 * @see dir
	 * @see theta
	 **/
	public void SetParams() {//Brendan: Is this where the arrow's fuel is set?
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