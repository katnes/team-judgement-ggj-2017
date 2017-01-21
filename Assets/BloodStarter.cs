using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodStarter : MonoBehaviour {

	public GameObject bloodSplat;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


	}

	void OnTriggerEnter(Collider other) {
		bloodSplat.SetActive (true);
	}
}
