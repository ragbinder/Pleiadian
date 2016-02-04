using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	public Vector3 strafeVelocityMax;
	public float engineVelocityMax = 1.0F;
	double topSpeed;
	private float throttle;
	private float throttleSensitivity = .05F;
	private Rigidbody2D rb;
	public float acceleration = 3;

	GameObject[] subsystems;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		//  Vector3 cameraPos = Camera.main.transform.position;
		//  cameraPos.x = transform.position.x;
		//  cameraPos.y = transform.position.y;
		//  Camera.main.transform.position = cameraPos;

	}
	
	void OnGUI () {
		GUI.Label (new Rect (10,10,150, 100), "" + throttle);
        GUI.Label (new Rect (10,20,150, 100), "" + rb.velocity);
	}
	
	void FixedUpdate () {
        handleInput ();

		rb.AddForce (transform.up * throttle * acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, engineVelocityMax);
	}

	void handleInput () {

		//Throttle Behavior
		float vertical = Input.GetAxis ("Vertical");

		if (vertical > 0) {
			throttle += throttleSensitivity;
		}
		if (vertical < 0) {
			throttle -= throttleSensitivity;
		}
		throttle = Mathf.Clamp01 (throttle);

        //Facing Behavior
		// Vector3 newHeading = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		// diff.Normalize();
		
		// float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		// transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        
		//Turning
		float horizontal = Input.GetAxis ("Horizontal");

		if (horizontal < 0) {
			transform.RotateAround(transform.position,Vector3.forward,1F);	                                              
		}
		if (horizontal > 0) {
			transform.RotateAround (transform.position, Vector3.forward, -1F);
		}

//		Debug.Log (throttle);
//		Debug.Log (horizontal);
	}
}
