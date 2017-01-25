using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodStarter : MonoBehaviour {

	public GameObject bloodSplat;
	public GameObject deathScream1;
	public GameObject deathScream2;
	public GameObject deathScream3;
	public GameObject squish;
	public GameObject crowdCheer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


	}

	void OnTriggerEnter(Collider other) {
		// Make sure the colliding object is the death create, not a celebrating prisoner
		if (other.name == "DeathCrate") {
			// Start the blood splat and end the death scream
			bloodSplat.SetActive (true);
			deathScream1.SetActive (false);
			deathScream2.SetActive (false);
			deathScream3.SetActive (false);
			squish.SetActive (true);
            GameObject.Find("currentPrisoner").GetComponent<Animator>().enabled = false;
           //StartCoroutine(MyMethod ());
        }
    }
	/*
	IEnumerator MyMethod() {
		
		yield return new WaitForSeconds(.5f);
		crowdCheer.SetActive (true);
	}
	*/
}
