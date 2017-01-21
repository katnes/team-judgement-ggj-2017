using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyControl : MonoBehaviour {
	/* 0 for nuetral/not yet selected
	 * 1 for kill
	 * 2 for let live
	 * */
	public int die = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.D)){
			die = 1;
		}

		if(Input.GetKeyDown(KeyCode.A)){
				die = 2;
			}
			
	}
}
