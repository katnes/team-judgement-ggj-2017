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
    public Animator ragMe1;
    public Animator ragMe2;
    public Animator ragMe3;
    public Animator ragMe4;
    public Animator ragMe5;


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
            // TODO: Be smart enough to find the actual animator on the game object and not use brute force to ragdoll
            // Animator ragMe = GameObject.Find("GameManager").GetComponent("Current Prisoner");
            ragMe1.enabled = false;
            ragMe2.enabled = false;
            ragMe3.enabled = false;
            ragMe4.enabled = false;
            ragMe5.enabled = false;
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
