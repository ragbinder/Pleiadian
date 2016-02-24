using UnityEngine;
using System.Collections;

public class Beam : Projectile {
    public float beamLength = 20F;
	// Use this for initialization
	void Start () {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0,transform.position);
        lineRenderer.SetPosition(1,transform.TransformPoint(new Vector3(0F,beamLength,0F)));
        
        Vector3 origin = transform.position;
        Vector3 direction = transform.TransformDirection(transform.up);
        RaycastHit2D hit = Physics2D.Raycast(origin,direction);
        if(hit.collider) {
            Debug.Log("Hit " + hit.collider);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
