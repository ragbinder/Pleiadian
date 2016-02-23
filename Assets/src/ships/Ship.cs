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
    public float turnRate = 2.5F;

	GameObject[] subsystems;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {

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
        
		//Turning
		float horizontal = Input.GetAxis ("Horizontal");

		if (horizontal < 0) {
			transform.RotateAround(transform.position,Vector3.forward,turnRate);	                                              
		}
		if (horizontal > 0) {
			transform.RotateAround(transform.position, Vector3.forward,-turnRate);
		}
	}
}
