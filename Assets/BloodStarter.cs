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
		if (other.CompareTag ("DeathCrate")) {

			bloodSplat.SetActive (true);
			deathScream1.SetActive (false);
			deathScream2.SetActive (false);
			deathScream3.SetActive (false);
			squish.SetActive (true);
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
