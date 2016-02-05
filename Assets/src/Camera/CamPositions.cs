using UnityEngine;
using System.Collections;

struct CameraState {
	public Vector3 position;
	public Quaternion rotation;

	public CameraState(Vector3 pos, Quaternion rot)
	{
		position = pos;
		rotation = rot;
	}

	public static bool operator !=(CameraState x, CameraState y) {
		if (x.position != y.position || x.rotation != y.rotation)
			return true;
		return false;
	}

	public static bool operator ==(CameraState x, CameraState y) {
		if (x.position == y.position && x.rotation == y.rotation)
			return true;
		return false;
	}
}

public class CamPositions : MonoBehaviour {
	Vector3 localCombatPosition = new Vector3(0F,-3.15F,-7.5F);
	Quaternion localCombatRotation = Quaternion.Euler(325F,0F,0F);
	CameraState combat;

	Vector3 localInventoryPosition = new Vector3(0F,0F,-4F);
	Quaternion localInventoryRotation = Quaternion.Euler(0F,0F,-90F);
	CameraState inventory;

	private CameraState currentState;
	private CameraState desiredState;
	public float transitionTime = 1.2F;
	private bool isTransitioning = false;

	// Use this for initialization
	void Start () {
		combat = new CameraState(localCombatPosition,localCombatRotation);
		inventory = new CameraState(localInventoryPosition,localInventoryRotation);

		currentState = combat;
		desiredState = combat;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Inventory")) {
			toggleState();
		}
	}

	void toggleState() {
		if(!isTransitioning)
		{
			if(currentState == combat)
				StartCoroutine(changeCameraState(combat,inventory,transitionTime));
			else
				StartCoroutine(changeCameraState(inventory,combat,transitionTime));
		}
	}

	IEnumerator changeCameraState(CameraState start, CameraState end, float timeInterval) {
		isTransitioning = true;
		for(float t = 0F; t < timeInterval; t += Time.deltaTime) {
			transform.localPosition = Vector3.Lerp(start.position, end.position, t/timeInterval);
			transform.localRotation = Quaternion.Lerp(start.rotation, end.rotation, t/timeInterval);
			yield return null;
		}
		isTransitioning = false;
		currentState = end;
	}
}