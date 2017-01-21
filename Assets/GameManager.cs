using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public TextMesh text;
	public GameObject deathCrate;
	public GameObject bloodTrigger;
	public float gravitySpeed;



	// Use this for initialization
	void Start () {
		deathCrate.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.D)){
			//run kill code
			text.text = "Killed";
			deathCrate.SetActive(true);
			Physics.gravity = new Vector3(0, gravitySpeed*-1, 0);
			//deathCrate.GetComponent<Rigidbody> ().AddForce (transform.up * gravitySpeed * -1);
			//Physics.gravity.x = gravitySpeed;
			//keyControl.die = 0;
		}
		if (Input.GetKeyDown(KeyCode.A)) {
			//run let live code
			text.text = "let live";
			//keyControl.die = 0;
		}



	}
}
