using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public TextMesh text;
	public GameObject deathCrate;
	//public GameObject bloodTrigger;
	public float gravitySpeed;
	public GameObject deathScream1;
	public GameObject deathScream2;
	public GameObject deathScream3;
	public GameObject squish; 
	public GameObject crowdCheer; 

	int rand;
	float frameSleep = 0.0f;
	bool slept = false;
	bool sleep = false;
	bool screaming = false;

	// Use this for initialization
	void Start () {
		squish.SetActive (false);
		deathCrate.SetActive (false);
		slept = false;
		crowdCheer.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.D)) {
			//run kill code
			text.text = "Killed";
			/*while (frameSleep < 150 && slept == false) {
				frameSleep += 1;
				Debug.Log (frameSleep);
			
			}*/
			frameSleep = 0.0f;
			sleep = true;
			rand = Random.Range (0, 3);
			if (screaming == false) {
				switch (rand) {
				case 0:
					deathScream1.SetActive (true);
					break;
				case 1:
					deathScream2.SetActive (true);
					break;
				case 2:
					deathScream3.SetActive (true);
					break;
				default:
					Debug.Log ("something is wrong");
					break;

				}
			}


		}



		if (sleep) {
			frameSleep+= Time.deltaTime;
			Debug.Log (frameSleep);
		}
		if (frameSleep > 2.0f) {
			sleep = false;

		
			slept = true;

			deathCrate.SetActive (true);
			Physics.gravity = new Vector3 (0, gravitySpeed * -1, 0);


			//Debug.Log (rand);


		}
				


			//deathCrate.GetComponent<Rigidbody> ().AddForce (transform.up * gravitySpeed * -1);
			//Physics.gravity.x = gravitySpeed;
			//keyControl.die = 0;
	
		if (Input.GetKeyDown(KeyCode.A)) {
			//run let live code
			text.text = "let live";
			//keyControl.die = 0;
		}



	}
}
