using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screndgame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.anyKeyDown) 
		{
		 Application.Quit(); // Quits the game
		}
		
	}
}
