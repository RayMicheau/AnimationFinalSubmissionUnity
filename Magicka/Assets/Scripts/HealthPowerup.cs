using UnityEngine;
using System.Collections;

public class HealthPowerup : MonoBehaviour {

	PlayerHealth  p1_health;
	Player2Health p2_health;

	const int health_amount = 20;

	float turnSpeed = 50.0f;

	// Use this for initialization
	void Awake () {
		p1_health = GameObject.Find ("Player01").GetComponent<PlayerHealth> ();
		p2_health = GameObject.Find ("Player02").GetComponent<Player2Health> ();
	}


	void Update() {
		transform.Rotate (Vector3.one, turnSpeed * Time.deltaTime); 
	
	}

	
	// Update is called once per frame
	void OnTriggerEnter(Collider c) {
		//Debug.Log (c.name);
		if (c.tag == "Player" && c.name == "Player01") {
			p1_health.Heal (health_amount);
			//Debug.Log ("Heal");
			Destroy (gameObject);
		}
		if (c.tag == "Player" && c.name == "Player02") {
			p2_health.Heal (health_amount);
			//Debug.Log ("Heal");
			Destroy (gameObject);
		}
	}


}
