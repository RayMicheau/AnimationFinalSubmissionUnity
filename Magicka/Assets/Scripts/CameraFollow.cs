using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	GameObject p1;
	GameObject p2;

	Vector3 centerTarget;

	private float aspectRatio;
	private float fov;
	private float tanFov;

	void Awake() {
		p1 = GameObject.Find ("Player01");
		p2 = GameObject.Find ("Player02");

	}

	// Use this for initialization
	void Start () {
		aspectRatio = Screen.width / Screen.height;
		tanFov = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);
	}
	
	// Update is called once per frame
	void Update() {
		Vector3 v1 = p1.transform.position;
		Vector3 v2 = p2.transform.position;

		centerTarget = (v1 - v2) / 2;

		transform.LookAt (centerTarget);

		//Camera.main.transform.position = new Vector3 (centerTarget.x, transform.position.y, transform.position.z);
	}

}
